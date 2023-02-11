using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject gameCanvas;
    public GameObject sceneCamera;

    private void Awake()
    {
        gameCanvas.SetActive(true);

    }

    public void spawnPlayer()
    {
       
            float randomValue = Random.Range(-1f, 1f);

       


        gameCanvas.SetActive(false);
        sceneCamera.SetActive(false);
    }

}
