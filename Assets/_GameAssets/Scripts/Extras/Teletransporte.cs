using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletransporte : MonoBehaviour
{
    [SerializeField] Transform destinoTransform;

    private bool teletransportandose = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            teletransportandose = true;
            collision.gameObject.GetComponent<Player>().Teletransportar();
            Destroy(gameObject);
        }
    }
}
