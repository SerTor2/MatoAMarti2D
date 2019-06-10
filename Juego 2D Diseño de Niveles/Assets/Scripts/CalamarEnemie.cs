using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalamarEnemie : MonoBehaviour
{
    private Vector3 posInitial;
    private float speed = 10;
    private float speedInitial = 0;
    public float altura = 5;
    private float currentTime = 0;
    private float maxTimeWait = 1;
    private bool ir = true;
    // Start is called before the first frame update
    void Start()
    {
        posInitial = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime >= maxTimeWait)
        {
            if(ir)
            {
                speed -= Time.deltaTime * 10;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posInitial + new Vector3(0,altura,0), speed * Time.deltaTime);
                if((gameObject.transform.position - (posInitial + new Vector3(0, altura,0))).magnitude <= 0.05)
                {
                    ir = false;
                    speedInitial = 0;
                }
                if (speed <= -8)
                {
                    ir = false;
                    speedInitial = speed * -1;
                }
            }
            else
            {
                speedInitial += Time.deltaTime * 3;
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, posInitial, speedInitial * Time.deltaTime);
                if ((gameObject.transform.position - posInitial).magnitude <= 0.05)
                {
                    ir = true;
                    currentTime = 0;
                    speed = 10;
                    gameObject.transform.position = posInitial;
                }
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
          Debug.Log("Matar");
            
        }
    }
}
