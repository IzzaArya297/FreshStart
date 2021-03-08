using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorManager : MonoBehaviour
{
    public GameObject[] tutor;
    public PlayerControl player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (player.currentInput != null && tutor[0] != null)
            {
                Destroy(tutor[0]);
                tutor[1].SetActive(true);
            }
            else if (player.currentInput == null && tutor[1] != null && tutor[1].activeSelf)
            {
                Destroy(tutor[1]);
                tutor[2].SetActive(true);
            }
        }
        else
        {
            if(GameManager.gameManager.kabelDiperlukan == 3 && tutor[0] != null)
            {
                Destroy(tutor[0]);
                tutor[2].SetActive(true);
            }
            else if(GameManager.gameManager.kabelDiperlukan == 2 && tutor[2] != null)
            {
                Destroy(tutor[2]);
                tutor[3].SetActive(true);
            }
            else if(GameManager.gameManager.kabelDiperlukan == 1 && tutor[3] != null)
            {
                Destroy(tutor[3]);
                tutor[4].SetActive(true);
            }
            else if(GameManager.gameManager.kabelDiperlukan == 0 && tutor[1] != null)
            {
                Destroy(tutor[1]);
                tutor[5].SetActive(true);
            }
        }
    }
}
