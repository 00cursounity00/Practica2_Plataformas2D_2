using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropedLava : MonoBehaviour
{
    [SerializeField] float dano;
    [SerializeField] float fuerza;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

        GetComponent<Animator>().SetTrigger("split");
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }
}
