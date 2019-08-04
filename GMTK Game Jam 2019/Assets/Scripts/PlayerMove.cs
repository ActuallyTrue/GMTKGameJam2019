using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public bool InstantMotion = true;
    public float Speed = 5f;
    private Animator anim;
    private bool facingRight;

    Rigidbody2D rBody;
    // Start is called before the first frame update

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        anim.SetFloat("xVelocity", rBody.velocity.x);
        anim.SetFloat("yVelocity", rBody.velocity.y);

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
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (!Mathf.Approximately(input.x, 0) && !Mathf.Approximately(input.y, 0))
        {
            //rBody.velocity = new Vector3(0,0,0);
            rBody.velocity = new Vector2(input.x * Speed * 100 * Time.deltaTime, input.y * Speed * 100 * Time.deltaTime) * 0.707f;
        }

        else if (!Mathf.Approximately(input.x, 0) || !Mathf.Approximately(input.y, 0))
        {
            //rBody.velocity = new Vector3(0, 0, 0);
            rBody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * Speed * 100 * Time.deltaTime, Input.GetAxisRaw("Vertical") * Speed * 100 * Time.deltaTime);
        }
        else
        {
            rBody.velocity = new Vector3(0,0,0);
        }
    }

    private void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!Mathf.Approximately(input.x, 0) && !Mathf.Approximately(input.y, 0))
        {
            //rBody.velocity = new Vector3(0,0,0);
            rBody.velocity = new Vector2(input.x * Speed * 100 * Time.deltaTime, input.y * Speed * 100 * Time.deltaTime) * 0.707f;
        }

        else if (!Mathf.Approximately(input.x, 0) || !Mathf.Approximately(input.y, 0))
        {
            //rBody.velocity = new Vector3(0, 0, 0);
            rBody.velocity = new Vector2(input.x * Speed * 100 * Time.deltaTime, input.y * Speed * 100 * Time.deltaTime);
        }
        else
        {
            rBody.velocity = new Vector3(0, 0, 0);
        }
    }

}