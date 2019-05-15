using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTravesable : MonoBehaviour
{
    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player") && collision.gameObject.transform.position.y > gameObject.transform.position.y)
        {
            collider.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collider.enabled = false;
        }
    }
}
