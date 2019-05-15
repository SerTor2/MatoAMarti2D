using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemie : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public GameObject bulletPrefab;
    private float currentTime = 0;
    private GameObject player;
    public float cadencia = 2f;
    public Transform posToBullet;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if ((gameObject.transform.position - player.gameObject.transform.position).magnitude <= 50)
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
            if (collision.gameObject.transform.position.y > gameObject.transform.position.y + boxCollider.size.y / 6)
            {
                collision.gameObject.GetComponent<PlayerScript>().Jump();
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Matar");
            }
        }
    }
}
