using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Colorwheel : MonoBehaviour
{
    public float rotationSpeed;

    Tilemap tiles;
    float hue;

    void Start()
    {
        tiles = GetComponent<Tilemap>();
    }

    void Update()
    {
        hue += rotationSpeed * Time.deltaTime;
        hue %= 360;
        Color color = Color.HSVToRGB(hue / 360f, 1f, 1f);
        tiles.color = color;
    }
}
