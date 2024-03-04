using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericTrigger : MonoBehaviour
{
    public UnityEvent Event;

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
            Event?.Invoke();
        }
    }
}
