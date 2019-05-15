using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    private bool lanzado = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!lanzado)
            gameObject.transform.position += Vector3.down * Time.deltaTime * 3;
        else
            gameObject.transform.position += Vector3.right * Time.deltaTime * 3;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            lanzado = true;
        else if (collision.gameObject.tag == "Map")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "Arbol")
        {
            collision.gameObject.GetComponent<TreeEnemie>().DownLife();
            Destroy(gameObject);
        }


    }
}
