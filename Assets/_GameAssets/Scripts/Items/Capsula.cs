using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsula : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] GameObject explosion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            Instantiate(item, transform.position, transform.rotation);
            GameObject go = Instantiate(explosion, transform.position, transform.rotation);
            go.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(UIConfigManager.VOLUMEN, 1);
            Destroy(gameObject);
        }
    }
}
