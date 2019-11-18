using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPrincipal : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void EmpezrJuego()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void Salir()
    {
        print("okfngklfxcnvbkkhnxflc");
        Application.Quit();
    }
}
