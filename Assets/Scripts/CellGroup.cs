using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGroup
{
    private string cellGroupOwner;
    private List<GameObject> cellGroup;

    public string CellGroupOwner
    {
        get => cellGroupOwner;
        set => cellGroupOwner = value;
    }

    public CellGroup(string cellGroupOwner)
    {
        this.cellGroupOwner = cellGroupOwner;
        this.cellGroup = new List<GameObject>();
    }

    public CellGroup(string cellGroupOwner, List<GameObject> cellGroup)
    {
        this.cellGroupOwner = cellGroupOwner;
        this.cellGroup = cellGroup;
    }

    public int GetCellGroupSize() =>
        cellGroup.Count;

    public void AddCellToGroup(GameObject newCell)
    {
        cellGroup.Add(newCell);
    }

    public void RemoveCellFromGroup(GameObject removedCell)
    {
        cellGroup.Remove(removedCell);
    }
}
