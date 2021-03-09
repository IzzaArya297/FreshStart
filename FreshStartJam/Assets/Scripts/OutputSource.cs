using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnum;

public class OutputSource : MonoBehaviour
{
    public InputSource inputType;
    public Transform source;
    public MultipleInputSource multInput;

    bool fastMode = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inputType != null)
        {

            if (inputType.lineCreated && fastMode)
            {
                inputType.CancelInvoke("SpawnElectro");
                inputType.normalize();
                inputType.player.canMove = true;
                fastMode = false;
                inputType.InvokeRepeating("SpawnElectro", 0.01f, 1 / inputType.deltaSpawnElectro);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (inputType.player.currentInput == inputType.gameObject && inputType.inputCondition == Condition.Bought)
            {
                //Set deltaSpawnRate
                inputType.setElectroRate();

                inputType.player.canMove = false;
                inputType.player.rb.velocity = Vector3.zero;

                inputType.cable.positionCount++;
                inputType.points.Add(source.position);
                inputType.cable.SetPositions(inputType.points.ToArray());
                inputType.inputCondition = Condition.Connected;
                //Spawn electro sebanyak deltaSpawnElectro perdetik
                inputType.InvokeRepeating("SpawnElectro", 0.01f, 1/inputType.deltaSpawnElectro);
                collision.gameObject.GetComponent<PlayerControl>().currentInput = null;
                SceneLoader.sceneLoader.lengthLeft.text = "";
                if (--GameManager.gameManager.kabelDiperlukan == 0)
                    GameManager.gameManager.openWall = true;

                
                if(multInput != null)
                {
                    //multInput.currentInput++;
                    multInput.enable(multInput.currentInput);
                    Debug.Log("ada");
                }
            }
            collision.gameObject.layer = 11;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.layer = 0;
        }
    }
}
