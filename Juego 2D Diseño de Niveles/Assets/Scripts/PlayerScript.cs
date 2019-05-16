using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector3 toMove;
    private float speed = 2;
    private float verticalSpeed = 0;
    private float gravity = 20;
    private bool onGround = true;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        toMove = Vector3.zero;
        if (!onGround)
        {
            verticalSpeed -= gravity * Time.deltaTime;
            if (verticalSpeed <= -5)
                verticalSpeed = -5;
        }
        else
        {
            verticalSpeed = 0;
        }



        if (Input.GetKey(KeyCode.A))
            toMove += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            toMove += Vector3.right;

        toMove = toMove.normalized;
        toMove *= 3;
        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            Jump();
        }

        toMove += Vector3.up * verticalSpeed;

        gameObject.transform.position += toMove * Time.deltaTime * speed;


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Map")
        {
            if (collision.gameObject.transform.position.y < gameObject.transform.position.y)
            {
                rb.gravityScale = 6;
                onGround = true;
            }
            verticalSpeed = 0;
        }
    }

    public void Jump()
    {
        onGround = false;
        rb.gravityScale = 0;
        verticalSpeed = 7;
    }

}
