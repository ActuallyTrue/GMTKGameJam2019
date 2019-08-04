using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityModulater : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool increasing = false;

    public float opacityMin, opacityMax, speed = 1.0f;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.Log("Sprite Renderer not found by OpacityModulater");
        }
    }

    // Update is called once per frame
    void Update()
    {

        //print(spriteRenderer.color.a);

        if (increasing)
        {

            if (spriteRenderer.color.a <= opacityMax)
            {
                spriteRenderer.color = new Color(1f,1f,1f,spriteRenderer.color.a + ( speed * Time.deltaTime));
            }
            else
            {
                increasing = false;
            }
        }
        else
        {

            if (spriteRenderer.color.a >= opacityMin)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, spriteRenderer.color.a - (speed * Time.deltaTime));
            }
            else
            {
                increasing = true;
            }
        }
    }
}
