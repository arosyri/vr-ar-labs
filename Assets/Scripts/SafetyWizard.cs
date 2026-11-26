using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SafetyWizard : MonoBehaviour
{
    [Header("Pages")]
    public GameObject formPage;
    public GameObject instructionPage;
    public GameObject questionPage;
    public GameObject resultPage;

    [Header("Form")]
    public ReportForm form;

    [Header("Buttons")]
    public Button formNextButton;
    public Button startTestButton;
    public Button restartButton;
    public Button[] answerButtons;

    [Header("Texts")]
    public TMP_Text progressText;
    public TMP_Text questionText;
    public TMP_Text feedbackText;
    public TMP_Text resultText;

    [Header("Final result (used later for the server report)")]
    public bool completed;
    public string userId;
    public string userName;
    public int score;
    public int errors;
    public float durationSeconds;
    public bool passed;

    private enum Step { Form, Instruction, Question, Result }

    private class Question
    {
        public string text;
        public string[] answers;
        public int correct;
    }

    private Question[] questions =
    {
        new Question
        {
            text = "What must you wear at the assembly table?",
            answers = new[] { "Safety glasses and gloves", "Headphones only", "Nothing special" },
            correct = 0
        },
        new Question
        {
            text = "What should you do when you enter the red danger zone?",
            answers = new[] { "Stay and keep working", "Leave it immediately", "Ignore the warning" },
            correct = 1
        },
        new Question
        {
            text = "What must you do before assembling the robot head?",
            answers = new[] { "Throw it on the table", "Skip the inspection", "Check that the part is not damaged" },
            correct = 2
        }
    };

    private int questionIndex;
    private float startTime;
    private bool waitingForNext;

    void Start()
    {
        formNextButton.onClick.AddListener(OnFormNext);
        startTestButton.onClick.AddListener(OnStartTest);
        restartButton.onClick.AddListener(OnRestart);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => OnAnswer(index));
        }

        ShowStep(Step.Form);
    }

    void ShowStep(Step step)
    {
        formPage.SetActive(step == Step.Form);
        instructionPage.SetActive(step == Step.Instruction);
        questionPage.SetActive(step == Step.Question);
        resultPage.SetActive(step == Step.Result);
    }

    void OnFormNext()
    {
        if (string.IsNullOrWhiteSpace(form.savedName))
        {
            form.statusText.text = "Press Save first";
            return;
        }
        ShowStep(Step.Instruction);
    }

    void OnStartTest()
    {
        questionIndex = 0;
        score = 0;
        errors = 0;
        completed = false;
        userId = System.Guid.NewGuid().ToString();
        startTime = Time.time;
        ShowQuestion();
    }

    void ShowQuestion()
    {
        ShowStep(Step.Question);
        waitingForNext = false;

        Question q = questions[questionIndex];
        progressText.text = "Question " + (questionIndex + 1) + "/" + questions.Length;
        questionText.text = q.text;
        feedbackText.text = "";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = q.answers[i];
        }
    }

    void OnAnswer(int index)
    {
        if (waitingForNext) return;
        waitingForNext = true;

        Question q = questions[questionIndex];
        if (index == q.correct)
        {
            score++;
            feedbackText.text = "Correct!";
        }
        else
        {
            errors++;
            feedbackText.text = "Wrong. Correct answer: " + q.answers[q.correct];
        }

        Invoke(nameof(NextQuestion), 1.5f);
    }

    void NextQuestion()
    {
        questionIndex++;
        if (questionIndex >= questions.Length) Finish();
        else ShowQuestion();
    }

    void Finish()
    {
        durationSeconds = Time.time - startTime;
        passed = score >= 2;
        completed = true;
        userName = form.savedName;

        resultText.text =
            "Name: " + userName + "\n" +
            "Correct answers: " + score + "/" + questions.Length + "\n" +
            "Errors: " + errors + "\n" +
            "Time: " + durationSeconds.ToString("F1") + " s\n" +
            "Result: " + (passed ? "PASSED" : "FAILED");

        Debug.Log("Wizard finished. " + resultText.text.Replace("\n", " | "));
        ShowStep(Step.Result);
    }

    void OnRestart()
    {
        ShowStep(Step.Form);
    }
}