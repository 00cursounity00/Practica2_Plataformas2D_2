using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIConfigManager : MonoBehaviour
{
    [SerializeField] Toggle sonido;
    [SerializeField] Slider volumenSlider;
    [SerializeField] Text VolumenTexto;
    [SerializeField] Button guardarBoton;

    private void Start()
    {
        sonido.isOn = PlayerPrefs.GetInt("sound", 1) == 1 ? true : false;
        //sonido.isOn = ((PlayerPrefs.GetInt("sound", 1) == 1);
        volumenSlider.value = PlayerPrefs.GetFloat("volume", 1);
        VolumenTexto.text = ("Volmen " + ((int)(volumenSlider.value * 100)) + "%");

    }

    public void Guardar()
    {
        int sound = sonido.isOn ? 1 : 0;
        float volume = volumenSlider.value;
        PlayerPrefs.SetInt("sound", sound);
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void Cancelar()
    {

    }

    public void Saltar()
    {
        guardarBoton.transform.DOScale(1.1f,0.3f).SetLoops(-1, LoopType.Yoyo);
    }
}
