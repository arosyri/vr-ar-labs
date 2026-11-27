using UnityEngine;
using TMPro;

public class RobotAssembly : MonoBehaviour
{
    public AssemblySocket[] sockets;

    public TMP_Text board;

    [Header("Result (used later for the server report)")]
    public bool assembled;
    public float assemblyTime;

    private float startTime = -1f;

    void Update()
    {
        int filled = 0;
        foreach (AssemblySocket s in sockets)
        {
            if (s.IsFilled) filled++;
        }

        if (filled > 0 && startTime < 0f) startTime = Time.time;
        if (filled == 0) startTime = -1f;

        bool done = filled == sockets.Length;

        if (done && !assembled)
        {
            assembled = true;
            assemblyTime = Time.time - startTime;
            Debug.Log("Robot assembled in " + assemblyTime.ToString("F1") + " s");
        }
        else if (!done && assembled)
        {
            assembled = false;
            Debug.Log("Robot disassembled");
        }

        if (assembled)
        {
            board.text = "ROBOT ASSEMBLED!\n" + assemblyTime.ToString("F1") + " s";
            board.color = Color.green;
        }
        else
        {
            board.text = "Assembled: " + filled + "/" + sockets.Length;
            board.color = Color.white;
        }
    }
}