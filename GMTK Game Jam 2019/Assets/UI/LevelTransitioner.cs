using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitioner : MonoBehaviour
{
    public string destinationLevel;

    //void OnCollisionEnter2D(Collider2D col)
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "PlayerBullet")
        {
            Debug.Log("Load Scene: " + destinationLevel);
            SceneManager.LoadScene(destinationLevel);
        }
    }

}
