using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movedor2 : MonoBehaviour
{
    [SerializeField] Transform origen;
    [SerializeField] Transform destino1;
    [SerializeField] Transform destino2;
    [SerializeField] float velocidad;
    [SerializeField] float distanciaDespertar;
    [SerializeField] int direccion;
    private float porcentaje = 0;
    private SpriteRenderer sr;
    private bool despertado = false;
    private bool primeraVez = true;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        Collider2D[] cds = Physics2D.OverlapCircleAll(transform.position, distanciaDespertar);
        foreach (Collider2D cd in cds)
        {
            if (cd.gameObject.GetComponent<Player>() != null)
            {
                GetComponent<Animator>().SetTrigger("despertar");
                despertado = true;
            }
        }

        if (despertado)
        {
            porcentaje += Time.deltaTime * velocidad * direccion;
            transform.position = Vector2.Lerp(origen.position, destino1.position, porcentaje);
            if (porcentaje >= 1 || porcentaje <= 0)
            {
                direccion *= -1;
                //transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * direccion, transform.localScale.y);
                //transform.localScale = new Vector2(direccion, 1); Solo si la escala es 1/1/1
                porcentaje = Mathf.Clamp(porcentaje, 0, 1f);
                sr.flipX = !sr.flipX;

                if (primeraVez)
                {
                    origen = destino2;
                    //destino1 = destino2;
                    primeraVez = false;
                }
            }
        }
    }
}
