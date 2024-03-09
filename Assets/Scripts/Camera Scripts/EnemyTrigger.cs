using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    CinemachineDollyCart cart;

    public List<GameObject> enemiesInThisZone = new List<GameObject>();

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
            cart = obj.gameObject.GetComponent<CinemachineDollyCart>();

            EnableEnemies();
        }
    }

    private void EnableEnemies()
    {
        enemiesInThisZone.ForEach(e => { e.SetActive(true); }); // Turn 'em all on

        StartCoroutine(TrackEnemies());
    }

    private IEnumerator TrackEnemies() // If all enemies are dead tell cart to move on
    {
        int aliveEnemyCount = 0;
        List<DroneController> droneList = new List<DroneController>();

        enemiesInThisZone.ForEach(e => { droneList.Add(e.GetComponent<DroneController>()); });
        
        while (true)
        {
            aliveEnemyCount = 0;

            foreach (DroneController enemy in droneList)
            {
                if (!enemy.isDead) aliveEnemyCount++; // Add to alive enemy count
            }

            if (aliveEnemyCount <= 0)
            {
                StartCoroutine(DeleteEnemiesFromWave(5f));
                StartCoroutine(ChangeSpeed(cart, Time.time));
                break;
            }

            yield return new WaitForSeconds(2f); // Only check every 2 seconds. No need for anything faster
        }     
    }

    private IEnumerator ChangeSpeed(CinemachineDollyCart cart, float initTime)
    {
        float changeDuration = 5f;
        float newSpeed = 30f;

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

    private IEnumerator DeleteEnemiesFromWave(float initTime)
    {
        yield return new WaitForSeconds(initTime);

        for (int i = enemiesInThisZone.Count - 1; i >= 0; i--)
        {
            Destroy(enemiesInThisZone[i].gameObject);
            enemiesInThisZone.RemoveAt(i);
        }
    }
    
}
