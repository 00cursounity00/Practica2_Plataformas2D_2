using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float dano;
    [SerializeField] float fuerza;
    private Animator animator;
    private bool bombaActiva = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !bombaActiva)
        {
            animator.SetTrigger("activarBomba");
            bombaActiva = true;
        }
    }

    public void Explotar()
    {
        Instantiate (explosion, transform.position, transform.rotation);
        Collider2D[] cds = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D cd in cds)
        {
            if (cd.gameObject.GetComponent<Player>() != null)
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
        Destroy(gameObject);
    }
}
