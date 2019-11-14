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
    [SerializeField] AudioClip[] audioClips;
    private UIManager ui;
    private bool checkpointActivo = false;
    private Player player;
    private AudioSource audioMusic;

    
    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        audioMusic = GetComponent<AudioSource>();
        player = GameObject.Find("Yago").GetComponent<Player>();
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
            return true;
        }
        else
        {
            ui.ActualizarVida(vida/vidaMax);
            return false;
        }
    }

    public bool RestarVida()
    {
        audioMusic.Stop();
        bool sinVidas = false;

        numeroVidas--;

        if (numeroVidas < 0)
        {
            Invoke("PlayGameOver", 0.4f);
            numeroVidas = numeroVidasMax;
            checkpointActivo = false;
            sinVidas = true;
            PlayerPrefs.DeleteKey(UIConfigManager.PARAM_X);
            PlayerPrefs.DeleteKey(UIConfigManager.PARAM_Y);
            PlayerPrefs.DeleteKey(UIConfigManager.CHECKPOINT_ACTIVO);
        }
        else
        {
            Invoke("PlayPerderVida", 0.4f);
        }

        PlayerPrefs.SetInt(UIConfigManager.VIDAS, numeroVidas);
        PlayerPrefs.Save();

        return sinVidas;
    }

    private void PlayPerderVida()
    {
        audioMusic.PlayOneShot(audioClips[2]);
    }

    private void PlayGameOver()
    {
        audioMusic.PlayOneShot(audioClips[3]);
    }

    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;
        ui.ActualizarPuntuacion(puntuacion);
    }

    public bool HayAlmacenadaPosicionPlayer()
    {
        return PlayerPrefs.HasKey(UIConfigManager.PARAM_X);
    }

    public bool ObtenerCheckpoint()
    {
        //checkpointActivo = PlayerPrefs.GetInt(CHECKPOINT_ACTIVO, 0) == 1 ? true : false;

        return checkpointActivo;
    }

    public void ActivarCheckpoint()
    {
        checkpointActivo = true;
        PlayerPrefs.SetInt(UIConfigManager.CHECKPOINT_ACTIVO, 1);
        PlayerPrefs.Save();
    }

    public void DesctivarCheckpoint()
    {
        checkpointActivo = false;
        PlayerPrefs.SetInt(UIConfigManager.CHECKPOINT_ACTIVO, 0);
        PlayerPrefs.Save();
    }

    public void GuardarPosicion(Vector2 position)
    {
        PlayerPrefs.SetFloat(UIConfigManager.PARAM_X, position.x);
        PlayerPrefs.SetFloat(UIConfigManager.PARAM_Y, position.y);
        PlayerPrefs.Save();
    }

    public Vector2 ObtenerPosicion(Vector2 posicionInicial)
    {
        float x = PlayerPrefs.GetFloat(UIConfigManager.PARAM_X, posicionInicial.x);
        float y = PlayerPrefs.GetFloat(UIConfigManager.PARAM_Y, posicionInicial.y);
        return new Vector2(x, y);
    }

    public void IniciarParametros()
    {
        /*numeroCorazones = numeroCorazonesMax;
        vidaCorazon = vidaCorazonMax;*/
        vida = vidaMax;
        poder = 0;
        numeroVidas = PlayerPrefs.GetInt(UIConfigManager.VIDAS, numeroVidasMax);
        puntuacion = 0;
        puntuacionMax = PlayerPrefs.GetInt(UIConfigManager.MEJOR_PUNTUACION_1_1, 0);
        tiempo = tiempoMax;
        audioMusic.volume = PlayerPrefs.GetFloat(UIConfigManager.VOLUMEN, 1);

        ui.ActualizarVida(vida / vidaMax);
        ui.ActualizarPoder(poder / poderMax);
        ui.ActualizarVidas(numeroVidas);
        ui.ActualizarTiempo(FormatearTiempo(tiempo));
        ui.ActualizarPuntuacion(puntuacion);
        ui.ActualizarPuntuacionMax(puntuacionMax);
        
        if (PlayerPrefs.GetInt(UIConfigManager.NIVEL_EMPEZADO, 0) == 0 && PlayerPrefs.GetInt(UIConfigManager.NIVEL_ACTUAL, 1) == 1)
        {
            Invoke("EmpezarCapituloYNivel", 1);
            Invoke("EmpezrCuentaAtras", 8);
            player.estadoPlayer = Player.EstadoPlayer.empezandoAJugar;
        }
        else if (PlayerPrefs.GetInt(UIConfigManager.NIVEL_EMPEZADO, 0) == 0)
        {
            Invoke("EmpezrNivel", 1);
            Invoke("EmpezrCuentaAtras", 3);
            player.estadoPlayer = Player.EstadoPlayer.empezandoAJugar;
        }
        else
        {
            EmpezrCuentaAtras();
            ui.FundirNegro(0, 1);
        }
    }

    private void EmpezrNivel()
    {
        string stage = "Stage " + PlayerPrefs.GetInt(UIConfigManager.NIVEL_ACTUAL, 1) + "-" + PlayerPrefs.GetInt(UIConfigManager.PANTALLA_ACTUAL, 1);
        ui.MostrarNivel(stage, "Start");
    }

    private void EmpezarCapituloYNivel()
    {
        audioMusic.PlayOneShot(audioClips[0]);
        string stage = "Stage " + PlayerPrefs.GetInt(UIConfigManager.NIVEL_ACTUAL, 1) + "-" + PlayerPrefs.GetInt(UIConfigManager.PANTALLA_ACTUAL, 1);
        ui.MostrarCapituloYNivel("Chapter 1", "Escape from the Volcano", stage, "Start");
    }

    private void EmpezrCuentaAtras()
    {
        if (PlayerPrefs.GetInt(UIConfigManager.CONTROLES_TACTILES, 1) == 1)
        {
            ui.MostrarControlesTactiles();
        }

        audioMusic.clip = audioClips[1];
        audioMusic.loop = true;
        audioMusic.Play();
        PlayerPrefs.SetInt(UIConfigManager.NIVEL_EMPEZADO, 1);
        PlayerPrefs.Save();
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

    public void SalirNivel()
    {
        SceneManager.LoadScene(0);
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
