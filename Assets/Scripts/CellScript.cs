using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CellScript : MonoBehaviour
{
    [SerializeField]
    private string cellType;
    [SerializeField]
    private string cellOwner;

    private int cellMax;
    [SerializeField]
    private int cellCount;

    private bool isActive;
    private bool isProducing;

    private SpriteRenderer cellSpriteRenderer;
    private SpriteRenderer cellFillingSpriteRenderer;

    private GameObject cellBorder;
    private SpriteRenderer cellBorderSpriteRenderer;

    private TextMeshPro cellText;

    public static event Action<GameObject, string, string> OnChangeCellOwner;

    public bool IsActive
    {
        get => isActive;
    }

    public string CellOwner
    {
        get => cellOwner;
        set => cellOwner = value;
    }

    public int CellCount
    {
        get => cellCount;
        set => cellCount = value;
    }

    private void Start()
    {
        cellSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        cellFillingSpriteRenderer = gameObject.transform.Find("CellFilling").GetComponent<SpriteRenderer>();
        cellBorder = gameObject.transform.Find("CellBorder").gameObject;
        cellBorderSpriteRenderer = cellBorder.GetComponent<SpriteRenderer>();
        cellText = gameObject.transform.Find("CellFilling").transform.Find("CellText").GetComponent<TextMeshPro>();

        DefineCellType();
        DefineCellColor();

        UpdateCellText();

        if (cellOwner!="")
            StartCoroutine(GenerateCells());
    }

    private void DefineCellType()
    {
        switch(cellType)
        {
            case "Big":
                cellSpriteRenderer.transform.localScale = new Vector2(1.5f, 1.5f);
                cellMax = 60;
                break;
            case "Medium":
                cellSpriteRenderer.transform.localScale = new Vector2(1.2f, 1.2f);
                cellMax = 40;
                break;
            default:
                cellMax = 20;
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
       isProducing = true;
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

    public void CellHit(string subcellOwner, int damage)
    {
        if (cellCount < 2)
        {
            ChangeCellOwner(subcellOwner);
        }

        cellCount-=damage;

        UpdateCellText();
    }

    public void ChangeCellOwner(string subcellOwner)
    {
        OnChangeCellOwner?.Invoke(this.gameObject, cellOwner, subcellOwner);

        cellOwner = subcellOwner;
        DefineCellColor();
        if (!isProducing)
            StartCoroutine(GenerateCells());        
    }
    
    public void UpdateCellText()
    {
        if (cellCount != 0)
            cellText.text = cellCount.ToString();
        else
            cellText.text = "";
    }

    public void ActivateCell()
    {
        isActive = true;
        StartCoroutine(AnimateActivation());
    }

    public void DeactivateCell()
    {
        isActive = false;
        StopCoroutine(AnimateActivation());
        cellBorderSpriteRenderer.transform.localScale = new Vector2(0f, 0f);
    }

    IEnumerator AnimateActivation()
    {
        float targetSize = cellFillingSpriteRenderer.transform.localScale.x * 0.050f;
        for (float i = 0; i < targetSize; i += (targetSize) / 10)
        {
            if (isActive)
            {
                cellBorderSpriteRenderer.transform.localScale = new Vector2(cellBorderSpriteRenderer.transform.localScale.x + i, cellBorderSpriteRenderer.transform.localScale.y + i);
                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                cellBorderSpriteRenderer.transform.localScale = new Vector2(0f, 0f);
                yield break;
            }
        }              
    }
}
