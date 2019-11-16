﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float fuerzaDisparo;
    [SerializeField] float cadenciaDisparo;
    [SerializeField] float fuerzaSalto;
    [SerializeField] GameObject prefabProyectil;
    [SerializeField] GameObject prefabProyectil2;
    [SerializeField] GameObject explosionPlayer;
    [SerializeField] GameObject efectoPoder;
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
    public enum EstadoPlayer {normal, inmune, teletransportandose, empezandoAJugar};
    public EstadoPlayer estadoPlayer = EstadoPlayer.normal;
    private Vector2 posicionInicial;
    private int parpadeos = 0;
    public bool tieneCadencia = false;
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


    //Metodos MonoBehaviour

    void Start()
    {
        //posicionInicial = GameObject.Find("PosicionInicialPlayer").transform.position;
        posicionInicial = transform.position;
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
            //gravedad = rb.gravityScale;
            //rb.gravityScale = 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            //rb.gravityScale = gravedad;
            transform.SetParent(null);
        }
    }


    //Metodos Player

    public void Disparar()
    {
        if (estadoPlayer != EstadoPlayer.teletransportandose && estadoPlayer != EstadoPlayer.empezandoAJugar && tieneCadencia == false && !animator.GetBool("recibiendoDano"))
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
                //print("suelo");
                GameObject proyectil = Instantiate(prefab, puntoDisparoSuelo.position, puntoDisparoSuelo.rotation);
                proyectil.GetComponent<Rigidbody2D>().AddForce(puntoDisparoSuelo.right * fuerzaDisparo);
            }
            else
            {
                //print("aire");
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

    public void Saltar()
    {
        if (estadoPlayer != EstadoPlayer.teletransportandose && estadoPlayer != EstadoPlayer.empezandoAJugar && rb != null && !animator.GetBool("recibiendoDano") && (ObtenerEnSuelo() || ObtenerEnAgua()))
        {
            audios[AUDIO_SALTO].Play();
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
            animator.SetBool("saltando", true);
            //Invoke("QuitarSaltar", 0.1f);
            //rb.AddForce(new Vector2(0, 1) * fuerzaSalto);
        }
    }

    private void QuitarSaltar()
    {
        animator.SetBool("saltando", false);
    }

    private bool ObtenerEnAgua()
    {
        return animator.GetBool("enAgua");
    }

    private bool ObtenerEnSuelo()
    {
        Collider2D cd = Physics2D.OverlapBox(detectorSuelo.position, new Vector2(0.8f, 0.2f), 0, layerSuelo);

        if (cd != null)
        {
            /*foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
            {
                cc.sharedMaterial = null;
            }*/
            //GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().sharedMaterial = null;
            animator.SetBool("enSuelo", true);
            return true;
        }

        /*foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
        {
            cc.sharedMaterial = pm2d;
        }*/

        //GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<CapsuleCollider2D>().sharedMaterial = pm2d;
        animator.SetBool("enSuelo", false);
        return false;
    }

    public void ActivarPoder()
    {
        if (estadoPlayer != EstadoPlayer.teletransportandose && estadoPlayer != EstadoPlayer.empezandoAJugar && estadoPlayer != EstadoPlayer.inmune && gm.ObtenerPoder() > 0 && !animator.GetBool("recibiendoDano"))
        {
            Sequence s = DOTween.Sequence();
            s.Append(GetComponent<SpriteRenderer>().DOColor(new Color(1, 0.8f, 0), 0.5f));
            s.Append(GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1), 0.5f));
            s.SetLoops(-1);
            efectoPoder.SetActive(true);
            estadoPlayer = EstadoPlayer.inmune;
            poderActivado = true;
            audios[AUDIO_PODER].Play();
            gm.ActivarPoder();
            //Invoke("CancelarPoder", gm.ObtenerPoder());
        }
    }

    public void CancelarPoder()
    {
        efectoPoder.SetActive(false);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
        estadoPlayer = EstadoPlayer.normal;
        poderActivado = false;
        DOTween.KillAll();
    }

    /*public void AumentarTiempoPoder()
    {
        CancelInvoke("CancelarPoder");
        Invoke("CancelarPoder", gm.ObtenerPoder());
    }*/

    private void Mover(float velocidadX)
    {
        if (estadoPlayer != EstadoPlayer.teletransportandose && estadoPlayer != EstadoPlayer.empezandoAJugar && rb != null && !animator.GetBool("recibiendoDano"))
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
                //rb.velocity = new Vector2(0, 0);
            }
        }
    }

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
                //Invoke("QuitarRecibirDano", 0.5f);
            }
            estadoPlayer = EstadoPlayer.inmune;
            tieneCadencia = false;
            animator.SetBool("disparando", false);
        }
    }

    private void QuitarRecibirDano()
    {
        //Invoke("QuitarEstadoRecibiendoDano", 1.6f);
        InvokeRepeating("Parpadeo", 0, 0.2f);
        animator.SetBool("recibiendoDano", false);
    }

    private void QuitarEstadoRecibiendoDano()
    {
        estadoPlayer = EstadoPlayer.normal;
    }

    public void Teletransportar()
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
        {
            cc.enabled = false;
        }

        estadoPlayer = EstadoPlayer.teletransportandose;
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
        //rb.isKinematic = true;
        //rb.velocity = Vector2.zero;
        Destroy(rb);

        foreach (CapsuleCollider2D cc in GetComponents<CapsuleCollider2D>())
        {
            cc.enabled = false;
        }

        if (gm.RestarVida())
        {
            //Time.timeScale = 0;
            Invoke("MostrarGameOver", 2);
        }
        else
        {
            Invoke("MostrarPantallaNegra", 2);
        }
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

    private void ReiniciarJuego()
    {
        gm.ResetNivel();
    }

    private void Parpadeo ()
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

    private void IniciarJuego()
    {
        rb.velocity = Vector2.zero;

        foreach (AudioSource audiosSource in audios)
        {
            audiosSource.volume = PlayerPrefs.GetFloat(UIConfigManager.VOLUMEN, 1); ;
        }

        if (gm.ObtenerCheckpoint())
        {
            transform.position = gm.ObtenerPosicion(posicionInicial);
        }
        else
        {
            transform.position = posicionInicial;
        }

        GameObject.Find("ParallaxBackground").transform.position = transform.position;

        gm.IniciarParametros();
    }

    public EstadoPlayer ObtenerEstado()
    {
        return estadoPlayer;
    }

    public bool ObtenerPoderActivado()
    {
        return poderActivado;
    }
}
