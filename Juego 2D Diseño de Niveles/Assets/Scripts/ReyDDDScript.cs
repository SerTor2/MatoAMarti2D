using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReyDDDScript : MonoBehaviour
{
    private int life = 5;
    public GameObject bulletPrefab;
    private float currentTime = 0;
    public float cadencia = 1.3f;
    private GameObject player;
    public GameObject objetoPrefab;
    public List<Transform> transforms = new List<Transform>();
    private bool dashes = false;
    private bool go = true;
    private Vector3 posInit;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        posInit = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (Random.Range(0, 2000) <= 1.5f && !dashes)
            {
                currentTime = 0;
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                dashes = true;
            }
            if (!dashes)
            {
                if (Random.Range(0, 1000) >= 5)
                {
                    if ((gameObject.transform.position - player.gameObject.transform.position).magnitude <= 20)
                    {
                        currentTime += Time.deltaTime;
                        if (currentTime >= cadencia)
                        {
                            int bullet = Random.Range(0, 3);
                            Instantiate(bulletPrefab, transforms[bullet].position, bulletPrefab.transform.rotation).GetComponent<Bullet>().speed = 10;
                            currentTime -= cadencia;
                        }
                    }
                }
                else
                {
                    Instantiate(objetoPrefab, transforms[2].position, objetoPrefab.transform.rotation).GetComponent<Objeto>().forward = new Vector3(Random.Range(-0.25f, -0.75f), Random.Range(0.5f, 1f));

                }
            }
            else
            {
                if (go && currentTime >= 1f)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posInit + Vector3.left * 8, 14 * Time.deltaTime);
                    if ((gameObject.transform.position - (posInit + Vector3.left * 8)).magnitude <= 0.2f)
                        go = false;
                }
                else if (!go)
                {
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posInit, 14 * Time.deltaTime);
                    if ((gameObject.transform.position - posInit).magnitude <= 0.2f)
                    {
                        go = true;
                        dashes = false;
                        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                        currentTime = 0;
                    }
                }
                else
                    currentTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<PlayerScript>().Respawn();
    }

    public void DownLife()
    {
        life--;
        if (life <= 0)
            gameObject.SetActive(false);
    }

    public void Respawn()
    {
        life = 5;
        gameObject.transform.position = posInit;
        go = true;
        dashes = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.SetActive(true);
    }
}

