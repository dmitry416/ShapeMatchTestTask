using UnityEngine;

public class TouchAnimal : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    private bool _isEnd = false;

    private void OnEnable()
    {
        _gameManager.onWin += End;
        _gameManager.onLose += End;
    }

    private void OnDisable()
    {
        _gameManager.onWin -= End;
        _gameManager.onLose -= End;
    }


    private void End()
    {
        _isEnd = true;
    }


    private void Update()
    {
        if (!_isEnd && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.transform.TryGetComponent(out AnimalShape shape))
            {
                shape.Click();
            }
        }
    }
}
