using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int kabelDiperlukan;
    public Animator wall;
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
            gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        RestartLevel();
    }

    public void GoHome()
    {
        SceneLoader.sceneLoader.ChangeScene(0);
    }

    public void RestartLevel()
    {
        SceneLoader.sceneLoader.ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenWall()
    {
        wall.Play("openWall");
        Debug.Log("a");
    }

    private void OnTriggerEnter2D()
    {
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        SceneLoader.sceneLoader.ChangeScene(0);
    }
}
