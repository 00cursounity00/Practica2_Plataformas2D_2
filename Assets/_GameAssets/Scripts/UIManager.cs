using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    //[SerializeField] GameObject panelCorazones;
    //[SerializeField] GameObject prefabCorazon;
    private GameManager gm;
    [SerializeField] Text textoPuntuacion;
    [SerializeField] Text textoPuntuacionMax;
    [SerializeField] Text textoVidas;
    [SerializeField] Text textoTiempo;
    [SerializeField] Slider sliderVida;
    [SerializeField] Slider sliderPoder;
    [SerializeField] Image pantallanegra;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //textoPuntuacion = GameObject.Find("TextoPuntuacionActual").GetComponent<Text>();
        //textoPuntuacionMax = GameObject.Find("TextoMejorPuntuacionActual").GetComponent<Text>();
        //textoVidas = GameObject.Find("TextoVidas").GetComponent<Text>();
        //textoTiempo = GameObject.Find("TextoTiempoRestante").GetComponent<Text>();
        //sliderVida = GameObject.Find("SliderVidaPlayer").GetComponent<Slider>();
        //sliderPoder = GameObject.Find("SliderPoder").GetComponent<Slider>();
        /*int numeroCorazonesActuales = gm.GetNumeroCorazones();

        for(int i = 0; i < numeroCorazonesActuales; i++)
        {
            Instantiate(prefabCorazon, panelCorazones.transform);
        }*/
    }

    public void ActualizarVidas(int numeroVidas)
    {
        textoVidas.text = numeroVidas.ToString();
    }

    public void ActualizarVida(float vida)
    {
        //GameObject[] corazones = GameObject.FindGameObjectsWithTag("ContenedorSalud");
        //GameObject ultimoCorazon = corazones[numeroCorazones - 1];
        //Image imagenultimoCorazon = ultimoCorazon.GetComponent<Image>();
        //imagenultimoCorazon.fillAmount = vidaCorazon;
        //GameObject.FindGameObjectsWithTag("ContenedorSalud")[numeroCorazones - 1].GetComponent<Image>().fillAmount = vidaCorazon;

        sliderVida.value = vida;
        if (vida <= 0.5f)
        {
            GameObject.Find("FillVidaPlayer").GetComponent<Image>().color = new Color(1, 0, 0);
        }
        else
        {
            GameObject.Find("FillVidaPlayer").GetComponent<Image>().color = new Color(0, 1, 0);
        }
    }

    public void ActualizarPoder(float poder)
    {        
        sliderPoder.value = poder;
    }

    public void ActualizarTiempo(string tiempo)
    {
        textoTiempo.text = tiempo;
    }

    public void ActualizarPuntuacion(int puntuacion)
    {
        textoPuntuacion.text = puntuacion.ToString();
    }

    public void ActualizarPuntuacionMax(int puntuacionMax)
    {
        textoPuntuacionMax.text = puntuacionMax.ToString();
    }

    /*public void ResetVida()
    {
        GameObject[] corazones = GameObject.FindGameObjectsWithTag("ContenedorSalud");
        foreach (GameObject corazon in corazones)
        {
            corazon.GetComponent<Image>().fillAmount = 1;
        }
    }*/

    public void FundirNegro(float alpha, float tiempo)
    {
        pantallanegra.CrossFadeAlpha (alpha, tiempo, true);
    }
}
