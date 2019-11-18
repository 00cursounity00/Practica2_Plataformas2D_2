using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] int puntos;
    [SerializeField] AudioClip sonido;

    private GameManager gm;
    private bool recogido = false;
    private AudioSource itemSounds;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        itemSounds = GameObject.Find("ItemSounds").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !recogido)
        {
            recogido = true;
            itemSounds.volume = PlayerPrefs.GetFloat(UIConfigManager.VOLUMEN, 1);
            itemSounds.PlayOneShot(sonido);
            gm.SumarPuntos(puntos);
            Destroy(gameObject);
        }
    }
}
