using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedChangeTrigger : MonoBehaviour
{
    public float changeDuration = 5f;
    public float newSpeed = 30f;

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

            StartCoroutine(ChangeSpeed(cart, Time.time));
        }
    }

    private IEnumerator ChangeSpeed(CinemachineDollyCart cart, float initTime)
    {
        while (true)
        {
            float t = (Time.time - initTime) / changeDuration;

            cart.m_Speed = Mathf.SmoothStep(cart.m_Speed, newSpeed, t);

            if (Mathf.Abs(cart.m_Speed - newSpeed) < 0.15)
            {
                cart.m_Speed = newSpeed;
                break;
            }

            yield return null;
        }
    }
}
