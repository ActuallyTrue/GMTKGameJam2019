using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthWidgetScript : MonoBehaviour
{
    private DamageAndHealth PlayerDmg;
    private Text HealthText;

    // Start is called before the first frame update
    void Start()
    {
        HealthText = GetComponent<Text>();
        GameObject player = GameObject.FindWithTag("Player");
        //Ideally, we would bind to a callback when damage is taken on the player.  But I might be too lazy to do that right now.
        PlayerDmg = player.GetComponent<DamageAndHealth>();
        if (PlayerDmg != null)
        {
            PlayerDmg.DamageEvent.AddListener(UpdateHealth);
            UpdateHealth();
        }
        
    }

// Update is called once per frame
void UpdateHealth()
    {
        HealthText.text = "♥ x " + PlayerDmg.GetHealth;
    }
}
