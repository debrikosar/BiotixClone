using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTapScript : MonoBehaviour
{
    private Touch touch;
    private Vector3 touchPosition;
    public float moveSpeed = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            transform.position = Vector2.Lerp(transform.position, touchPosition, moveSpeed);
        }
    }
}
