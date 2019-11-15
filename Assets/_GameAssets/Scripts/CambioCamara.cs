using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambioCamara : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera virtualCamera;
    [SerializeField] float nuevoScreenX, nuevoScreenY, nuevoBiasX, nuevoBiasY, nuevoSoftZoneHeight;
    private float screenX, screenY, biasX, biasY, softZoneHeight;
    private Transform playerTransform;

    void Start()
    {
        screenX = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX;
        screenY = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY;
        biasX = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_BiasX;
        biasY = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_BiasY;
        softZoneHeight = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_SoftZoneHeight;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Yago")
        {
            if (transform.position.x < playerTransform.position.x)
            {
                virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX = nuevoScreenX;
                virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = nuevoScreenY;
                //virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_BiasX = nuevoBiasX;
                //virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_BiasY = nuevoBiasY;
                //virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_SoftZoneHeight = nuevoSoftZoneHeight;
            }
            else
            {
                virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenX = screenX;
                virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_ScreenY = screenY;
                //virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_BiasX = biasX;
                //virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_BiasX = biasY;
                //virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineFramingTransposer>().m_SoftZoneHeight = softZoneHeight;
            }
        }
    }
}
