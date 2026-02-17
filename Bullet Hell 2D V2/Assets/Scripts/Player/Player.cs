using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Specialized;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class Player : MonoBehaviour
{
    hitreg checkup, checkleft, checkdown, checkright;
    public hitreg[] hitregs;

    public float speed, size, rotation, timerFinish;

    bool up, left, down, right, angled, blocku, blockl, blockd, blockr, shifting, timer;
    float xMath, yMath, radianConverter, tempspeed;

    int tempcount = 0;

    void Start()
    {
        if (size > 0)
        {
            transform.localScale = new Vector3(size, size, transform.localScale.z);
        }
        if (timerFinish > 0)
        {
            timer = true;
        }
        hitregs = GetComponentsInChildren<hitreg>();
        foreach (hitreg hit in hitregs)
        {
            if (tempcount == 0)
            {
                checkup = hit;
            }
            else if (tempcount == 1)
            {
                checkleft = hit;
            }
            else if (tempcount == 2)
            {
                checkdown = hit;
            }
            else
            {
                checkright = hit;
            }
            tempcount += 1;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
        if (!FreezeFrame.frozen)
        {
            if (timerFinish > 0)
            {
                timerFinish -= Time.deltaTime;
            }
            else
            {
                if (timer)
                {
                    SceneManager.LoadScene("Level Select");
                }
            }
            CheckRotation();
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -rotation);
            up = false;
            left = false;
            down = false;
            right = false;
            if (Input.GetKey(KeyCode.W))
            {
                up = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                left = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                down = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                right = true;
            }

            if ((up && right) || (up && left) || (down && right) || (down && left))
            {
                if (!angled)
                {
                    speed = (speed / 1.414f);
                    angled = true;
                }
            }
            else
            {
                if (angled)
                {
                    speed = (speed * 1.414f);
                    angled = false;
                }
            }
            if (up && !checkup.wall)
            {
                MathFinder();
                transform.position += new Vector3(xMath, yMath) * Time.deltaTime;
            }
            if (left && !checkleft.wall)
            {
                rotation += 270;
                MathFinder();
                rotation -= 270;
                transform.position += new Vector3(xMath, yMath) * Time.deltaTime;
            }
            if (down && !checkdown.wall)
            {
                rotation += 180;
                MathFinder();
                rotation -= 180;
                transform.position += new Vector3(xMath, yMath) * Time.deltaTime;
            }
            if (right && !checkright.wall)
            {
                rotation += 90;
                MathFinder();
                rotation -= 90;
                transform.position += new Vector3(xMath, yMath) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Q))
            {
                rotation -= Time.deltaTime * 80;
                CheckRotation();
            }

            if (Input.GetKey(KeyCode.E))
            {
                rotation += Time.deltaTime * 80;
                CheckRotation();
            }

            if (Input.GetKey(KeyCode.Z))
            {
                rotation = 0;
            }


            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (!shifting)
                {
                    speed /= 2;
                    shifting = true;
                }
            }
            else if (shifting)
            {
                speed *= 2;
                shifting = false;
            }

            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public void MathFinder()
    {
        radianConverter = (float)((rotation * (Math.PI)) / 180);
        xMath = (float)(Math.Sin(radianConverter)) * speed;
        yMath = (float)(Math.Cos(radianConverter)) * speed;
    }

    public void CheckRotation()
    {
        if (rotation >= 360)
        {
            rotation -= 360;
        }
        if (rotation <= 0)
        {
            rotation += 360;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "finish")
        {
            if (SceneManager.GetActiveScene().buildIndex == 6)
            {
                SceneManager.LoadScene(0);
            }
            SceneManager.LoadScene("Level Select");
        }
    }
}
