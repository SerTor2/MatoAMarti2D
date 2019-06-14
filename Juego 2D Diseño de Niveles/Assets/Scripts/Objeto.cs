using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    private bool lanzado = false;
    public Vector3 forward = Vector3.zero;
    private float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!lanzado)
        {
            forward += Vector3.down * Time.deltaTime;

            gameObject.transform.position += forward * Time.deltaTime * speed;
        }
        else
            gameObject.transform.position += Vector3.right * Time.deltaTime * speed * 2.5f;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            lanzado = true;
        else if (collision.gameObject.tag == "Map")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Arbol" && lanzado)
        {
            collision.gameObject.GetComponent<TreeEnemie>().DownLife();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "ReyDDD" && lanzado)
        {
            {
                collision.gameObject.GetComponent<ReyDDDScript>().DownLife();
                Destroy(gameObject);
            }
        }


    }
}
