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
    [SerializeField] Button guardarBoton;

    public const string SONIDO = "SONIDO";
    public const string VOLUMEN = "VOLUMEN";
    public const string CONTROLES_TACTILES = "CONTROLES_TACTILES";
    public const string MEJOR_PUNTUACION_1_1 = "MEJOR_PUNTUACION_1_1";
    public const string CHECKPOINT_ACTIVO = "CHECKPOINT_ACTIVO";
    public const string NIVEL_EMPEZADO = "NIVEL_EMPEZADO";
    public const string NIVEL_ACTUAL = "NIVEL_ACTUAL";
    public const string PANTALLA_ACTUAL = "PANTALLA_ACTUAL";
    public const string PARAM_X = "x";
    public const string PARAM_Y = "y";
    public const string VIDAS = "VIDAS";

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

    public void Saltar()
    {
        guardarBoton.transform.DOScale(1.1f,0.3f).SetLoops(-1, LoopType.Yoyo);
    }
}
