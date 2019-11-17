using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDrop : MonoBehaviour
{
    [SerializeField] GameObject prefabProyectil;
    [SerializeField] Transform puntoDisparo;
    [SerializeField] float cadenciaMin = 0.15f;
    [SerializeField] float cadenciaMax = 0.4f;
    [SerializeField] bool random = false;

    private void Start()
    {
        if (random)
        {
            GetComponent<Animator>().speed = Random.Range(cadenciaMin, cadenciaMax);
        }
        else
        {
            GetComponent<Animator>().speed = cadenciaMin;
        }
    }

    private void LavaDroped()
    {
        Instantiate(prefabProyectil, puntoDisparo.position, puntoDisparo.rotation);

        if (random)
        {
            GetComponent<Animator>().speed = Random.Range(cadenciaMin, cadenciaMax);
        }
    }
}
