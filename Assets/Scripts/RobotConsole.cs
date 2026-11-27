using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RobotConsole : MonoBehaviour
{
    public Toggle powerToggle;
    public Slider speedSlider;
    public Button runButton;
    public TMP_Text speedText;
    public TMP_Text statusText;

    public RobotAssembly assembly;

    void Start()
    {
        powerToggle.onValueChanged.AddListener(OnPowerChanged);
        speedSlider.onValueChanged.AddListener(OnSpeedChanged);
        runButton.onClick.AddListener(OnRun);

        OnSpeedChanged(speedSlider.value);
        statusText.text = "Power is off";
    }

    void OnPowerChanged(bool isOn)
    {
        statusText.text = isOn ? "Power is on" : "Power is off";
    }

    void OnSpeedChanged(float value)
    {
        speedText.text = "Speed: " + Mathf.RoundToInt(value);
    }

    void OnRun()
    {
        Debug.Log("Run button pressed. Power: " + powerToggle.isOn + ", assembled: " + (assembly != null && assembly.assembled));

        if (!powerToggle.isOn)
        {
            statusText.text = "Turn the power on first";
        }
        else if (assembly == null)
        {
            statusText.text = "Assembly manager is not assigned";
        }
        else if (!assembly.assembled)
        {
            statusText.text = "Robot is not assembled yet";
        }
        else
        {
            statusText.text = "Diagnostics OK. Speed " + Mathf.RoundToInt(speedSlider.value);
            Debug.Log("Diagnostics passed, speed " + Mathf.RoundToInt(speedSlider.value));
        }
    }
}