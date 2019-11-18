using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] Text textoPuntuacion;
    [SerializeField] Text textoPuntuacionMax;
    [SerializeField] Text textoVidas;
    [SerializeField] Text textoTiempo;
    [SerializeField] Text textoPrincipal;
    [SerializeField] Text textoSecundario;
    [SerializeField] Slider sliderVida;
    [SerializeField] Slider sliderPoder;
    [SerializeField] Image pantallaNegra;
    [SerializeField] Image imagenCapitulo;
    [SerializeField] Image imagenYago;
    [SerializeField] Button botonReintentar;
    [SerializeField] Button botonSalir;
    [SerializeField] Button botonSaltar;
    [SerializeField] Button botonDisparar;
    [SerializeField] Button botonPoder;
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField] AudioClip[] audioClips;

    private GameManager gm;
    private Player player;
    private string tituloNivelString, subtituloNivelString;
    private int puntuacionActual;
    private int vidasActuales;
    private int tiempoActual;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Yago").GetComponent<Player>();
    }

    // ACTUALIZACION UI

    public void ActualizarVidas(int numeroVidas)
    {
        textoVidas.text = numeroVidas.ToString();
    }

    public void ActualizarVida(float vida)
    {
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

    public void MostrarControlesTactiles()
    {
        botonPoder.gameObject.SetActive(true);
        botonSaltar.gameObject.SetActive(true);
        botonDisparar.gameObject.SetActive(true);
        fixedJoystick.gameObject.SetActive(true);
    }

    public void FundirNegro(float alpha, float tiempo)
    {
        pantallaNegra.CrossFadeAlpha(alpha, tiempo, true);
    }

    // COMIENZO CAPITULO

    public void MostrarCapituloYNivel (string tituloCapitulo, string subtituloCapitulo, string tituloNivel, string subtituloNivel)
    {
        imagenYago.gameObject.SetActive(true);
        imagenCapitulo.gameObject.SetActive(true);
        textoPrincipal.gameObject.SetActive(true);
        textoPrincipal.text = tituloCapitulo;
        textoSecundario.gameObject.SetActive(true);
        textoSecundario.text = subtituloCapitulo;
        textoPrincipal.transform.position = new Vector2(textoPrincipal.transform.position.x, (textoPrincipal.transform.position.y + 500));
        textoPrincipal.transform.localScale = new Vector3(1, 1, 1);
        textoSecundario.transform.position = new Vector2((textoSecundario.transform.position.x + 300), (textoSecundario.transform.position.y - 500));
        textoSecundario.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        imagenYago.transform.position = new Vector2((imagenYago.transform.position.x - 800), imagenYago.transform.position.y);
        Sequence s = DOTween.Sequence();
        s.Append(textoPrincipal.transform.DOMoveY((textoPrincipal.transform.position.y - 500), 1));
        s.Append(textoSecundario.transform.DOMoveY((textoSecundario.transform.position.y + 500), 0));
        s.Append(textoSecundario.transform.DOScale(1, 1));
        s.Append(imagenYago.transform.DOMoveX((imagenYago.transform.position.x + 800), 1));
        tituloNivelString = tituloNivel;
        subtituloNivelString = subtituloNivel;
        Invoke("QuitarMostrarCapitulo", 4.5f);
        Invoke("MostrarNivel2", 5);
    }

    private void MostrarNivel2 ()
    {
        MostrarNivel(tituloNivelString, subtituloNivelString);
    }

    public void QuitarMostrarCapitulo ()
    {
        imagenYago.gameObject.SetActive(false);
        imagenCapitulo.gameObject.SetActive(false);
        textoPrincipal.gameObject.SetActive(false);
        textoSecundario.transform.position = new Vector2((textoSecundario.transform.position.x - 300), textoSecundario.transform.position.y);
        textoSecundario.gameObject.SetActive(false);
    }

    // COMIENZO NIVEL

    public void MostrarNivel(string tituloNivel, string subtituloNivel)
    {
        textoPrincipal.gameObject.SetActive(true);
        textoPrincipal.text = tituloNivel;
        textoSecundario.gameObject.SetActive(true);
        textoSecundario.text = subtituloNivel;
        textoPrincipal.transform.position = new Vector2(textoPrincipal.transform.position.x, (textoPrincipal.transform.position.y + 500));
        textoPrincipal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        textoSecundario.transform.position = new Vector2(textoSecundario.transform.position.x, (textoSecundario.transform.position.y - 500));
        textoSecundario.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        imagenYago.transform.position = new Vector2((imagenYago.transform.position.x - 800), imagenYago.transform.position.y);
        Sequence s = DOTween.Sequence();
        s.Append(textoPrincipal.transform.DOMoveY((textoPrincipal.transform.position.y - 500), 0));
        s.Append(textoPrincipal.transform.DOScale(1, 1.5f));
        s.Append(textoSecundario.transform.DOMoveY((textoSecundario.transform.position.y + 500), 0));
        s.Append(textoSecundario.transform.DOScale(1, 0.5f));
        Invoke("QuitarMostrarNivel", 2);
    }

    public void QuitarMostrarNivel()
    {
        textoPrincipal.gameObject.SetActive(false);
        textoSecundario.gameObject.SetActive(false);
        FundirNegro(0, 0.5f);
        player.estadoPlayer = Player.EstadoPlayer.normal;
    }

    // GAME OVER

    public void MostrarGameOver()
    {
        botonSaltar.gameObject.SetActive(false);
        botonDisparar.gameObject.SetActive(false);
        botonPoder.gameObject.SetActive(false);
        fixedJoystick.gameObject.SetActive(false);
        pantallaNegra.CrossFadeAlpha(1, 1, true);
        Invoke("TerminarMostrarGameOver", 1);
    }

    public void TerminarMostrarGameOver()
    {
        textoPrincipal.gameObject.SetActive(true);
        textoPrincipal.text = "GAME OVER";
        botonReintentar.gameObject.SetActive(true);
        botonSalir.gameObject.SetActive(true);
        textoPrincipal.transform.DOScale(1, 0.5f);
        Sequence s = DOTween.Sequence();
        s.Append(botonReintentar.transform.DOScale(1, 1));
        s.Append(botonReintentar.transform.DOShakeScale(1));
        botonSalir.transform.DOScale(1, 2);
    }

    //NIVEL COMPLETADO

    public void MostrarNivelCompletado(int puntuacion, int vidas, int tiempo)
    {
        botonSaltar.gameObject.SetActive(false);
        botonDisparar.gameObject.SetActive(false);
        botonPoder.gameObject.SetActive(false);
        fixedJoystick.gameObject.SetActive(false);
        pantallaNegra.CrossFadeAlpha(1, 1, true);
        Invoke("TerminarMostrarNivelCompletado", 1);
    }

    public void TerminarMostrarNivelCompletado()
    {
        textoPrincipal.gameObject.SetActive(true);
        textoPrincipal.text = "STAGE CLEAR";
        botonReintentar.gameObject.SetActive(true);
        botonSalir.gameObject.SetActive(true);
        textoPrincipal.transform.DOScale(1, 0.5f);
        Sequence s = DOTween.Sequence();
        s.Append(botonReintentar.transform.DOScale(1, 1));
        s.Append(botonReintentar.transform.DOShakeScale(1));
        botonSalir.transform.DOScale(1, 2);
    }

    // EFECTOS BOTONES

    public void AgrandarReintentar()
    {
        botonReintentar.transform.DOScale(1.2f, 0);
    }

    public void AgrandarSalir()
    {
        botonSalir.transform.DOScale(1.2f, 0);
    }

    public void EncogerReintentar()
    {
        botonReintentar.transform.DOScale(1, 0);
    }

    public void EncogerSalir()
    {
        botonSalir.transform.DOScale(1, 0);
    }
}
