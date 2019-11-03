using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : MonoBehaviour
{
    [SerializeField] int tipo;
    [SerializeField] float cantidad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (tipo == 0)
            {
                //collision.gameObject.GetComponent<Player>().RecargarVida(cantidad);
            }
            else
            {
                //collision.gameObject.GetComponent<Player>().RecargarPoder(cantidad);
            }
        }
    }
}
