using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader sceneLoader;

    public GameObject panel, GUI;
    public GameObject[] healthImage;

    public Animator anim;
    public AudioSource mainMenu, sfx;
    [HideInInspector]
    public int health;
    public Text lengthLeft;
    // Start is called before the first frame update
    void Start()
    {
        if (sceneLoader != null)
            Destroy(gameObject);
        else
        {
            PlayerPrefs.DeleteKey("FirstOpen");
            sceneLoader = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(int index)
    {
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        panel.SetActive(true);
        anim.Play("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
        if (index != 0)
        {
            health = 2;
            healthImage[0].SetActive(true);
            healthImage[1].SetActive(true);
            GUI.SetActive(true);
            if(index != SceneManager.GetActiveScene().buildIndex)
            {
                GetComponent<AudioSource>().enabled = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().enabled = false;
            GUI.SetActive(false);
        }
        anim.Play("FadeOut");
        yield return new WaitForSeconds(0.9f);
        panel.SetActive(false);
    }

    public void GoToHome()
    {
        ChangeScene(0);
    }

    public void RetryLevel()
    {
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
        lengthLeft.text = "";
    }

    public IEnumerator ChangeHealth()
    {
        health--;
        healthImage[health].SetActive(false);
        if (health == 0)
        {
            yield return new WaitForSeconds(2f);
            RetryLevel();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }
}
