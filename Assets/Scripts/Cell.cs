using UnityEngine;

public class Cell
{
    private Vector2Int position;
    private int value;

    public event System.Action OnValueChanged;
    public event System.Action OnPositionChanged;

    public Vector2Int Position
    {
        get => position;
        set
        {
            if (position != value)
            {
                position = value;
                OnPositionChanged?.Invoke();
            }
        }
    }

    public int Value
    {
        get => value;
        set
        {
            if (this.value != value)
            {
                this.value = value;
                OnValueChanged?.Invoke();
            }
        }
    }

    public Cell(Vector2Int position, int value)
    {
        this.position = position;
        this.value = value;
    }
}