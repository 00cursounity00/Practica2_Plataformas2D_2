using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] float dano;
    //[SerializeField] float alto;
    //[SerializeField] float ancho;
    [SerializeField] string tipoDano;
    [SerializeField] float fuerza;

    private void Start()
    {
        //Destroy(gameObject, 20f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name + "   --   " + tipoDano);
        if (collision.gameObject.GetComponent<Enemigo>() != null && tipoDano == "enemigo")
        {
            collision.gameObject.GetComponent<Enemigo>().RecibirDano(dano);
        }
        else if (collision.gameObject.GetComponent<Player>() != null && tipoDano == "player")
        {
            if (collision.gameObject.transform.position.x > transform.position.x)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1.5f) * fuerza;
                if (collision.gameObject.transform.rotation.y == 0)
                {
                    collision.gameObject.transform.Rotate(new Vector3(0, 180, 0));
                }
            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1.5f) * fuerza;
                if (collision.gameObject.transform.rotation.y == 180)
                {
                    collision.gameObject.transform.Rotate(new Vector3(0, 180, 0));
                }
            }

            collision.gameObject.GetComponent<Player>().RecibirDano(dano);
        }
        
        //Destroy(gameObject);
    }

    /*
    private void Start()
    {
          Invoke("HacerSlash", 0.05f);
    }

    private void HacerSlash()
    {
        print("Llega");
        Collider2D[] cds = Physics2D.OverlapCapsuleAll(transform.position, new Vector2(alto, ancho), CapsuleDirection2D.Vertical, 90);
        foreach (Collider2D cd in cds)
        {
            print(cd.name);
            if (cd.gameObject.GetComponent<Enemigo>() != null && tipoDano == "enemigo")
            {
                cd.gameObject.GetComponent<Enemigo>().RecibirDano(dano);
            }
            else if (cd.gameObject.GetComponent<Player>() != null && tipoDano == "player")
            {
                if (cd.gameObject.transform.position.x > transform.position.x)
                {
                    cd.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1.5f) * fuerza;
                    if (cd.gameObject.transform.rotation.y == 0)
                    {
                        cd.gameObject.transform.Rotate(new Vector3(0, 180, 0));
                    }
                }
                else
                {
                    cd.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1.5f) * fuerza;
                    if (cd.gameObject.transform.rotation.y == 180)
                    {
                        cd.gameObject.transform.Rotate(new Vector3(0, 180, 0));
                    }
                }
                cd.gameObject.GetComponent<Player>().RecibirDano(dano);
            }
        }
        //Destroy(gameObject);
    }*/
}
