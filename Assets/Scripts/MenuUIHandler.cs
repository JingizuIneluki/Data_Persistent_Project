using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    //public ColorPicker ColorPicker;
    public Button colorButton;
    public Color newColor;
    public Image colorPreview;
    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    public GameObject settings;
    public GameObject main;
    public TMP_InputField inputField;
    public String playerName;

    private void Start()
    {
        settings.SetActive(false);
    
        redSlider.onValueChanged.AddListener(UpdateColor);
        greenSlider.onValueChanged.AddListener(UpdateColor);
        blueSlider.onValueChanged.AddListener(UpdateColor);

    }
    public void StartNew()
    {
        SceneManager.LoadScene("main");
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void SaveColor()
    {
        GameManager.Instance.SetColorValues(redSlider.value, greenSlider.value, blueSlider.value);
        GameManager.Instance.TeamColor = newColor;
    }
    private void UpdateColor(float value)
    {
        // Update the color preview based on the slider values
         newColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
        colorPreview.color = newColor;
       

    }
    public void EnableSettings()
    {
        settings.SetActive(true);
        main.SetActive(false);
    }
    public void Return()
    {
        settings.SetActive(false);
        main.SetActive(true);
    }
    public void MusicOn()
    {
        
            GameManager.Instance.StartStopMusic(0) ;
        
        
    }
    public void MusicOff()
    {
        GameManager.Instance.StartStopMusic(1);

    }
    public void SetName()
    {
        playerName=inputField.text;
        GameManager.Instance.SaveNamePlayer(playerName);
    }
}
