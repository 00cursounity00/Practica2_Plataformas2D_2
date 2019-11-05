using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] Slider barraVida;
    [SerializeField] Image fillVida;
    [SerializeField] float vidaMax;
    [SerializeField] GameObject explosion;
    private float vida;
    private int parpadeos;
    public bool recibiendoDano;

    private void Start()
    {
        vida = vidaMax;
    }

    public void RecibirDano(float dano)
    {
        if (!recibiendoDano)
        {
            vida -= dano;
            barraVida.value = (vida / vidaMax);

            if (vida / vidaMax <= 0.5f)
            {
                fillVida.color = new Color(1, 0, 0);
            }

            if (vida <= 0)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                recibiendoDano = true;
                InvokeRepeating("Parpadeo", 0, 0.2f);
            }
        }
    }

    private void Parpadeo()
    {
        if (parpadeos < 5)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            parpadeos++;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            parpadeos = 0;
            recibiendoDano = false;
            CancelInvoke();
        }
    }
}
