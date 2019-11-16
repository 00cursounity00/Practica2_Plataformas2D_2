using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDrop : MonoBehaviour
{
    [SerializeField] GameObject prefabProyectil;
    [SerializeField] Transform puntoDisparo;
    
    private void LavaDroped()
    {
        Instantiate(prefabProyectil, puntoDisparo.position, puntoDisparo.rotation);
    }
}
