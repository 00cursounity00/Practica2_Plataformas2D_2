using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPrincipal : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Nivel1");
    }
}
