using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float speed;
    public FixedJoystick variableJoystick;

    float x, y;

    void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            print("Android");
            x = variableJoystick.Horizontal;
            y = variableJoystick.Vertical;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            print("Windows");
            x = Input.GetAxis("Horizontal");
            print(Input.GetAxis("Horizontal"));
            y = Input.GetAxis("Vertical");
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            print("Windows");
            x = Input.GetAxis("Horizontal");
            print(Input.GetAxis("Horizontal"));
            y = Input.GetAxis("Vertical");
        }

        Move(x);
    }

    private void Move(float x)
    {
        if (Mathf.Abs(x) > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(x * Time.deltaTime * speed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
