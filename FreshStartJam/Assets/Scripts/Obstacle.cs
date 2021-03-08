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

        //Debug.Log("index : " + index);
        //Debug.Log("points length : " + inputPath.points.Count);

        if (!inputPath.firstObsDestroyed)
        {
            if (inputPath.points.Count - 3 == index)
            {
                health--;
            }
        }
        else
        {
            if (inputPath.points.Count - 2 == index)
            {
                health--;
                //Debug.Log("Take Damage ");
            }
        }

        if (health <= 0)
        {
            if (!inputPath.firstObsDestroyed)
            {
                Destroyed(index + 1);
                Destroyed(index);
                inputPath.firstObsDestroyed = true;
            }
            else
            {
                Destroyed(index);
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
        //Debug.Log("WaitForIt!");
        yield return new WaitForSeconds(timeEmit);
        //Debug.Log("Duerrrrr!");
        electrocute.transform.localScale = Vector3.one * electrocuteRadius;
        yield return new WaitForSeconds(2f);
        //Debug.Log("Duerrrrr done!");
        electrocute.transform.localScale = Vector3.one * 0.001f;
        emiting = false;
    }

}
