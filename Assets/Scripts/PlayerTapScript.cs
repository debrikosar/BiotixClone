using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTapScript : MonoBehaviour
{
    private Touch touch;
    private Vector3 touchPosition;

    public float moveSpeed = 10f;

    public List<GameObject> activeCells;

    private CircleCollider2D circleCollider;

    private int collisionsCheckPre;
    private int collisionsCheckPost;

    [SerializeField]
    private GameObject subcellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        collisionsCheckPre = 0;
        collisionsCheckPost = 0;

        activeCells = new List<GameObject>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, touchPosition, moveSpeed);
            if(!circleCollider.enabled)
                circleCollider.enabled = true;       
            
        }
        else
        {
            if (circleCollider.enabled)
            {
                if (collisionsCheckPost == collisionsCheckPre)
                    ResetActiveCells();
                else
                    collisionsCheckPost = collisionsCheckPre;
                circleCollider.enabled = false;
            }
        }
    }

    private void ResetActiveCells()
    {
        foreach(GameObject cell in activeCells)
            cell.GetComponent<CellScript>().IsActive = false;

        activeCells = new List<GameObject>();

        collisionsCheckPre = 0;
        collisionsCheckPost = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cell")
        {
            CellScript tempCollisionCellScript = collision.gameObject.GetComponent<CellScript>();

            if(tempCollisionCellScript.CellOwner == "Green")
            {
                if (!tempCollisionCellScript.IsActive)
                {
                    tempCollisionCellScript.IsActive = true;
                    activeCells.Add(collision.gameObject);

                    collisionsCheckPre = activeCells.Count;
                }
            }

            else if(activeCells.Count > 0)
            {
                CellScript tempCellScript;
                foreach (GameObject cell in activeCells)
                {
                    tempCellScript = cell.GetComponent<CellScript>();
                    //add target removal & amount of subcells

                    StartCoroutine(SubcellSpawning(tempCellScript.CellCount / 2, cell.transform.position, collision.gameObject));

                    //collision.gameObject.GetComponent<CellScript>().CellHit("Green", cell.GetComponent<CellScript>().CellCount / 2);
                    tempCellScript.CellCount = tempCellScript.CellCount / 2;
                    tempCellScript.UpdateCellText();
                }

                ResetActiveCells();
            }
        }
    }

    IEnumerator SubcellSpawning(float cellCount, Vector2 subcellPosition, GameObject subcellDestination)
    {
        for (var i = 0; i < cellCount; i++)
        {
            GameObject instance = Instantiate(subcellPrefab, subcellPosition, Quaternion.identity);
            instance.GetComponent<SubcellScript>().SubcellTarget = subcellDestination;
            instance.GetComponent<SubcellScript>().StartAttack(subcellDestination.transform.position);
            instance.GetComponent<SubcellScript>().SubcellOwner = "Green";

            yield return new WaitForSeconds(0.03f);
        }

    }
}
