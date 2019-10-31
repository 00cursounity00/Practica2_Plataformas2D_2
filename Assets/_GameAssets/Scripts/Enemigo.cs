using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    [SerializeField] Slider barraVida;
    [SerializeField] float vidaMax;
    private float vida;

    private void Start()
    {
        vida = vidaMax;
    }

    public void RecibirDano(float dano)
    {
        vida -= dano;
        barraVida.value = (vida / vidaMax);

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
