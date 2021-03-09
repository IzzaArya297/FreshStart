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

    public AudioClip tangSound;

    // Start is called before the first frame update
    void Start()
    {
        //inputPath = this.transform.parent.gameObject.GetComponentInParent<LineSource>();
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
                //AudioManager.audioManager.PlaySound(tangSound);
            }
        }
        else
        {
            if (inputPath.points.Count - 2 == index)
            {
                health--;
                //AudioManager.audioManager.PlaySound(tangSound);
            }
        }

        if (health <= 0)
        {
            if (!inputPath.firstObsDestroyed)
            {
                Destroyed(index + 1);
                Destroyed(index);
                inputPath.firstObsDestroyed = true;
                //AudioManager.audioManager.PlaySound(tangSound);
            }
            else
            {
                Destroyed(index);
                //AudioManager.audioManager.PlaySound(tangSound);
            }
        }
    }

    public void Destroyed(int i)
    {
        if (--GameManager.gameManager.kabelDiperlukan == 0)
            GameManager.gameManager.openWall = true;
        Destroy(inputPath.Obstacle[i]);
        inputPath.points.RemoveAt(inputPath.points.Count - 1);
        inputPath.cable.positionCount--;
    }

    IEnumerator electrocuting()
    {
        emiting = true;
        yield return new WaitForSeconds(timeEmit);
        electrocute.transform.localScale = Vector3.one * electrocuteRadius;
        yield return new WaitForSeconds(2f);
        electrocute.transform.localScale = Vector3.one * 0.001f;
        emiting = false;
    }

}
