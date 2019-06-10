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
    public GameObject checkPoint;
    private List<EnemiePatrol> enemiePatrol =  new List<EnemiePatrol>();
    private List<TreeEnemie> enemieTree = new List<TreeEnemie>();
    private List<TurretEnemie> enemieTurret = new List<TurretEnemie>();
    private List<GameObject> coins = new List<GameObject>();
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject[] enemiesP = GameObject.FindGameObjectsWithTag("EnemiePatrol");
        foreach(GameObject go in enemiesP)
        {
            enemiePatrol.Add(go.GetComponent<EnemiePatrol>());
        }
        GameObject[] enemiesT = GameObject.FindGameObjectsWithTag("EnemieTurret");
        foreach (GameObject go in enemiesT)
        {
            enemieTurret.Add(go.GetComponent<TurretEnemie>());
        }
        GameObject[] enemiesTr = GameObject.FindGameObjectsWithTag("Arbol");
        foreach (GameObject go in enemiesTr)
        {
            enemieTree.Add(go.GetComponent<TreeEnemie>());
        }

        GameObject[] normalCoins = GameObject.FindGameObjectsWithTag("NormalCoin");
        foreach (GameObject go in normalCoins)
        {
            coins.Add(go);
        }

        GameObject[] specialCoins = GameObject.FindGameObjectsWithTag("SpecialCoin");
        foreach (GameObject go in specialCoins)
        {
            coins.Add(go);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "CheckPoint")
        {
            checkPoint = collision.gameObject;
        }
        if(collision.gameObject.tag == "Dead")
        {
            Respawn();
        }
    }

    public void Jump()
    {
        onGround = false;
        rb.gravityScale = 0;
        verticalSpeed = 7;
    }

    public void Respawn()
    {
        verticalSpeed = 0;
        foreach (EnemiePatrol enemie in enemiePatrol)
        {
            if (checkPoint != null)
            {
                if (enemie.transform.position.x >= checkPoint.transform.position.x)
                {
                    enemie.Respawn();
                }
            }
            else enemie.Respawn();
        }

        foreach (TreeEnemie enemie in enemieTree)
        {
            if (checkPoint != null)
            {
                if (enemie.transform.position.x >= checkPoint.transform.position.x)
                {
                    enemie.Respawn();
                }
            }
            else enemie.Respawn();
        }

        foreach (TurretEnemie enemie in enemieTurret)
        {
            if (checkPoint != null)
            {
                if (enemie.transform.position.x >= checkPoint.transform.position.x)
                {
                    enemie.Respawn();
                }
            }
            else enemie.Respawn();
        }

        foreach (GameObject go in coins)
        {
            if (checkPoint != null)
            {
                if (go.transform.position.x >= checkPoint.transform.position.x)
                {
                    go.SetActive(true);
                }
            }
            else go.SetActive(true);
        }

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(go);
        }

        if (checkPoint != null)
            gameObject.transform.position = checkPoint.transform.position;
    }

}
