using UnityEngine;
using UnityEngine.UI;

public class AnimalCell : MonoBehaviour
{
    [SerializeField] private Image _shape;
    [SerializeField] private Image _animal;

    private bool _isActive = false;

    public bool isActive { get { return _isActive; } }
    public Image shape { get { return _shape; } }
    public Image animal { get { return _animal; } }
    public Color color { get { return _shape.color; } }

    public void SetCell(Sprite shape, Color color, Sprite animal)
    {
        _shape.sprite = shape;
        _shape.color = color;
        _animal.sprite = animal;
        _shape.gameObject.SetActive(true);
        _animal.gameObject.SetActive(true);
        _isActive = true;
    }

    public void ClearCell()
    {
        _shape.gameObject.SetActive(false);
        _animal.gameObject.SetActive(false);
        _isActive = false;
    }
}
