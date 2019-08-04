using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    float timer = -5.0f;
    public float spawnTime = 5f;
    public GameObject spawnMe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnMe != null)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                timer = 0f;
                Instantiate(spawnMe);
            }
        }
    }
}
