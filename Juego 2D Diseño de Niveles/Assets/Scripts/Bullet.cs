using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float currentTime = 0;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        currentTime += Time.deltaTime;
        if (currentTime >= 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            print("Muerte");
        if (collision.gameObject.tag.Equals("Map"))
            Destroy(gameObject);
    }
}
