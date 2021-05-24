using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CellScript : MonoBehaviour
{
    [SerializeField]
    private string cellType;
    [SerializeField]
    private string cellOwner;

    private int cellMax;
    public int cellCount;

    private SpriteRenderer cellSpriteRenderer;
    private SpriteRenderer cellFillingSpriteRenderer;
    private TextMeshPro cellText;

    void Start()
    {
        cellSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        cellFillingSpriteRenderer = gameObject.transform.Find("CellFilling").GetComponent<SpriteRenderer>();
        cellText = gameObject.transform.Find("CellFilling").transform.Find("CellText").GetComponent<TextMeshPro>();

        DefineCellType();
        DefineCellColor();

        cellCount = 10;

        StartCoroutine(GenerateCells());
    }

    private void DefineCellType()
    {
        switch(cellType)
        {
            case "Big":
                cellSpriteRenderer.transform.localScale = new Vector2(1.5f, 1.5f);
                cellMax = 20;
                break;
            default:
                cellMax = 10;
                break;
        }
    }

    private void DefineCellColor()
    {
        switch (cellOwner)
        {
            case "Red":
                cellSpriteRenderer.color = Color.red;
                cellFillingSpriteRenderer.color = Color.red;
                break;
            case "Blue":
                cellSpriteRenderer.color = Color.blue;
                cellFillingSpriteRenderer.color = Color.blue;
                break;
            case "Green":
                cellSpriteRenderer.color = Color.green;
                cellFillingSpriteRenderer.color = Color.green;
                break;
        }
    }

    IEnumerator GenerateCells()
    {
        while (true)
        {
            if (cellCount < cellMax)
            {
                cellCount += 1;
                cellText.text = cellCount.ToString();
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
