using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador : MonoBehaviour
{
    [SerializeField] float fuerza;
    [SerializeField] float dano;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Atacar(collision);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        Atacar(collision);
    }

    private void Atacar(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Player>().ObtenerEstado() == Player.EstadoPlayer.normal)
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1.5f) * fuerza;
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1.5f) * fuerza;
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            collision.gameObject.GetComponent<Player>().RecibirDano(dano);
        }
    }
}
