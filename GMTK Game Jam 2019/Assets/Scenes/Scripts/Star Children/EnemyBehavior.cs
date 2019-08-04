using UnityEngine;

public class EnemyBehavior : SimpleAI2D
{
    private bool isFiring = false;
    private GameObject wanderGoal;

    public float fireRange = 10f;
    public float wanderDistance = 5f;
    public float wanderArriveThreshold = 1f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if (Player == null)
        {
            Player = FindObjectOfType<PlayerMove>().gameObject.transform;
        }
        wanderGoal = new GameObject();
        wanderGoal.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (base.tempDistance <= fireRange)
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
            if (chasing == false)
            {
                Wander();
                //print("wander init");
            }
        }
    }

    public bool ShouldFire()
    {
        return isFiring;
    }
    void Wander()
    {
        if (Path.Count < 3 )
        {
            wanderGoal.transform.position = new Vector3(Random.Range(-wanderDistance, wanderDistance) + transform.position.x,
                Random.Range(-wanderDistance, wanderDistance) + transform.position.y,0f);
            //print("New WEandergoal");
        }
        movementGoal = wanderGoal.transform;
        //print(wanderGoal.transform.position.ToString());

        FindPath(transform.position, wanderGoal.transform.position);
        MoveAI();

    }
}
