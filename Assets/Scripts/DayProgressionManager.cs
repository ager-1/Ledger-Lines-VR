using UnityEngine;
using TMPro;

public class DayProgressionManager : MonoBehaviour
{
    [Header("Game Data")]
    public DayData[] allDays;

    [Header("UI References")]
    public TextMeshProUGUI scenarioDisplayText;

    private int currentDayIndex = 0;
    private int currentScenarioIndex = 0;
    public OptionNode[] choiceNodes;

    void Start()
    {
        currentDayIndex = PlayerPrefs.GetInt("SavedDay", 0);

        if (currentDayIndex >= allDays.Length)
        {
            currentDayIndex = 0;
        }

        DisplayCurrentScenario();
    }

    public void DisplayCurrentScenario()
    {
        DayData today = allDays[currentDayIndex];
        ScenarioData currentScenario = today.scenarios[currentScenarioIndex];

        scenarioDisplayText.text = "Day " + (currentDayIndex + 1) + "\n\n" + currentScenario.scenarioDescription;
        for (int i = 0; i < 3; i++)
        {
            choiceNodes[i].SetupNode(currentScenario.options[i]);
            choiceNodes[i].ResetTrace();
        }
    }
    public void OnTraceCompleted()
    {
        currentScenarioIndex++;

        if (currentScenarioIndex >= 3)
        {
            CompleteDay();
        }
        else
        {
            DisplayCurrentScenario();
        }
    }
    private void CompleteDay()
    {
        currentDayIndex++;
        currentScenarioIndex = 0;
        PlayerPrefs.SetInt("SavedDay", currentDayIndex);
        PlayerPrefs.Save();

        if (currentDayIndex >= allDays.Length)
        {
            scenarioDisplayText.text = "Week Complete!\n\nTriggering the Emergency Protocol.";
        }
        else
        {
            DisplayCurrentScenario();
        }
    }
}