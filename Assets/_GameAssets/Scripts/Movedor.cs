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
                //transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direccion, transform.localScale.y);
                //transform.localScale = new Vector2(direccion, 1); Solo si la escala es 1/1/1
                porcentaje = Mathf.Clamp(porcentaje, 0, 1f);
                if (!CompareTag("PlataformaMovil"))
                {
                    sr.flipX = !sr.flipX;
                }
            }
        }
    }
}
