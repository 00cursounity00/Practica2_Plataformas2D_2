using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] int numeroBg;
    [SerializeField] float velocidadScrollX;
    [SerializeField] float velocidadScrollY;
    [SerializeField] float distanciaScrollTop;
    [SerializeField] float distanciaScrollBottom;
    [SerializeField] float distanciaSpawnRight;
    [SerializeField] float distanciaSpawnLeft;
    [SerializeField] float distanciaDestruccionRight;
    [SerializeField] float distanciaDestruccionLeft;
    [SerializeField] GameObject player;
    [SerializeField] string nombreBgBase, nombreBgRight, nombreBgLeft;
    private Transform playerTransform;
    private Rigidbody2D playerRb;
    private float x, y, factorX, factorY, difX, difY, posicionX, posicionY;

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();
        posicionX = playerTransform.position.x;
        posicionY = playerTransform.position.y;
        //print(posicionX + "  --  " + transform.position.x);
    }

    public void EstablecrBg (int numeroBgNuevo, string nombreBgBaseNuevo)
    {
        numeroBg = numeroBgNuevo;
        nombreBgBase = nombreBgBaseNuevo;

        if (numeroBg == 0)
        {
            nombreBgRight = nombreBgBase + "1";
            nombreBgLeft = nombreBgBase + "-1";
        }
        else if (numeroBg == 1)
        {
            nombreBgRight = nombreBgBase + (numeroBg + 1);
            nombreBgLeft = nombreBgBase;
        }
        else if (numeroBg == -1)
        {
            nombreBgRight = nombreBgBase;
            nombreBgLeft = nombreBgBase + (numeroBg - 1);
        }
        else
        {
            nombreBgRight = nombreBgBase + (numeroBg + 1);
            nombreBgLeft = nombreBgBase + (numeroBg - 1);
        }
    }

    void FixedUpdate()
    {
        x = playerRb.velocity.x;
        y = playerRb.velocity.y;

        //print(posicionX + "  --  " + playerTransform.position.x);

        if (Mathf.Abs(posicionX - playerTransform.position.x) > 0.01f || Mathf.Abs(posicionY - playerTransform.position.y) > 0.01f)
        {
            //if (Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0)
            //{
            if (Mathf.Abs(posicionX - playerTransform.position.x) > 0.01f)
            {
                factorX = (transform.position.x - transform.position.x + x) * Time.deltaTime * velocidadScrollX;
            }
            else
            {
                factorX = 0;
            }

            if (Mathf.Abs(posicionY - playerTransform.position.y) > 0.01f)
            {
                difY = transform.position.y - playerTransform.position.y;

                if (difY < distanciaScrollBottom && difY > -distanciaScrollTop)
                {
                    factorY = (transform.position.y - transform.position.y + y) * Time.deltaTime * velocidadScrollY;
                }
                else
                {
                    factorY = (transform.position.y - transform.position.y + y) * Time.deltaTime;
                }
            }
            else
            {
                factorY = 0;
            }

            transform.Translate(new Vector2(factorX, factorY));
            posicionX = playerTransform.position.x;
            posicionY = playerTransform.position.y;
            //}
        }

        difX = playerTransform.position.x - transform.position.x;
        //print("playerX: " + playerTransform.position.x + "  --  x:" + transform.position.x + "  --  Find:" + GameObject.Find(nombreBgLeft));
        if (difX > distanciaSpawnRight && GameObject.Find(nombreBgRight) == null)
        {
            GameObject go = Instantiate(gameObject, new Vector2(transform.position.x + 38.4f, transform.position.y), transform.rotation);
            go.name = nombreBgRight;
            go.GetComponent<Background>().EstablecrBg(numeroBg + 1, nombreBgBase);
        }
        else if (difX < -distanciaSpawnLeft && GameObject.Find(nombreBgLeft) == null)
        {
            GameObject go = Instantiate(gameObject, new Vector2(transform.position.x - 38.4f, transform.position.y), transform.rotation);
            go.name = nombreBgLeft;
            go.GetComponent<Background>().EstablecrBg(numeroBg - 1, nombreBgBase);
        }

        if (difX > distanciaDestruccionRight || difX < -distanciaDestruccionLeft)
        {
            Destroy(gameObject);
        }
    }
}
