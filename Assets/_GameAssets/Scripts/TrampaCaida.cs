﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaCaida : MonoBehaviour
{
    private bool activada;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activada = true;
            collision.gameObject.GetComponent<Player>().PerderVida(false);
        }
    }
}
