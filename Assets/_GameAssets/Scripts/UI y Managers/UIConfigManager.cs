using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIConfigManager : MonoBehaviour
{
    [SerializeField] Toggle sonido;
    [SerializeField] Toggle controlesTactiles;
    [SerializeField] Slider volumenSlider;
    [SerializeField] Text VolumenTexto;
    [SerializeField] Button jugarBoton;
    [SerializeField] Button opcionesBoton;
    [SerializeField] Button salirBoton;
    [SerializeField] Button guardarBoton;
    [SerializeField] Button cancelarBoton;

    public const string SONIDO = "SONIDO";
    public const string VOLUMEN = "VOLUMEN";
    public const string CONTROLES_TACTILES = "CONTROLES_TACTILES";
    public const string MEJOR_PUNTUACION_1 = "MEJOR_PUNTUACION_1";
    public const string MEJOR_PUNTUACION_2 = "MEJOR_PUNTUACION_2";
    public const string MEJOR_PUNTUACION_3 = "MEJOR_PUNTUACION_3";
    public const string MEJOR_PUNTUACION_4 = "MEJOR_PUNTUACION_4";
    public const string MEJOR_PUNTUACION_5 = "MEJOR_PUNTUACION_5";
    public const string MEJOR_PUNTUACION_6 = "MEJOR_PUNTUACION_6";
    public const string MEJOR_PUNTUACION_7 = "MEJOR_PUNTUACION_7";
    public const string MEJOR_PUNTUACION_8 = "MEJOR_PUNTUACION_8";
    public const string MEJOR_PUNTUACION_9 = "MEJOR_PUNTUACION_9";
    public const string MEJOR_PUNTUACION_10 = "MEJOR_PUNTUACION_10";
    public const string CHECKPOINT_ACTIVO = "CHECKPOINT_ACTIVO";
    public const string PARAM_X = "x";
    public const string PARAM_Y = "y";
    public const string NIVEL_EMPEZADO = "NIVEL_EMPEZADO";
    public const string NIVEL_ACTUAL = "NIVEL_ACTUAL";
    public const string PANTALLA_ACTUAL = "PANTALLA_ACTUAL";
    public const string VIDAS = "VIDAS";
    public const string PODER = "PODER";

    private void Start()
    {
        CargarOpciones();
    }

    private void CargarOpciones()
    {
        sonido.isOn = PlayerPrefs.GetInt(SONIDO, 1) == 1 ? true : false;
        controlesTactiles.isOn = PlayerPrefs.GetInt(CONTROLES_TACTILES, 1) == 1 ? true : false;
        volumenSlider.value = PlayerPrefs.GetFloat(VOLUMEN, 1);
        VolumenTexto.text = ("Volume " + ((int)(volumenSlider.value * 100)) + "%");
        PlayerPrefs.DeleteKey(CHECKPOINT_ACTIVO);
        PlayerPrefs.DeleteKey(NIVEL_EMPEZADO);
        PlayerPrefs.DeleteKey(NIVEL_ACTUAL);
        PlayerPrefs.DeleteKey(PANTALLA_ACTUAL);
        PlayerPrefs.DeleteKey(PARAM_X);
        PlayerPrefs.DeleteKey(PARAM_Y);
        PlayerPrefs.DeleteKey(VIDAS);
        PlayerPrefs.DeleteKey(PODER);
        PlayerPrefs.Save();
    }

    public void Guardar()
    {
        int sound = sonido.isOn ? 1 : 0;
        int touchControls = controlesTactiles.isOn ? 1 : 0;
        float volume = volumenSlider.value;
        PlayerPrefs.SetInt(SONIDO, sound);
        PlayerPrefs.SetInt(CONTROLES_TACTILES, touchControls);
        PlayerPrefs.SetFloat(VOLUMEN, volume);
        PlayerPrefs.Save();
    }

    public void Cancelar()
    {
        CargarOpciones();
    }

    // EFECTOS BOTONES

    public void AgrandarJugar()
    {
        jugarBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarOpciones()
    {
        opcionesBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarSalir()
    {
        salirBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarGuardar()
    {
        guardarBoton.transform.DOScale(1.2f, 0);
    }

    public void AgrandarCancelar()
    {
        cancelarBoton.transform.DOScale(1.2f, 0);
    }

    public void EncogerJugar()
    {
        jugarBoton.transform.DOScale(1, 0);
    }

    public void EncogerOpciones()
    {
        opcionesBoton.transform.DOScale(1, 0);
    }

    public void EncogerSalir()
    {
        salirBoton.transform.DOScale(1, 0);
    }

    public void EncogerGuardar()
    {
        guardarBoton.transform.DOScale(1, 0);
    }

    public void EncogerCancelar()
    {
        cancelarBoton.transform.DOScale(1, 0);
    }
}
