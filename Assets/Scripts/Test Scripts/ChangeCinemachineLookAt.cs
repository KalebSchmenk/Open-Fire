using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCinemachineLookAt : MonoBehaviour
{
    public CinemachineBrain camBrain;
    public CinemachineVirtualCamera camOne;
    public CinemachineVirtualCamera camTwo;
    public CinemachineDollyCart dollyCart;
    public float switchIn = 2.5f;

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
        yield return new WaitForSeconds(switchIn);
        camOne.enabled = false;
        camTwo.enabled = true;
    }
}
