using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject panelCorazones;
    [SerializeField] GameObject prefabCorazon;
    private GameManager gm;
    private Text textoPuntuacion;
    private Text textoVidas;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        textoPuntuacion = GameObject.Find("TextoPuntuacion").GetComponent<Text>();
        textoVidas = GameObject.Find("TextoVidas").GetComponent<Text>();
        int numeroCorazonesActuales = gm.GetNumeroCorazones();

        for(int i = 0; i < numeroCorazonesActuales; i++)
        {
            Instantiate(prefabCorazon, panelCorazones.transform);
        }
    }

    public void ActualizarVidas(int numeroVidas)
    {
        textoVidas.text = numeroVidas.ToString();
    }

    public void ActualizarVida(int numeroCorazones, float vidaCorazon)
    {
        GameObject[] corazones = GameObject.FindGameObjectsWithTag("ContenedorSalud");
        GameObject ultimoCorazon = corazones[numeroCorazones - 1];
        Image imagenultimoCorazon = ultimoCorazon.GetComponent<Image>();
        imagenultimoCorazon.fillAmount = vidaCorazon;
        //GameObject.FindGameObjectsWithTag("ContenedorSalud")[numeroCorazones - 1].GetComponent<Image>().fillAmount = vidaCorazon;
    }

    public void ActualizarPuntuacion(int puntuacion)
    {
        textoPuntuacion.text = puntuacion.ToString();
    }

    public void ResetVida()
    {
        GameObject[] corazones = GameObject.FindGameObjectsWithTag("ContenedorSalud");
        foreach (GameObject corazon in corazones)
        {
            corazon.GetComponent<Image>().fillAmount = 1;
        }
    }
}
