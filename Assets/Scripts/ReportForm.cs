using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReportForm : MonoBehaviour
{
    public TMP_InputField nameField;
    public Toggle glassesToggle;
    public Toggle glovesToggle;
    public Slider torqueSlider;
    public TMP_Text sliderValueText;
    public TMP_Text statusText;

    public string defaultName = "Trainee";

    public string savedName;
    public bool savedGlasses;
    public bool savedGloves;
    public int savedTorque;

    void Start()
    {
        nameField.text = defaultName;
        OnSliderChanged(torqueSlider.value);
        statusText.text = "";
    }

    public void OnSliderChanged(float value)
    {
        sliderValueText.text = "Torque: " + Mathf.RoundToInt(value);
    }

    public void Save()
    {
        if (string.IsNullOrWhiteSpace(nameField.text))
        {
            statusText.text = "Please enter your name";
            return;
        }

        savedName = nameField.text;
        savedGlasses = glassesToggle.isOn;
        savedGloves = glovesToggle.isOn;
        savedTorque = Mathf.RoundToInt(torqueSlider.value);

        statusText.text = "Saved: " + savedName + ", glasses " + savedGlasses + ", gloves " + savedGloves + ", torque " + savedTorque;
        Debug.Log(statusText.text);
    }

    public void ResetForm()
    {
        nameField.text = defaultName;
        glassesToggle.isOn = false;
        glovesToggle.isOn = false;
        torqueSlider.value = 50;
        statusText.text = "Form reset";
    }
}