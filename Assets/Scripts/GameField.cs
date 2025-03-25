using UnityEngine;
using System.Collections.Generic;

public class GameField : MonoBehaviour
{
    [SerializeField] public int width = 4;
    [SerializeField] public int height = 4;
    [SerializeField] public CellView cellPrefab;

    public int Width => width;
    public int Height => height;

    private List<Cell> cells = new();

    public void Start()
    {
        CreateCell();
        CreateCell();
    }

    public Vector2Int? GetEmptyPosition()
    {
        HashSet<Vector2Int> occupiedPositions = new();
        foreach (Cell cell in cells)
        {
            occupiedPositions.Add(cell.Position);
        }

        List<Vector2Int> emptyPositions = new();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2Int pos = new(x, y);
                if (!occupiedPositions.Contains(pos))
                {
                    emptyPositions.Add(pos);
                }
            }
        }

        return emptyPositions.Count == 0
            ? null
            : emptyPositions[Random.Range(0, emptyPositions.Count)];
    }

    public void CreateCell()
    {
        Vector2Int? emptyPos = GetEmptyPosition();
        if (!emptyPos.HasValue) return;

        int value = Random.Range(0, 100) < 90 ? 1 : 2;
        Cell newCell = new(emptyPos.Value, value);
        cells.Add(newCell);

        CellView cellView = Instantiate(cellPrefab, transform);
        cellView.Init(newCell, this);
    }
}