using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLookAt : MonoBehaviour
{
    public CinemachineBrain camBrain;
    public CinemachineVirtualCamera camOne;
    public CinemachineVirtualCamera camTwo;

    public Transform newLook;

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
            StartCoroutine(Switchin());
        }
    }


    IEnumerator Switchin()
    {
        if (camOne.enabled == true) // If cam 1 is on, change to cam two
        {
            camTwo.LookAt = newLook;

            camOne.enabled = false;
            camTwo.enabled = true;
        }
        else // Vice versa
        {
            camOne.LookAt = newLook;

            camTwo.enabled = false;
            camOne.enabled = true;
        }

        yield break;
    }
}
