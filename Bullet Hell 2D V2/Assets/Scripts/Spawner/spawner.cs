using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject bullet;
    public float timer, initTimer, bulletSpeed, bulletSize, endTimer, numBullets, arcGap, rotatingccw, burst, burstDelay, randSpeed1, randSpeed2, randTime1, randTime2;
    public bool aiming;
    float countdown, rotation, burstCur;

    List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        burstCur = burst;
        if (initTimer == 0)
        {
            initTimer = 0.01f;
            countdown = UnityEngine.Random.Range(randTime1, randTime2);
        }
        //if (randTime1 > 0)
        //{
            //countdown = UnityEngine.Random.Range(randTime1, randTime2);
        //}
        else
        {
            countdown = initTimer;
        }
        if (endTimer == 0)
        {
            endTimer = 999999;
        }
        if (burst <= 1)
        {
            burst = 1;
        }
        rotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        if (!FreezeFrame.frozen)
        {
            if (initTimer <= 0)
            {
                rotation += rotatingccw * Time.deltaTime;
            }
            else
            {
                initTimer -= Time.deltaTime;
            }
            if (rotation > 360)
            {
                rotation = 0;
            }
            if (rotation < 0)
            {
                rotation = 360;
            }
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotation);
            if (endTimer > 0)
            {
                endTimer -= Time.deltaTime;
                if (countdown > 0)
                {
                    countdown -= 1 * Time.deltaTime;
                }
                else
                {
                    if (burstCur > 1)
                    {
                        burstCur -= 1;
                        shoot();
                        countdown = burstDelay;
                    }
                    else
                    {
                        burstCur -= 1;
                        shoot();
                        if (burst > 1)
                        {
                            burstCur = burst;
                        }
                        else
                        {
                            shoot();
                        }
                        if (randTime1 > 0)
                        {
                            countdown = UnityEngine.Random.Range(randTime1, randTime2);
                        }
                        else
                        {
                            countdown = timer;
                        }
                    }
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void shoot()
    {
        for (int i = 1; i < numBullets + 1; i++)
        {
            GameObject temp = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (arcGap * i)));
            if (randSpeed1 > 0)
            {
                temp.GetComponent<Bullet>().SetSpeed((float)(UnityEngine.Random.Range(randSpeed1, randSpeed2)));

            }
            else
            {
                temp.GetComponent<Bullet>().SetSpeed((float)(bulletSpeed));
            }
            if (bulletSize > 0)
            {
                temp.GetComponent<Bullet>().Size((float)(bulletSize));
            }
            if (aiming)
            {
                temp.GetComponent<Bullet>().AtPlayer(true);
            }
            bullets.Add(temp);
        }
    }
}
