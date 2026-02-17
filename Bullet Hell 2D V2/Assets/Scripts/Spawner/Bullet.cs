using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    float speed;
    bool despawn, aimAtPlayer;
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
        //transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.z - 90f);
    }

    void Update()
    {
        if (aimAtPlayer)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.Rotate(new Vector3(0, 0, -90));
            aimAtPlayer = false;
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("wall"))
        {
            Destroy(gameObject);
        }

        if (despawn && other.CompareTag("player"))
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void AtPlayer(bool aiming)
    {
       aimAtPlayer = aiming;
    }

    public void Pierce(bool piercing)
    {
        despawn = piercing;
    }

    public void Size(float size)
    {
        transform.localScale = new Vector3(size, size, transform.localScale.z);
    }
}
