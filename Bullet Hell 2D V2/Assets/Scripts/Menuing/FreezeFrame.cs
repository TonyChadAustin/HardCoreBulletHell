using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{
    public static bool frozen = true;

    void Start()
    {
        frozen = true;
        gameObject.SetActive(true);

    }

    void Update()
    {
        if (frozen)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                frozen = false;
                gameObject.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            frozen = true;
            gameObject.SetActive(true);
        }
    }
}
