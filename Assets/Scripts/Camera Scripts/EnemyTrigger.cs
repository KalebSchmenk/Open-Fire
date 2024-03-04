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
                cart.m_Speed = 30;
                break;
            }

            yield return new WaitForSeconds(2f); // Only check every 2 seconds. No need for anything faster
        }     
    }
}
