using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject flyingShip;

    public List<Transform> possibleShipSpawns = new List<Transform>();
    public Transform middlePos;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnFlyingShip", 0.25f, 5f);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SpawnFlyingShip()
    {
        var random = UnityEngine.Random.Range(0, possibleShipSpawns.Count);

        var obj = Instantiate(flyingShip, possibleShipSpawns[random].position, Quaternion.identity);

        var lookRot = middlePos.position - obj.transform.position;
        lookRot.y = 0f;

        obj.transform.rotation = Quaternion.LookRotation(lookRot);
    }
}
