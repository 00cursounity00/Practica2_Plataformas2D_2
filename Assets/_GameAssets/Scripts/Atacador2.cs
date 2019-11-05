using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador2 : MonoBehaviour
{
    [SerializeField] float dano;
    //[SerializeField] Transform puntoDisparo;
    //[SerializeField] GameObject prefabSlash;
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

    private IEnumerator Atacar()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            //Instantiate(prefabSlash, puntoDisparo.position, puntoDisparo.rotation);
            GetComponent<Animator>().SetTrigger("atacar");
            atacando = true;
        }

    }

    public void QuitarAtacando()
    {
        atacando = false;
    }
}
