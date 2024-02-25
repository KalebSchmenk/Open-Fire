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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Switchin());
    }

    IEnumerator Switchin()
    {
        yield return new WaitForSeconds(switchIn);
        camOne.enabled = false;
        camTwo.enabled = true;
    }
}
