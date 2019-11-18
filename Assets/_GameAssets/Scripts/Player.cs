using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public bool tieneCadencia = false;

    [SerializeField] float velocidad;
    [SerializeField] float fuerzaDisparo;
    [SerializeField] float cadenciaDisparo;
    [SerializeField] float fuerzaSalto;
    [SerializeField] GameObject prefabProyectil;
    [SerializeField] GameObject prefabProyectil2;
    [SerializeField] GameObject explosionPlayer;
    [SerializeField] GameObject efectoPoder;
    [SerializeField] GameObject fogonazo;
    [SerializeField] Transform puntoDisparoSuelo;
    [SerializeField] Transform puntoDisparoAire;
    [SerializeField] Transform detectorSuelo;
    [SerializeField] LayerMask layerSuelo;
    [SerializeField] PhysicsMaterial2D pm2d;
    [SerializeField] FixedJoystick variableJoystick;

    private AudioSource[] audios;
    private AudioSource audioMusic;
    private float x, y, xAxis, yAxis, xJoystick, yJoystick;
    private Rigidbody2D rb;
    private GameManager gm;
    private Animator animator;
    private bool enSuelo = false;
    public enum EstadoPlayer {normal, inmune, paralizado};
    public EstadoPlayer estadoPlayer = EstadoPlayer.normal;
    private Vector2 posicionInicial;
    private int parpadeos = 0;
    private UIManager ui;
    private float gravedad;
    private Transform destinoTeletransporte;
    private bool poderActivado = false;

    private const int AUDIO_DISPARO = 0;
    private const int AUDIO_SALTO = 1;
    private const int AUDIO_EXPLOSION = 2;
    private const int AUDIO_DANO = 3;
    private const int AUDIO_DISPARO_2 = 4;
    private const int AUDIO_PODER = 5;
    
    void Start()
    {
        posicionInicial = GameObject.Find("PosicionInicialPlayer").transform.position;
        //posicionInicial = transform.position;
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        audios = GetComponents<AudioSource>();
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();
        audioMusic = GameObject.Find("GameManager").GetComponent<AudioSource>();
        IniciarJuego();
    }

    void Update()
    {
        ObtenerEnSuelo();

        if (Input.GetButtonDown("Fire1"))
        {
            Disparar();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Saltar();
        }

        if (Input.GetButtonDown("Fire3"))
        {
            ActivarPoder();
        }
    }

    void FixedUpdate()
    {
        xAxis = variableJoystick.Horizontal;
        yAxis = variableJoystick.Vertical;
        
        xJoystick = Input.GetAxis("Horizontal");
        yJoystick = Input.GetAxis("Vertical");

        if (Mathf.Abs(xAxis) > Mathf.Abs(xJoystick))
        {
            x = xAxis;
        }
        else
        {
            x = xJoystick;
        }

        if (Mathf.Abs(yAxis) > Mathf.Abs(yJoystick))
        {
            y = yAxis;
        }
        else
        {
            y = yJoystick;
        }

        if (x > 0.1f)
        {
            x = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (x < -0.1f)
        {
            x = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        Mover(x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            transform.SetParent(null);
        }
    }

    // MOVER

    private void Mover(float velocidadX)
    {
        if (estadoPlayer != EstadoPlayer.paralizado && rb != null && !animator.GetBool("recibiendoDano"))
        {
            if (Mathf.Abs(velocidadX) > 0.1f)
            {
                animator.SetBool("corriendo", true);
                rb.velocity = new Vector2(velocidadX * velocidad, rb.velocity.y);
            }
            else
            {
                animator.SetBool("corriendo", false);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    // DISPARAR

    public void Disparar()
    {
        if (estadoPlayer != EstadoPlayer.paralizado && tieneCadencia == false && !animator.GetBool("recibiendoDano"))
        {
            tieneCadencia = true;
            Invoke("QuitarCadencia", cadenciaDisparo);
            animator.SetBool("disparando", true);
            Invoke("QuitarDisparar", 0.1f);
            GameObject prefab;

            if (!poderActivado)
            {
                prefab = prefabProyectil;
                audios[AUDIO_DISPARO].Play();
            }
            else
            {
                prefab = prefabProyectil2;
                audios[AUDIO_DISPARO_2].Play();
            }

            if (animator.GetBool("enSuelo"))
            {
                GameObject proyectil = Instantiate(prefab, puntoDisparoSuelo.position, puntoDisparoSuelo.rotation);
                proyectil.GetComponent<Rigidbody2D>().AddForce(puntoDisparoSuelo.right * fuerzaDisparo);
            }
            else
            {
                GameObject proyectil = Instantiate(prefab, puntoDisparoAire.position, puntoDisparoAire.rotation);
                proyectil.GetComponent<Rigidbody2D>().AddForce(puntoDisparoAire.right * fuerzaDisparo);
            }
        }
    }

    private void QuitarCadencia()
    {
        tieneCadencia = false;
    }

    private void QuitarCadencia2()
    {
        Invoke("QuitarCadencia", cadenciaDisparo);
    }

    private void QuitarDisparar()
    {
        animator.SetBool("disparando", false);
    }

    // SALTAR

    public void Saltar()
    {
        if (estadoPlayer != EstadoPlayer.paralizado && rb != null && !animator.GetBool("recibiendoDano") && (ObtenerEnSuelo() || ObtenerEnAgua()))
        {
            audios[AUDIO_SALTO].Play();
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            animator.SetBool("saltando", true);
        }
    }

    private void QuitarSaltar()
    {
        animator.SetBool("saltando", false);
    }

    // PODER

    public void ActivarPoder()
    {
        if (estadoPlayer != EstadoPlayer.paralizado && estadoPlayer != EstadoPlayer.inmune && gm.ObtenerPoder() > 0 && !animator.GetBool("recibiendoDano"))
        {
            Sequence s = DOTween.Sequence();
            s.Append(GetComponent<SpriteRenderer>().DOColor(new Color(1, 0.8f, 0), 0.5f));
            s.Append(GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1), 0.5f));
            s.SetLoops(-1);
            efectoPoder.SetActive(true);
            fogonazo.SetActive(true);
            fogonazo.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.2f);
            Invoke("QuitarFogonazo",0.2f);
            estadoPlayer = EstadoPlayer.inmune;
            poderActivado = true;
            audios[AUDIO_PODER].Play();
            gm.ActivarPoder();
        }
    }

    public void QuitarFogonazo()
    {
        fogonazo.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0);
        fogonazo.SetActive(false);
    }

    public void CancelarPoder()
    {
        efectoPoder.SetActive(false);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        estadoPlayer = EstadoPlayer.normal;
        poderActivado = false;
        DOTween.KillAll();
    }

    // DANO

    public void RecibirDano(float dano)
    {
        if (estadoPlayer == EstadoPlayer.normal)
        {
            audios[AUDIO_DANO].Play();
            if (gm.QuitarVida(dano))
            {
                PerderVida(true);
            }
            else
            {
                animator.SetBool("recibiendoDano", true);
            }

            estadoPlayer = EstadoPlayer.inmune;
            tieneCadencia = false;
            animator.SetBool("disparando", false);
        }
    }

    private void QuitarRecibirDano()
    {
        InvokeRepeating("Parpadeo", 0, 0.2f);
        animator.SetBool("recibiendoDano", false);
    }

    private void QuitarEstadoRecibiendoDano()
    {
        estadoPlayer = EstadoPlayer.normal;
    }

    // TELETRANSPORTE

    public void Teletransportar()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
        {
            cc.enabled = false;
        }

        estadoPlayer = EstadoPlayer.paralizado;
        animator.SetBool("corriendo", false);
        ui.FundirNegro(1, 1.5f);
        CancelInvoke();
        GetComponent<SpriteRenderer>().enabled = true;
        Invoke("TerminarTeletransportar", 1.5f);
    }

    public void TerminarTeletransportar()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        rb.isKinematic = false;

        foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
        {
            cc.enabled = true;
        }

        estadoPlayer = EstadoPlayer.normal;
        transform.position = GameObject.Find("DestinoTeletransporte").transform.position;
        ui.FundirNegro(0, 1.5f);
    }

    // VIDA

    public void PerderVida(bool conExplosion)
    {
        CancelInvoke("Parpadeo");
        efectoPoder.SetActive(false);
        GetComponent<SpriteRenderer>().enabled = false;

        if (conExplosion)
        {
            audios[AUDIO_EXPLOSION].Play();
            Instantiate(explosionPlayer, transform.position, transform.rotation);
        }

        Destroy(rb);

        foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
        {
            cc.enabled = false;
        }

        if (gm.RestarVida())
        {
            Invoke("MostrarGameOver", 2);
        }
        else
        {
            Invoke("MostrarPantallaNegra", 2);
        }
    }

    // OBTENCION ESTADOS

    private bool ObtenerEnAgua()
    {
        return animator.GetBool("enAgua");
    }

    private bool ObtenerEnSuelo()
    {
        Collider2D cd = Physics2D.OverlapBox(detectorSuelo.position, new Vector2(0.8f, 0.2f), 0, layerSuelo);

        if (cd != null)
        {
            GetComponent<CapsuleCollider2D>().sharedMaterial = null;
            animator.SetBool("enSuelo", true);
            return true;
        }

        GetComponent<CapsuleCollider2D>().sharedMaterial = pm2d;
        animator.SetBool("enSuelo", false);
        return false;
    }

    public bool ObtenerPoderActivado()
    {
        return poderActivado;
    }

    public EstadoPlayer ObtenerEstado()
    {
        return estadoPlayer;
    }

    // ACCIONES NIVEL
    
    private void IniciarJuego()
    {
        rb.velocity = Vector2.zero;

        foreach (AudioSource audiosSource in audios)
        {
            audiosSource.volume = PlayerPrefs.GetFloat(UIConfigManager.VOLUMEN, 1); ;
        }

        transform.position = gm.ObtenerPosicion(posicionInicial);
        GameObject.Find("ParallaxBackground").transform.position = transform.position;
        gm.IniciarParametros();
    }

    private void ReiniciarJuego()
    {
        gm.ResetNivel();
    }

    public void TerminarNivel()
    {
        CancelInvoke("Parpadeo");
        GetComponent<SpriteRenderer>().enabled = true;
        Destroy(rb);

        foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
        {
            cc.enabled = false;
        }

        gm.TerminarNivel();
        estadoPlayer = EstadoPlayer.paralizado;
    }

    private void MostrarGameOver()
    {
        ui.MostrarGameOver();
    }

    private void MostrarPantallaNegra()
    {
        ui.FundirNegro(1, 1);
        Invoke("ReiniciarJuego", 1);
    }

    // EXTRAS

    private void Parpadeo()
    {
        if (parpadeos < 8)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            parpadeos++;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            parpadeos = 0;
            estadoPlayer = EstadoPlayer.normal;
            CancelInvoke("Parpadeo");
        }
    }
}
