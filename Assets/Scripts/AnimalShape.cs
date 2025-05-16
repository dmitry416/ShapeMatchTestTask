using System;
using System.Collections.Generic;
using UnityEngine;

public enum ShapeType
{
    Simple, Heavy, Explosive, Frozen
}

[RequireComponent(typeof(Rigidbody2D))]
public class AnimalShape : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _shapeImg;
    [SerializeField] private SpriteRenderer _borderImg;
    [SerializeField] private SpriteRenderer _animalImg;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 2f;

    private Rigidbody2D _rb;
    private ShapeType _type;
    private int _deletions = 10;
    private ActionBar _ab;

    public Action<AnimalShape> onClick;
    public Sprite shape { get { return _shapeImg.sprite; } }
    public Sprite animal { get { return _animalImg.sprite; } }
    public Color color { get { return _shapeImg.color; } }
    public ShapeType type { get { return _type; } }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ab = FindFirstObjectByType<ActionBar>();
    }

    private void OnEnable()
    {
        _ab.onAdd += Unfroze;
    }

    private void OnDisable()
    {
        _ab.onAdd -= Unfroze;
    }

    public void SetView(Sprite shape, Sprite border, Sprite animal, Color color, ShapeType type)
    {
        _shapeImg.sprite = shape;
        _borderImg.sprite = border;
        _animalImg.sprite = animal;
        _shapeImg.color = color;
        _type = type;

        switch (type)
        {
            case ShapeType.Heavy:
                _rb.gravityScale = 3f;
                _borderImg.color = Color.black;
                break;
            case ShapeType.Explosive:
                _borderImg.color = Color.red;
                break;
            case ShapeType.Frozen:
                _borderImg.color = Color.blue;
                
                break;
        }
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
        switch (_type)
        {
            case ShapeType.Explosive:
                Explode();
                break;
            case ShapeType.Frozen:
                if (_deletions > 0)
                    return;
                break;
        }
        onClick?.Invoke(this);
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);

        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.TryGetComponent<AnimalShape>(out var shape))
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                shape.GetComponent<Rigidbody2D>().AddForce(direction * _explosionForce, ForceMode2D.Impulse);
            }
        }
    }

    private void Unfroze()
    {
        if (--_deletions <= 0 && _type == ShapeType.Frozen)
        {
            _borderImg.color = Color.white;
        }
    }
}
