using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador3 : MonoBehaviour
{
    [SerializeField] float fuerza;
    [SerializeField] float dano;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Atacar(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Atacar(collision);
    }

    private void Atacar(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().ObtenerEstado() == Player.EstadoPlayer.normal)
        {
            if (collision.gameObject.transform.rotation.y == 0)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1.5f) * fuerza;
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1.5f) * fuerza;
            }
            collision.gameObject.GetComponent<Player>().RecibirDano(dano);
        }
    }
}
