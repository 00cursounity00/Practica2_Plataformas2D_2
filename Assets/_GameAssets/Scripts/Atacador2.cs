using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atacador2 : MonoBehaviour
{
    [SerializeField] Transform puntoDisparo;
    [SerializeField] GameObject prefabSlash;

    void Start()
    {
        StartCoroutine("Atacar");
    }
    
    private IEnumerator Atacar()
    {
        while (true)
        {
            Instantiate(prefabSlash, puntoDisparo.position, puntoDisparo.rotation);
            GetComponent<Animator>().SetTrigger("atacar");
            yield return new WaitForSeconds(3f);
        }

    }
}
