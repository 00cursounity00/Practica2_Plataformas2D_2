using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida: MonoBehaviour
{
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
            itemSounds.volume = PlayerPrefs.GetFloat(UIConfigManager.VOLUMEN, 1);
            itemSounds.PlayOneShot(sonido);
            recogida = true;
            gm.SumarVida();
            Destroy(gameObject);
        }
    }
}
