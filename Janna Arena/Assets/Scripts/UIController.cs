using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIControler : MonoBehaviour
{

    public Slider ExpSlider;
    public TMP_Text ExpLevelText;
    static public UIControler instance;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateExp(int currentExp, int LevelExp, int currentLevel)
    {
        ExpSlider.maxValue = LevelExp;
        ExpSlider.value = currentExp;

        ExpLevelText.text = "Level: " + currentLevel;
    }
}
