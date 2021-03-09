using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float health;
    public float pushForce;

    public LineSource inputPath;

    public GameObject electrocute;
    public float electrocuteRadius;
    public float timeEmit;
    bool emiting;
    
    public int index;

    public AudioClip tangSound, destroy;

    // Start is called before the first frame update
    void Start()
    {
        inputPath = this.transform.parent.gameObject.GetComponentInParent<LineSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (!emiting)
        {
            StartCoroutine(electrocuting());
        }
    }
    public void takeDamage()
    {
        if (!inputPath.firstObsDestroyed)
        {
            if (inputPath.points.Count - 3 == index)
            {
                health--;
<<<<<<< HEAD
                SceneLoader.sceneLoader.PlaySound(tangSound);
=======
                AudioManager.audioManager.PlaySound(tangSound);
>>>>>>> parent of 3d9628f (ayayayayayay)
            }
        }
        else
        {
            if (inputPath.points.Count - 2 == index)
            {
                health--;
<<<<<<< HEAD
                SceneLoader.sceneLoader.PlaySound(tangSound);
=======
                AudioManager.audioManager.PlaySound(tangSound);
>>>>>>> parent of 3d9628f (ayayayayayay)
            }
        }

        if (health <= 0)
        {
            if (!inputPath.firstObsDestroyed)
            {
                Destroyed(index + 1);
                Destroyed(index);
                inputPath.firstObsDestroyed = true;
<<<<<<< HEAD
                SceneLoader.sceneLoader.PlaySound(tangSound);
=======
                AudioManager.audioManager.PlaySound(tangSound);
>>>>>>> parent of 3d9628f (ayayayayayay)
            }
            else
            {
                Destroyed(index);
<<<<<<< HEAD
                SceneLoader.sceneLoader.PlaySound(tangSound);
=======
                AudioManager.audioManager.PlaySound(tangSound);
>>>>>>> parent of 3d9628f (ayayayayayay)
            }
        }
    }

    public void Destroyed(int i)
    {
        if (--GameManager.gameManager.kabelDiperlukan == 0)
            GameManager.gameManager.openWall = true;
        SceneLoader.sceneLoader.PlaySound(destroy);
        Destroy(inputPath.Obstacle[i]);
        inputPath.points.RemoveAt(inputPath.points.Count - 1);
        inputPath.cable.positionCount--;
    }

    IEnumerator electrocuting()
    {
        emiting = true;
        Invoke("Kedip", 3.5f);
        yield return new WaitForSeconds(timeEmit);
        electrocute.transform.localScale = Vector3.one * electrocuteRadius;
        GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(2f);
        electrocute.transform.localScale = Vector3.one * 0.001f;
        emiting = false;
    }

    void Kedip()
    {
        GetComponent<Animator>().Play("Electrocute");
    }
}
