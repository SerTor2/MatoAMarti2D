using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemie : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    public GameObject bulletPrefab;
    private float currentTime = 0;
    private GameObject player;
    public float cadencia = 2f;
    public Transform posToBullet;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if ((gameObject.transform.position - player.gameObject.transform.position).magnitude <= 20)
            {
                currentTime += Time.deltaTime;
                if(currentTime >= cadencia)
                {
                    Instantiate(bulletPrefab, posToBullet.position, bulletPrefab.transform.rotation);
                    currentTime -= cadencia;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (collision.gameObject.transform.position.y > gameObject.transform.position.y + circleCollider.radius / 8)
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
        gameObject.SetActive(true);
    }
}
