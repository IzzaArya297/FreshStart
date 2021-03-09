using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    public int kabelDiperlukan;

    float timeElapsed;
    public float lerpDuration = 3;

    public Transform wall;
    float startValue;
    public float endValue = 10;
    float valueToLerp;
    [HideInInspector]
    public bool openWall;
    // Start is called before the first frame update
    void Start()
    {
        if (gameManager == null)
            gameManager = this;
        startValue = wall.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (openWall)
            OpenWall();
    }
    public void OpenWall()
    {
        if (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            openWall = false;
        }
        wall.position = new Vector2(valueToLerp, wall.position.y);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        SceneLoader.sceneLoader.ChangeScene(0);
    }

}
