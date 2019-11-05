using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : MonoBehaviour
{
    [SerializeField] int tipo;
    [SerializeField] float cantidad;
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
            if (tipo == 0)
            {
                gm.RecargarVida(cantidad);
            }
            else if (tipo == 1)
            {
                gm.RecargarPoder(cantidad);
            }
            Destroy(gameObject);
        }
    }
}
