using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEnemie : MonoBehaviour
{
    private int life = 3;
    public GameObject bulletPrefab;
    private float currentTime = 0;
    public float cadencia = 1f;
    private GameObject player;
    public List<Transform> transforms = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
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
                if (currentTime >= cadencia)
                {
                    int bullet = Random.Range(0, 3);
                    Instantiate(bulletPrefab, transforms[bullet].position, bulletPrefab.transform.rotation);
                    currentTime -= cadencia;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            print("Muerte");
    }

    public void DownLife()
    {
        life--;
        if (life <= 0)
            Destroy(gameObject);
    }
}
