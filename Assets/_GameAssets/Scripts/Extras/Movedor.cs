using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movedor : MonoBehaviour
{
    [SerializeField] Transform origen;
    [SerializeField] Transform destino;
    [SerializeField] float velocidad;
    [SerializeField] int direccion;
    private float porcentaje = 0;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        if (GetComponent<Atacador2>() == null || (GetComponent<Atacador2>() != null && GetComponent<Enemigo>().recibiendoDano == false))
        {
            porcentaje += Time.deltaTime * velocidad * direccion;
            transform.position = Vector2.Lerp(origen.position, destino.position, porcentaje);
            if (porcentaje >= 1 || porcentaje <= 0)
            {
                direccion *= -1;
                porcentaje = Mathf.Clamp(porcentaje, 0, 1f);

                if (!CompareTag("Plataforma"))
                {
                    sr.flipX = !sr.flipX;
                }
            }
        }
    }
}
