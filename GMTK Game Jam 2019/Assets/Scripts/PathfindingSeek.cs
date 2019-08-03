using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingSeek : MonoBehaviour
{
    private Pathfinding pathComp;

    public GameObject goal;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Pathfinding2D>().FindPath(transform.position, goal.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Pathfinding2D>().FindPath(transform.position, goal.transform.position);
        gameObject.GetComponent<Pathfinding2D>().Move();
    }
}
