using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida: MonoBehaviour
{
    private GameManager gm;
    private bool recogida = false;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Player") && !recogida)
        {
            recogida = true;
            gm.SumarVida();
            Destroy(gameObject);
        }
    }
}
