using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveWidgetScript : MonoBehaviour
{
    public GameObject GameManager;  //ideally there would be a better way to find this, maybe a tag, rather than setting it per-level
    public int TotalWaves = 10; //ideally this would be gotten from a game-wide progress manager, rather than hardcoded in each level

    private int currentWave = 0;
    private WaveSpawner waveSpawner;
    private Text waveText;

    // Start is called before the first frame update
    void Start()
    {
        waveText = GetComponent<Text>();
        if (GameManager != null)
        {
            waveSpawner = GameManager.GetComponent<WaveSpawner>();
            if (waveSpawner != null)
            {
                waveSpawner.WaveEvent.AddListener(UpdateWaveText);
                UpdateWaveText();
            }
        }
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = waveSpawner.GetWaveName() + " / " + TotalWaves.ToString();
        }
    }

}
