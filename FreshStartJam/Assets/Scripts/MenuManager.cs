using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    float dclick_threshold = 0.3f, timerdclick = 0;

    public GameObject errorPanel, LevelPanel, uninstallPanel, installPanel;
    public Animator animInstall;

    public Button[] levelInstallButton, levelUninstallButton;
    bool installOpened = true;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("FirstOpen"))
        {
            LevelPanel.SetActive(true);
            installPanel.SetActive(true);
        }
        ChangeGreen();
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
        LevelPanel.SetActive(true);
        animInstall.Play("firstReload");
        PlayerPrefs.SetInt("FirstOpen", 0);
    }

    public void CloseError()
    {
        errorPanel.SetActive(false);
    }

    public void GoToLevel(int index)
    {
        SceneLoader.sceneLoader.ChangeScene(index);
    }

    public void ChangePanel()
    {
        if (installOpened)
        {
            uninstallPanel.SetActive(true);
            installPanel.SetActive(false);
            installOpened = false;
            return;
        }
        uninstallPanel.SetActive(false);
        installPanel.SetActive(true);
        installOpened = true;
    }

    void ChangeGreen()
    {
        for(int i = 0; i < levelInstallButton.Length; i++)
        {
            Debug.Log(PlayerPrefs.HasKey(levelInstallButton[i].gameObject.name));
            if (!PlayerPrefs.HasKey(levelInstallButton[i].gameObject.name))
                continue;
            ColorBlock colors = levelInstallButton[i].colors;
            Color temp = colors.normalColor;
            temp.a = 0.5f;
            colors.normalColor = temp;
            levelInstallButton[i].colors = colors;
        }
        for (int i = 0; i < levelUninstallButton.Length; i++)
        {
            if (!PlayerPrefs.HasKey(levelUninstallButton[i].gameObject.name))
                continue;
            ColorBlock colors = levelUninstallButton[i].colors;
            Color temp = colors.normalColor;
            temp.a = 0.5f;
            colors.normalColor = temp;
            levelUninstallButton[i].colors = colors;
        }
    }
}
