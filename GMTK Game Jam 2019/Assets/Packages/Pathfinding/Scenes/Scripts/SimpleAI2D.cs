using UnityEngine;
using System.Collections;
using System.Linq;

public class SimpleAI2D : Pathfinding2D 
{
    public uint SearchPerSecond = 5;
    public Transform Player;
    public float SearchDistance = 10f;
    public float Speed = 2f;
    public int ArrivedThreshold = 3;

    private bool search = true;
    protected bool chasing = false;
    protected float tempDistance = 0F;
    protected Transform movementGoal;


    protected void Start () 
    {
        movementGoal = Player;
        //Make sure that we dont dividde by 0 in our search timer coroutine
        if (SearchPerSecond == 0)
            SearchPerSecond = 1;

        //We do not want a negative distance
        if (SearchDistance < 0)
            SearchDistance = 0;
	}
	
	protected void Update () 
    {
        //Make sure we set a player in the inspector!
        if (Player != null)
        {
            //save distance so we do not have to call it multiple times
            tempDistance = Vector3.Distance(transform.position, Player.position);

            //print("TempDistance: " + tempDistance + " Search Distance: " + SearchDistance);
            //Check if we are able to search
            if (search == true)
            {
                //print("Search: " + search.ToString());
                //Start the time
                StartCoroutine(SearchTimer());

                //Now check the distance to the player, if it is within the distance it will search for a new path
                if (tempDistance < SearchDistance)
                {
                    FindPath(transform.position, Player.position);
                    movementGoal = Player;
                    chasing = true;
                    //print("chasing");
                }
                else
                {
                    chasing = false;
                }
            }

            //Make sure that we actually got a path! then call the new movement method
            if (Path.Count > 0)
            {
                MoveAI();
            }
        }
        else
        {
            Debug.Log("No player set in the inspector!");
        }
	}

    IEnumerator SearchTimer()
    {
        //Set search to false for an amount of time, and then true again.
        search = false;
        yield return new WaitForSeconds(1 / SearchPerSecond);
        search = true;
    }

    protected void MoveAI()
    {
        //Make sure we are within distance + 1 added so we dont get stuck at exactly the search distance
        //if (tempDistance < SearchDistance + 1f)
        if (Path.Count > 0 && movementGoal != null)
        {       
            //if we get close enough or we are closer then the indexed position, then remove the position from our path list, 
            if (Vector3.Distance(transform.position, Path[0]) < 0.2F || tempDistance < Vector3.Distance(Path[0], movementGoal.position)) 
            {
                Path.RemoveAt(0);
            }

            if(Path.Count < ArrivedThreshold)
                return;

            //First we will create a new vector ignoreing the depth (z-axiz).
            Vector3 ignoreZ = new Vector3(Path[0].x, Path[0].y, transform.position.z);
            
            //now move towards the newly created position
            transform.position = Vector3.MoveTowards(transform.position, ignoreZ, Time.deltaTime * Speed);
           // print("moving");
        }
    }
}
