using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float tamanoBackground;
    public float viewZone;
    public float velocidadParallax;
    public bool scrolling, parallax;

    private Transform camaraTransform;
    private Transform[] backgrounds;
    private int rightIndex, leftIndex;
    private float ultimaCamaraX;


    // Start is called before the first frame update
    void Start()
    {
        camaraTransform = Camera.main.transform;
        ultimaCamaraX = camaraTransform.position.x;
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = backgrounds.Length - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (parallax)
        {
            float diferenciaX = camaraTransform.position.x - ultimaCamaraX;
            transform.position += Vector3.right * diferenciaX * velocidadParallax;
        }

        ultimaCamaraX = camaraTransform.position.x;

        if (scrolling)
        {
            if (camaraTransform.position.x < (backgrounds[leftIndex].position.x + viewZone))
            {
                ScrollLeft();
            }
            else if (camaraTransform.position.x > (backgrounds[rightIndex].position.x - viewZone))
            {
                ScrollRight();
            }
        }

    }

    private void ScrollLeft()
    {
        float lastY = backgrounds[rightIndex].position.y;
        backgrounds[rightIndex].position = Vector3.right * (backgrounds[leftIndex].position.x - tamanoBackground);
        backgrounds[rightIndex].position = new Vector2 (backgrounds[rightIndex].position.x, lastY);
        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
        {
            rightIndex = backgrounds.Length - 1;
        }
    }

    private void ScrollRight()
    {
        float lastY = backgrounds[rightIndex].position.y;
        backgrounds[leftIndex].position = Vector3.right * (backgrounds[rightIndex].position.x + tamanoBackground);
        backgrounds[leftIndex].position = new Vector2(backgrounds[rightIndex].position.x, lastY);
        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == backgrounds.Length)
        {
            leftIndex = 0;
        }
    }
}
