using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerui : MonoBehaviour
{
    public Text timerText;
    public float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!FreezeFrame.frozen)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");
        }
    }
}
