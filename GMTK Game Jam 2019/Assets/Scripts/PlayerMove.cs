using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public bool InstantMotion = true;
    public float speed = 5f;

    Rigidbody2D rBody;
    // Start is called before the first frame update

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if(InstantMotion)
        {
            InstantMove();
        }
        else
        {
            Move();
        }
    }


    //checks input axis and sets a velocity for rBody
    private void InstantMove()
    {

        if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0) && !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0,0,0);
            rBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * 100 * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * 100 * Time.deltaTime) * 0.707f;
        }

        else if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), 0) || !Mathf.Approximately(Input.GetAxisRaw("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0, 0, 0);
            rBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * 100 * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * 100 * Time.deltaTime);
        }
        else
        {
            rBody.velocity = new Vector3(0,0,0);
        }
    }

    private void Move()
    {

        if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0) && !Mathf.Approximately(Input.GetAxis("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0,0,0);
            rBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * 100 * Time.deltaTime, Input.GetAxis("Vertical") * speed * 100 * Time.deltaTime) * 0.707f;
        }

        else if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0) || !Mathf.Approximately(Input.GetAxis("Vertical"), 0))
        {
            //rBody.velocity = new Vector3(0, 0, 0);
            rBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * 100 * Time.deltaTime, Input.GetAxis("Vertical") * speed * 100 * Time.deltaTime);
        }
        else
        {
            rBody.velocity = new Vector3(0, 0, 0);
        }
    }
}