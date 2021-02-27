using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    float dclick_threshold = 0.3f, timerdclick = 0;

    public GameObject errorPanel, installPanel;
    public Animator animInstall;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("FirstOpen"))
        {
            installPanel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AppClicked()
    {
        if ((Time.time - timerdclick) > dclick_threshold)
        {
            Debug.Log("single click");
            //call the SingleClick() function, not shown
        }
        else
        {
            Debug.Log("double click");
            errorPanel.SetActive(true);
        }
        timerdclick = Time.time;
    }

    public void DoFreshStart()
    {
        errorPanel.SetActive(false);
        installPanel.SetActive(true);
        animInstall.Play("firstReload");
        PlayerPrefs.SetInt("FirstOpen", 0);
    }

    public void CloseError()
    {
        errorPanel.SetActive(false);
    }

    public void GoToLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
