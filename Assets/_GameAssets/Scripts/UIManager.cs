using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    //[SerializeField] GameObject panelCorazones;
    //[SerializeField] GameObject prefabCorazon;
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
    [SerializeField] FixedJoystick fixedJoystick;
    [SerializeField]AudioClip[] audioClips;

    private GameManager gm;
    private Player player;
    private string tituloNivelString, subtituloNivelString;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Yago").GetComponent<Player>();
        //textoGameOver.gameObject.SetActive(true);
        //botonReintentar.gameObject.SetActive(true);
        //botonAtras.gameObject.SetActive(true);
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
        pantallaNegra.CrossFadeAlpha (alpha, tiempo, true);
    }

    public void MostrarGameOver()
    {
        botonSaltar.gameObject.SetActive(false);
        botonDisparar.gameObject.SetActive(false);
        fixedJoystick.gameObject.SetActive(false);
        textoPrincipal.gameObject.SetActive(true);
        textoPrincipal.text = "GAME OVER";
        botonReintentar.gameObject.SetActive(true);
        botonSalir.gameObject.SetActive(true);
        pantallaNegra.CrossFadeAlpha(1, 2, true);
        textoPrincipal.transform.DOScale(1, 2);
        Sequence s = DOTween.Sequence();
        s.Append(botonReintentar.transform.DOScale(1.2f, 1));
        s.Append(botonReintentar.transform.DOShakeScale(1));
        botonSalir.transform.DOScale(1, 2);
    }

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

    public void MostrarControlesTactiles()
    {
        botonSaltar.gameObject.SetActive(true);
        botonDisparar.gameObject.SetActive(true);
        fixedJoystick.gameObject.SetActive(true);
    }
}
