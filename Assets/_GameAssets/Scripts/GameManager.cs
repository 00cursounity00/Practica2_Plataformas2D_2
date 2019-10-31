using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] int numeroCorazones;
    [SerializeField] int numeroCorazonesMax;
    [SerializeField] float vidaCorazon;
    [SerializeField] float vidaCorazonMax;
    [SerializeField] int numeroVidas;
    [SerializeField] int numeroVidasMax;
    [SerializeField] int puntuacion;
    private UIManager ui;
    private const string PARAM_X = "x";
    private const string PARAM_Y = "y";
    private const string VIDAS = "VIDAS";
    private bool checkpointActivo = false;

    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    public int GetNumeroCorazones()
    {
        return numeroCorazones;
    }

    public bool QuitarVida(float dano)
    {
        float resto = vidaCorazon - dano;
        vidaCorazon = resto;

        if (vidaCorazon <= 0)
        {
            ui.ActualizarVida(numeroCorazones, 0);
            numeroCorazones--;
            vidaCorazon = vidaCorazonMax;

            if (numeroCorazones == 0)
            {
                RestarVida();
                return true;
            }
        }

        if (resto < 0)
        {
            QuitarVida(resto * -1);
        }

        ui.ActualizarVida(numeroCorazones, vidaCorazon);
        return false;
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

    public void ResetGame()
    {
        numeroCorazones = numeroCorazonesMax;
        vidaCorazon = vidaCorazonMax;
        ui.ResetVida();
        ui.ActualizarVidas(numeroVidas);
    }
}
