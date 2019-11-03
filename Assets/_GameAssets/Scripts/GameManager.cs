using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /*[SerializeField] int numeroCorazones;
    [SerializeField] int numeroCorazonesMax;
    [SerializeField] float vidaCorazon;
    [SerializeField] float vidaCorazonMax;*/
    [SerializeField] float vida;
    [SerializeField] float vidaMax;
    [SerializeField] float poder;
    [SerializeField] float podernMax;
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
        IniciarParametros();

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

    private void RestarVida()
    {
        numeroVidas--;
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

    public void ActivarCheckpoint(Vector2 position)
    {
        PlayerPrefs.SetInt(VIDAS, numeroVidas);
        PlayerPrefs.SetFloat(PARAM_X, position.x);
        PlayerPrefs.SetFloat(PARAM_Y, position.y);
        PlayerPrefs.Save();
    }

    public Vector2 ObtenerCheckpoint(Vector2 posicionInicial)
    {
        numeroVidas = PlayerPrefs.GetInt(VIDAS, numeroVidasMax);
        ui.ActualizarVidas(numeroVidas);
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
        ui.ActualizarPoder(poder/podernMax);
        ui.ActualizarVidas(numeroVidas);
        ui.ActualizarTiempo(FormatearTiempo(tiempo));
        ui.ActualizarPuntuacion(puntuacion);
        ui.ActualizarPuntuacionMax(puntuacionMax);
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
}
