using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiePatrol : MonoBehaviour
{
    public Transform patrol1;
    public Transform patrol2;
    public float speed = 3;
    public bool killable = true;
    private bool toPatrol1 = true;
    private Vector3 startPos;
    private CircleCollider2D circleCollider;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(patrol1 != null && patrol2 != null)
        {
            if (toPatrol1)
            {
                if (patrol1.position.x <= gameObject.transform.position.x)
                    gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                else
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);


                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol1.position, speed * Time.deltaTime);
                if ((patrol1.position - gameObject.transform.position).magnitude <= 0.05)
                    toPatrol1 = false;
            }
            else
            {
                if (patrol2.position.x <= gameObject.transform.position.x)
                    gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                else
                    gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);

                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, patrol2.position, speed * Time.deltaTime);
                if ((patrol2.position - gameObject.transform.position).magnitude <= 0.05)
                    toPatrol1 = true;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(collision.gameObject.transform.position.y > gameObject.transform.position.y + circleCollider.radius / 10 && killable)
            {
                collision.gameObject.GetComponent<PlayerScript>().Jump();
                gameObject.SetActive(false);
            }
            else
            {
                collision.gameObject.GetComponent<PlayerScript>().Respawn();
            }
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = startPos;
        killable = true;
        toPatrol1 = true;
        gameObject.SetActive(true);
    }

}
