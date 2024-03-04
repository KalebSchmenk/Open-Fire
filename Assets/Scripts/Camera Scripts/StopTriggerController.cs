using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopTriggerController : MonoBehaviour
{
    public float slowDownIn = 3f;

    public UnityEvent OnStopEvent;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        if (!obj)
        {
            Debug.LogWarning("Null obj");
            return;
        }

        if (obj.CompareTag("Cart"))
        {
            CinemachineDollyCart cart = obj.gameObject.GetComponent<CinemachineDollyCart>();

            StartCoroutine(SlowToZero(cart, Time.time));
        }
    }

    private IEnumerator SlowToZero(CinemachineDollyCart cart, float initTime)
    {
        while (true)
        {
            float t = (Time.time - initTime) / slowDownIn;

            cart.m_Speed = Mathf.SmoothStep(cart.m_Speed, 0f, t);

            if (cart.m_Speed < 0.15f)
            {
                cart.m_Speed = 0f;
                OnStopEvent?.Invoke();
                break;
            }

            yield return null;
        }  
    }
}
