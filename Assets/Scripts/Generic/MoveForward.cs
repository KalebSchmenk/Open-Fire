using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5f;

    public void MoveObjectForward()
    {
        StartCoroutine(MoveForwardCoroutine());
        Destroy(this.gameObject, 25f);
    }

    private IEnumerator MoveForwardCoroutine()
    {
        while (true)
        {
            this.transform.Translate(Vector3.forward * speed * 0.25f);
            yield return new WaitForFixedUpdate();
        }
    }
}
