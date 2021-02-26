using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEnum;

public class OutputSource : MonoBehaviour
{
    public InputSource inputType;
    public Transform source;

    bool fastMode = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(inputType.inputCondition == Condition.Connected)
        {

        }
        if(inputType.lineCreated && fastMode)
        {
            inputType.CancelInvoke("SpawnElectro");
            fastMode = false;
            inputType.InvokeRepeating("SpawnElectro", 0.1f, 1 / inputType.deltaSpawnElectro);
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

                inputType.cable.positionCount++;
                inputType.points.Add(source.position);
                inputType.cable.SetPositions(inputType.points.ToArray());
                inputType.inputCondition = Condition.Connected;
                //Spawn electro sebanyak deltaSpawnElectro perdetik
                inputType.InvokeRepeating("SpawnElectro", 0.1f, 1/inputType.deltaSpawnElectro);
                collision.gameObject.GetComponent<PlayerControl>().currentInput = null;
                GameManager.gameManager.kabelTerhubung++;
                if (GameManager.gameManager.kabelTerhubung == GameManager.gameManager.kabelDiperlukan)
                {
                    GameManager.gameManager.Done();
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
