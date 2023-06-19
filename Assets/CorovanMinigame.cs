using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorovanMinigame : MonoBehaviour
{
    [SerializeField] private Minigame1 game1;

    [SerializeField] private Minigame2 game2;

    [SerializeField] private GameObject panel;

    public bool complete;

    public void StartGame()
    {
        complete = false;
        panel.SetActive(true);
        var rand = Random.Range(0, 2);
        game1.gameObject.SetActive(true);
        // switch (rand)
        // {
        //     case 0:
        //         game1.gameObject.SetActive(true);
        //         break;
        //     case 1:
        //         game2.gameObject.SetActive(true);
        //         break;
        // }
    }
}
