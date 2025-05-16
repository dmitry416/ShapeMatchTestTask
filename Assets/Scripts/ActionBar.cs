using System;
using UnityEngine;

public class ActionBar : MonoBehaviour
{
    [SerializeField] private AnimalCell[] _cells;

    public Action onClear;
    public Action onFull;
    public Action onAdd;

    public void AddAnimal(AnimalShape animalShape)
    {
        int count = 0;
        bool isPlaced = false;
        bool isFull = true;
        foreach (AnimalCell cell in _cells)
        {
            if (!cell.isActive)
            {
                if (!isPlaced)
                {
                    cell.SetCell(animalShape.shape, animalShape.color, animalShape.animal);
                    isPlaced = true;
                    onAdd?.Invoke();
                }
                else
                    isFull = false;
            }
            else if (cell.shape.sprite == animalShape.shape && cell.color == animalShape.color && cell.animal.sprite == animalShape.animal)
                ++count;
        }
        if (count == 2)
        {
            foreach (AnimalCell cell in _cells)
                if (cell.isActive && cell.shape.sprite == animalShape.shape && cell.color == animalShape.color && cell.animal.sprite == animalShape.animal)
                    cell.ClearCell();
            onClear?.Invoke();
        }
        else if (isFull)
            onFull?.Invoke();
    }
}
