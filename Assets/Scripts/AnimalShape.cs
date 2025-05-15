using System;
using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

[RequireComponent(typeof(Rigidbody2D))]
public class AnimalShape : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _shapeImg;
    [SerializeField] private SpriteRenderer _borderImg;
    [SerializeField] private SpriteRenderer _animalImg;

    private Rigidbody2D _rb;

    public Action<AnimalShape> onClick;
    public Sprite shape { get { return _shapeImg.sprite; } }
    public Sprite animal { get { return _animalImg.sprite; } }
    public Color color { get { return _shapeImg.color; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void SetView(Sprite shape, Sprite border, Sprite animal, Color color)
    {
        _shapeImg.sprite = shape;
        _borderImg.sprite = border;
        _animalImg.sprite = animal;
        _shapeImg.color = color;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        _rb.simulated = true;
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        _rb.simulated = false;
    }

    public void Click()
    {
        onClick?.Invoke(this);
        Destroy(gameObject);
    }
}
