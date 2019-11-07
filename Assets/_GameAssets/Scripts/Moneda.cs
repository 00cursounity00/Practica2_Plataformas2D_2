using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] int puntos;
    private GameManager gm;
    private bool recogido = false;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !recogido)
        {
            recogido = true;
            gm.SumarPuntos(puntos);
            Destroy(gameObject);
        }
    }
}
