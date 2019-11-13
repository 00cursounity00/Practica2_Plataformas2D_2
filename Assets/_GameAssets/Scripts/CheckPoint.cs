using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.GuardarPosicion(transform.position);
            gm.ActivarCheckpoint();
            GetComponent<Animator>().SetBool("checkPoinActivado", true);
            Destroy(this);
        }
    }
}
