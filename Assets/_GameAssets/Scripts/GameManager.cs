using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*[SerializeField] int numeroCorazones;
    [SerializeField] int numeroCorazonesMax;
    [SerializeField] float vidaCorazon;
    [SerializeField] float vidaCorazonMax;*/
    [SerializeField] float vida;
    [SerializeField] float vidaMax;
    [SerializeField] float poder;
    [SerializeField] float poderMax;
    [SerializeField] int numeroVidas;
    [SerializeField] int numeroVidasMax;
    [SerializeField] int puntuacion;
    [SerializeField] int puntuacionMax;
    [SerializeField] int tiempo;
    [SerializeField] int tiempoMax;
    private UIManager ui;
    private const string PARAM_X = "x";
    private const string PARAM_Y = "y";
    private const string VIDAS = "VIDAS";
    private const string MEJOR_PUNTUACION_1_1 = "MEJOR_PUNTUACION_1_1";
    private bool checkpointActivo = false;

    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {        
        if (tiempo < 0)
        {
            StopCoroutine("CuentaAtras");
        }
        else
        {
            ui.ActualizarTiempo(FormatearTiempo(tiempo));
        }
    }

    /*public int GetNumeroCorazones()
    {
        return numeroCorazones;
    }
    */
    public bool QuitarVida(float dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            ui.ActualizarVida(0);
            RestarVida();
            return true;
        }
        else
        {
            ui.ActualizarVida(vida/vidaMax);
            return false;
        }
    }

    public void RestarVida()
    {
        numeroVidas--;

        if (numeroVidas < 0)
        {
            numeroVidas = numeroVidasMax;
            PlayerPrefs.DeleteKey(PARAM_X);
            PlayerPrefs.DeleteKey(PARAM_Y);
        }

        PlayerPrefs.SetInt(VIDAS, numeroVidas);
        PlayerPrefs.Save();
    }

    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;
        ui.ActualizarPuntuacion(puntuacion);
    }

    public bool HayAlmacenadaPosicionPlayer()
    {
        return PlayerPrefs.HasKey(PARAM_X);
    }

    public void GuardarPosicion(Vector2 position)
    {
        PlayerPrefs.SetFloat(PARAM_X, position.x);
        PlayerPrefs.SetFloat(PARAM_Y, position.y);
        PlayerPrefs.Save();
    }

    public Vector2 ObtenerPosicion(Vector2 posicionInicial)
    {
        float x = PlayerPrefs.GetFloat(PARAM_X, posicionInicial.x);
        float y = PlayerPrefs.GetFloat(PARAM_Y, posicionInicial.y);
        return new Vector2(x, y);
    }

    public void IniciarParametros()
    {
        /*numeroCorazones = numeroCorazonesMax;
        vidaCorazon = vidaCorazonMax;*/
        vida = vidaMax;
        poder = 0;
        numeroVidas = PlayerPrefs.GetInt(VIDAS, numeroVidasMax);
        puntuacion = 0;
        puntuacionMax = PlayerPrefs.GetInt(MEJOR_PUNTUACION_1_1, 0);
        tiempo = tiempoMax;        
        ui.ActualizarVida(vida/vidaMax);
        ui.ActualizarPoder(poder/poderMax);
        ui.ActualizarVidas(numeroVidas);
        ui.ActualizarTiempo(FormatearTiempo(tiempo));
        ui.ActualizarPuntuacion(puntuacion);
        ui.ActualizarPuntuacionMax(puntuacionMax);
        ui.FundirNegro(0, 2);
        StartCoroutine("CuentaAtras");
    }

    private string FormatearTiempo (int tiempo)
    {
        string stringTiempo;
        int minutos;
        int segundos;

        if (tiempo < 60)
        {
            stringTiempo = "0:" + tiempo.ToString("00");
        }
        else
        {
            minutos = tiempo / 60;
            segundos = tiempo - (minutos * 60);
            stringTiempo = minutos.ToString("00") + ":" + segundos.ToString("00");
        }

        return stringTiempo;
    }

    public void RecargarVida(float vidaRecarga)
    {
        vida = vida + vidaRecarga;

        if (vida > vidaMax)
        {
            vida = vidaMax;
        }

        ui.ActualizarVida(vida/vidaMax);
    }

    public void RecargarPoder(float poderRecarga)
    {
        poder = poder + poderRecarga;

        if (poder > poderMax)
        {
            poder = poderMax;
        }

        ui.ActualizarPoder(poder/poderMax);
    }

    public void ResetNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator CuentaAtras()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            tiempo--;
            if (tiempo == 0)
            {
                GameObject.Find("Yago").GetComponent<Player>().PerderVida(true);
            }
        }
    }
       
    public void SumarVida()
    {
        if (numeroVidas < numeroVidasMax)
        {
            numeroVidas++;
            ui.ActualizarVidas(numeroVidas);
        }
    }
}
