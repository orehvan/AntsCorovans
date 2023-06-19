using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Minigame2 : MonoBehaviour
{
    private List<string> colors = new List<string>{"red", "blue", "yellow", "purple"};
    public int correctInputs;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject wireStart;
    [SerializeField] private GameObject wireEnd;
    public bool isWiring;
    public string wiringColor;
    public GameObject source;

    [SerializeField] private CorovanMinigame game;
    
    void Start()
    {
        PlayerController.Instance.inputDisabled = true;
        correctInputs = 0;
        GenerateMinigame();
    }

    private void GenerateMinigame()
    {
        var generatedColors = new List<string>();
        foreach (Transform wireGO in wireStart.transform)
        {
            var wire = wireGO.GetComponent<Wire>();
            var color = colors[Random.Range(0, colors.Count)];
            wire.color = color;
            wire.ColorWire();
            generatedColors.Add(color);
        }

        foreach (Transform wireGO in wireEnd.transform)
        {
            var wire = wireGO.GetComponent<Wire>();
            var index = Random.Range(0, generatedColors.Count);
            var color = generatedColors[index];
            generatedColors.RemoveAt(index);
            wire.color = color;
            wire.ColorWire();
        }
    }
    
    public bool CheckInput(string input)
    {
        if (input != wiringColor)
            return false;
        isWiring = false;
        correctInputs++;
        if (correctInputs == 4)
        {
            Complete();
        }
        return true;
    }
    
    private void Complete()
    {
        game.complete = true;
        gameObject.SetActive(false);
        panel.SetActive(false);
        PlayerController.Instance.inputDisabled = false;
    }
}
