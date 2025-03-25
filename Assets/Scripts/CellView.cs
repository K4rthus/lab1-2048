using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [Header("Color Settings")]
    [SerializeField] private Color startColor = Color.yellow;
    [SerializeField] private Color endColor = Color.red;
    [SerializeField] private int maxCellValue = 2048;

    [Header("References")]
    [SerializeField] public TextMeshProUGUI valueText;
    [SerializeField] public Image backgroundImage;

    private Cell cell;
    private GameField gameField;

    public void Init(Cell cell, GameField gameField)
    {
        this.cell = cell;
        this.gameField = gameField;
        cell.OnValueChanged += UpdateValue;
        cell.OnPositionChanged += UpdatePosition;

        UpdateValue();
        UpdatePosition();
    }

    private void UpdateValue()
    {
        int displayedValue = (int)Mathf.Pow(2, cell.Value);
        valueText.text = displayedValue.ToString();

        float t = Mathf.Clamp01((float)cell.Value / Mathf.Log(maxCellValue, 2));

        Color targetColor = Color.Lerp(startColor, endColor, t);
        targetColor.a = 1f;
        backgroundImage.color = targetColor;
    }

    private void UpdatePosition()
    {
        int gridSizeX = gameField.Width;
        int gridSizeY = gameField.Height;

        float totalWidth = 218f;
        float totalHeight = 218f;

        float stepX = totalWidth / (gridSizeX - 1);
        float stepY = totalHeight / (gridSizeY - 1);

        float posX = -109 + cell.Position.x * stepX;
        float posY = 109 - cell.Position.y * stepY;

        transform.localPosition = new Vector3(posX, posY, 0);
    }

    private void OnDestroy()
    {
        if (cell != null)
        {
            cell.OnValueChanged -= UpdateValue;
            cell.OnPositionChanged -= UpdatePosition;
        }
    }
}