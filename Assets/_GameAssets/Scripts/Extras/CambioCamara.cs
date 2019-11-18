using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCamera;
    [SerializeField] float nuevoScreenX, nuevoScreenY, velocidadCambio;

    private float screenX, screenY;
    private Transform playerTransform;
    private bool activado = false;
    private float porcentaje = 0;

    void Start()
    {
        screenX = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX;
        screenY = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (activado)
        {
            porcentaje += Time.deltaTime * velocidadCambio;
            virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX = Mathf.Lerp(screenX, nuevoScreenX, porcentaje);
            virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = Mathf.Lerp(screenY, nuevoScreenY, porcentaje);

            if (virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX >= nuevoScreenX)
            {
                activado = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !activado)
        {
            if (transform.position.x < playerTransform.position.x)
            {
                activado = true;
            }
        }
    }
}
