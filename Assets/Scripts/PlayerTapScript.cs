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

    // Start is called before the first frame update
    void Start()
    {
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
            if(activeCells.Count != 0)
            {
                ResetActiveCells();
            }
            circleCollider.enabled = false;

            if (circleCollider.enabled)
                circleCollider.enabled = false;
        }
    }

    private void ResetActiveCells()
    {
        foreach(GameObject cell in activeCells)
            cell.GetComponent<CellScript>().IsActive = false;

        activeCells = new List<GameObject>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("boo");
        if (collision.gameObject.tag == "Cell")
        {
            if(collision.gameObject.GetComponent<CellScript>().CellOwner == "Green")
            {
                if (!collision.gameObject.GetComponent<CellScript>().IsActive)
                {
                    collision.gameObject.GetComponent<CellScript>().IsActive = true;
                    activeCells.Add(collision.gameObject);
                }
            }
        }
    }
}
