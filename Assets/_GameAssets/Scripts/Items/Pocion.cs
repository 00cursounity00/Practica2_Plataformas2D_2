using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : MonoBehaviour
{
    [SerializeField] int tipo;
    [SerializeField] float cantidad;
    [SerializeField] AudioClip sonido;

    private GameManager gm;
    private bool recogida = false;
    private AudioSource itemSounds;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        itemSounds = GameObject.Find("ItemSounds").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (collision.gameObject.CompareTag("Player") && !recogida)
        {
            recogida = true;
            itemSounds.volume = PlayerPrefs.GetFloat(UIConfigManager.VOLUMEN, 1);
            itemSounds.PlayOneShot(sonido);

            if (tipo == 0)
            {
                gm.RecargarVida(cantidad);
            }
            else if (tipo == 1)
            {
                gm.RecargarPoder(cantidad);
            }

            Destroy(gameObject);
        }
    }
}
