using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader sceneLoader;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        if (sceneLoader == null)
            sceneLoader = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(int index)
    {
        anim.Play("Fade");
        StartCoroutine(LoadScene(index));
    }

    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene(index);
    }
}
