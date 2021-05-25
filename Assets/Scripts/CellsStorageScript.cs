using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellsStorageScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cellStorage;

    private List<CellGroup> cellGroups;

    private void Start()
    {
        cellGroups = new List<CellGroup>();
        cellGroups.Add(new CellGroup("Green"));
        cellGroups.Add(new CellGroup("Blue"));
        cellGroups.Add(new CellGroup(""));

        foreach (GameObject cell in cellStorage)
        {
            cellGroups.Find(item => item.CellGroupOwner == cell.GetComponent<CellScript>().CellOwner).AddCellToGroup(cell);
        }

        CellScript.OnChangeCellOwner += RecountCells;
    }

    public void RecountCells(GameObject cell, string cellOldOwner, string cellNewOwner)
    {
        cellGroups.Find(item => item.CellGroupOwner == cellOldOwner).RemoveCellFromGroup(cell);
        cellGroups.Find(item => item.CellGroupOwner == cellNewOwner).AddCellToGroup(cell);

        foreach(CellGroup cellGroup in cellGroups)
        {
            if(cellGroup.GetCellGroupSize() == cellStorage.Count)
            {
                if(cellGroup.CellGroupOwner == "Green")
                    SceneManager.LoadScene(1);
                else
                    SceneManager.LoadScene(2);
            }
        }
    }

    private void OnDestroy()
    {
        CellScript.OnChangeCellOwner -= RecountCells;
    }
}
