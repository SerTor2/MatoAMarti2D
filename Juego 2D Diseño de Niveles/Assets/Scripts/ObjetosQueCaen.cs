using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosQueCaen : MonoBehaviour
{
    public GameObject objetoPrefab;
    public List<Transform> trans = new List<Transform>();
    private float currentTime = 0;
    private float randomFloat;
    private int randomInt;
    // Start is called before the first frame update
    void Start()
    {
        randomFloat = Random.Range(5, 10);
        randomInt = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= randomFloat)
        {
            currentTime = 0;
            Instantiate(objetoPrefab, trans[randomInt].position, objetoPrefab.transform.rotation);
            randomFloat = Random.Range(5, 10);
            randomInt = Random.Range(0, 3);
        }
    }
}
