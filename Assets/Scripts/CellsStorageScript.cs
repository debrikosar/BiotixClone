using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CellsStorageScript : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> cellStorage;

    private List<CellGroup> cellGroups;

    [SerializeField]
    private GameObject subcellPrefab;

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

        StartCoroutine(EnemyLogic());
    }

    public void RecountCells(GameObject cell, string cellOldOwner, string cellNewOwner)
    {
        cellGroups.Find(item => item.CellGroupOwner == cellOldOwner).RemoveCellFromGroup(cell);
        cellGroups.Find(item => item.CellGroupOwner == cellNewOwner).AddCellToGroup(cell);

        foreach(CellGroup cellGroup in cellGroups)
        {
            /*if(cellGroup.GetCellGroupSize() == cellStorage.Count)
            {
                if(cellGroup.CellGroupOwner == "Green")
                    SceneManager.LoadScene(1);
                else
                    SceneManager.LoadScene(2);
            }*/
            if(cellGroup.CellGroupOwner == "Green" && cellGroup.GetCellGroupSize() == 0)
                SceneManager.LoadScene(2);
            else if(cellGroup.CellGroupOwner == "Blue" && cellGroup.GetCellGroupSize() == 0)
                SceneManager.LoadScene(1);
        }
    }

    private void OnDestroy()
    {
        CellScript.OnChangeCellOwner -= RecountCells;
    }

    IEnumerator EnemyLogic()
    {
        GameObject randomAttacker;
        GameObject randomDefender;
        List<GameObject> targetGroup;
        float timer;

        while (true)
        {
            timer = Random.Range(1f, 3f);

            foreach (CellGroup cellGroup in cellGroups)
            {
                randomDefender = null;
                if (cellGroup.CellGroupOwner != "Green" && cellGroup.CellGroupOwner != "")
                {
                    randomAttacker = cellGroup.CellGroupList[Random.Range(0, cellGroup.CellGroupList.Count)];

                    while (randomDefender == null)
                    {
                        switch (Random.Range(0, 4))
                        {
                            case 0:
                                targetGroup = cellGroups.Find(item => item.CellGroupOwner == "Green").CellGroupList;
                                if (targetGroup.Count != 0)
                                    randomDefender = targetGroup[Random.Range(0, targetGroup.Count)];
                                break;
                            default:
                                targetGroup = cellGroups.Find(item => item.CellGroupOwner == "").CellGroupList;
                                if (targetGroup.Count != 0)
                                    randomDefender = targetGroup[Random.Range(0, targetGroup.Count)];
                                break;
                        }
                    }
                    StartCoroutine(SubcellSpawning(randomAttacker.GetComponent<CellScript>().CellCount/2, randomAttacker.transform.position, randomDefender, cellGroup.CellGroupOwner));
                    randomAttacker.GetComponent<CellScript>().CellCount = randomAttacker.GetComponent<CellScript>().CellCount / 2;
                }
            }
            yield return new WaitForSeconds(timer);
        }
    }

    IEnumerator SubcellSpawning(float cellCount, Vector2 subcellPosition, GameObject subcellDestination, string cellOwner)
    {
        for (var i = 0; i < cellCount; i++)
        {
            GameObject instance = Instantiate(subcellPrefab, subcellPosition, Quaternion.identity);
            instance.GetComponent<SubcellScript>().SubcellTarget = subcellDestination;
            instance.GetComponent<SubcellScript>().StartAttack(subcellDestination.transform.position);
            instance.GetComponent<SubcellScript>().SubcellOwner = cellOwner;

            yield return new WaitForSeconds(0.03f);
        }

    }
}
