using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffects : MonoBehaviour
{
    public enum Effect {HeartBeat, GrowShrink, Spin };
    public SpriteRenderer renderer;
    public Effect effect = Effect.GrowShrink;

    private bool shrinking = true;
    private Vector3 startingScale;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<SpriteRenderer>().Equals(null))
        {
            renderer = gameObject.GetComponent<SpriteRenderer>();
        }
        startingScale = transform.localScale;

        switch (effect)
        {
            case Effect.HeartBeat:
                StartCoroutine(HeartBeat());
                break;
            case Effect.GrowShrink:
                StartCoroutine(GrowShrink());
                break;
            case Effect.Spin:
                StartCoroutine(Spin());
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator HeartBeat()
    {
        while(true)
        {
            if (shrinking)
            {
                transform.localScale = startingScale * .75f;
                shrinking = false;
            }
            else
            {
                transform.localScale = startingScale;
                shrinking = true;
            }

            Debug.Log(shrinking);
            yield return new WaitForSeconds(.5f);
        }
    }

    private IEnumerator GrowShrink()
    {
        while(true)
        {
            Debug.Log(transform.localScale.magnitude + " : " + startingScale.magnitude);
            if (shrinking)
            {
                transform.localScale = transform.localScale * .95f;
            }
            else if (!shrinking)
            {
                transform.localScale = transform.localScale * 1.05f;
                shrinking = false;
            }
            
            if(transform.localScale.magnitude >= startingScale.magnitude)
            {
                shrinking = true;
            }
            else if(transform.localScale.magnitude <= startingScale.magnitude * .40f)
            {
                shrinking = false;
            }

            yield return new WaitForSeconds(.01f);
        }
    }

    private IEnumerator Spin()
    {
        while(true)
        {
            transform.Rotate(new Vector3(Time.deltaTime * 0, 0, 5));

            yield return new WaitForSeconds(0.05f);
        }
    }
}
