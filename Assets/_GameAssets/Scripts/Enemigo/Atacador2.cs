using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador2 : MonoBehaviour
{
    [SerializeField] float dano;
    [SerializeField] float tiempoEntreAtaques;

    private bool atacando = false;

    void Start()
    {
        StartCoroutine("Atacar");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (atacando)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().RecibirDano(dano);
            }
        }
    }

    public void QuitarAtacando()
    {
        atacando = false;
    }

    private IEnumerator Atacar()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreAtaques);
            GetComponent<Animator>().SetTrigger("atacar");
            atacando = true;
        }

    }    
}
