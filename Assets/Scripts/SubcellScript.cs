using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubcellScript : MonoBehaviour
{
    private string subcellOwner;

    public string SubcellOwner
    {
        get => subcellOwner;
        set => subcellOwner = value;
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cell")
        {
            if (collision.gameObject.GetComponent<CellScript>().IsTarget == true)
            {
                if(collision.gameObject.GetComponent<CellScript>().CellOwner != subcellOwner)
                    collision.gameObject.GetComponent<CellScript>().CellHit(subcellOwner, 1);
                else
                    collision.gameObject.GetComponent<CellScript>().CellHit(subcellOwner, -1);
                StopAllCoroutines();
                Destroy(gameObject);
            }
        }
    }

    public void StartAttack(Vector2 direction)
    {
        StartCoroutine(AttackCell(direction));
    }

    IEnumerator AttackCell(Vector2 direction)
    {
        while (true) 
        {
            transform.position = Vector3.MoveTowards(transform.position, direction, 0.01f);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
