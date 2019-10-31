using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [SerializeField] float dano;
    [SerializeField] float tiempoDestruccion;
    private int direccion;

    private void Start()
    {
        Destroy(this.gameObject, tiempoDestruccion);
        if (GameObject.FindGameObjectsWithTag("Player")[0].transform.position.x > 0)
        {
            direccion = -1;
        }
        else
        {
            direccion = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemigo>() != null)
        {
            collision.gameObject.GetComponent<Enemigo>().RecibirDano(dano);
        }
        Destroy(gameObject);
    }
    /*
    private void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * angularSpeed * direccion);
    }*/
}
