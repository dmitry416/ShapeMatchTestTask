using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 1.5f;
    [SerializeField] private int _triplesCount = 7;
    [SerializeField] private float _dropSpeed = 0.1f;
    [Space]
    [SerializeField] private ActionBar _actionBar;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private AnimalShape _shapePrefab;
    [SerializeField] private Sprite[] _animalSprites;
    [SerializeField] private Sprite[] _shapes;
    [SerializeField] private Sprite[] _borders;
    [SerializeField] private Color[] _colors;

    private AnimalShape[] _animalShapes;
    private int _triplesCleared = 0;

    public Action onWin;
    public Action onLose;

    private void OnEnable()
    {
        _actionBar.onClear += PlusScore;
        _actionBar.onFull += LoseGame;
    }

    private void OnDisable()
    {
        _actionBar.onClear -= PlusScore;
        _actionBar.onFull -= LoseGame;
    }

    private void Start()
    {
        _animalShapes = new AnimalShape[_triplesCount * 3];
        InitShapes();
        Drop();
    }

    private void InitShapes()
    {
        for (int i = 0; i < _triplesCount; ++i)
        {
            Color color = _colors[UnityEngine.Random.Range(0, _colors.Length)];
            Sprite animal = _animalSprites[UnityEngine.Random.Range(0, _animalSprites.Length)];
            int id = UnityEngine.Random.Range(0, _shapes.Length);
            Sprite shape = _shapes[id];
            Sprite border = _borders[id];
            for (int j = 0; j < 3; ++j)
            {
                AnimalShape animalShape = Instantiate(_shapePrefab);
                animalShape.SetView(shape, border, animal, color, (ShapeType)(4 - Mathf.Sqrt(UnityEngine.Random.Range(0.01f, 16f))));
                animalShape.onClick += MoveToActionBar;
                _animalShapes[i * 3 + j] = animalShape;
            }
        }
    }

    public void Drop()
    {
        StopAllCoroutines();
        StartCoroutine(DropShapes());
    }

    private IEnumerator DropShapes()
    {
        foreach (AnimalShape shape in _animalShapes)
            if (shape != null)
                shape.Deactivate();
        
        foreach (AnimalShape shape in _animalShapes)
        {
            if (shape == null)
                continue;
            shape.transform.position = _spawnPoint.position + Vector3.right * (_spawnPoint.position.x + UnityEngine.Random.Range(-_spawnRadius, _spawnRadius));
            shape.Activate();
            yield return new WaitForSeconds(_dropSpeed);
        }
    }

    private void MoveToActionBar(AnimalShape shape)
    {
        _actionBar.AddAnimal(shape);
    }

    private void PlusScore()
    {
        if (++_triplesCleared >= _triplesCount)
            WinGame();
    }

    private void LoseGame()
    {
        onLose?.Invoke();
    }

    private void WinGame()
    {
        onWin?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
