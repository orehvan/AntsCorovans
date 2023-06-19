using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Minigame1 : MonoBehaviour
{
    private List<string> colors = new List<string>{"red", "blue", "yellow", "purple", "brown"};
    private Dictionary<string, bool> colorsInserted = new Dictionary<string, bool>();
    public string currentColor;
    private int correctInputs;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panel;
    [SerializeField] private CorovanMinigame game;
    [SerializeField] private List<ColorButton> buttons;
    void Start()
    {
        // foreach (var color in colors)
        //     colorsInserted.Add(color, false);
        GenerateStart();
    }

    public void GenerateStart()
    {
        PlayerController.Instance.inputDisabled = true;
        foreach (var color in colors)
            colorsInserted[color] = false;
        foreach (var button in buttons)
            button.Reset();
        
        correctInputs = 0;
        GenerateNextColor();
    }

    private void GenerateNextColor()
    {
        currentColor = colors[Random.Range(0, colors.Count)];
        while (colorsInserted[currentColor])
            currentColor = colors[Random.Range(0, colors.Count)];
        text.text = $"Press {currentColor}!";

    }

    public bool CheckInput(string input)
    {
        if (input != currentColor) return false;
        
        colorsInserted[currentColor] = true;
        correctInputs++;
        if (correctInputs == 5)
        {
            Complete();
            return true;
        }
        GenerateNextColor();
        return true;
    }

    private void Complete()
    {
        gameObject.SetActive(false);
        panel.SetActive(false);
        game.complete = true;
        PlayerController.Instance.inputDisabled = false;
    }
}
