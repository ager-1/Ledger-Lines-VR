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
    public TipBotManager tipBot;
    void Start()
    {
        currentDayIndex = PlayerPrefs.GetInt("SavedDay", 0);
        if (currentDayIndex >= allDays.Length)
        {
            TriggerEmergency();
        }
        else
        {
            DisplayCurrentScenario();
        }
    }

    public void DisplayCurrentScenario()
    {
        scenarioDisplayText.color = Color.white;

        DayData today = allDays[currentDayIndex];
        ScenarioData currentScenario = today.scenarios[currentScenarioIndex];

        scenarioDisplayText.text = "Day " + (currentDayIndex + 1) + "\n\n" + currentScenario.scenarioDescription;
        if (tipBot != null)
        {
            tipBot.PlayTip(currentDayIndex, currentScenarioIndex);
        }

        for (int i = 0; i < 3; i++)
        {
            choiceNodes[i].gameObject.SetActive(true);
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
            TriggerEmergency();
        }
        else
        {
            DisplayCurrentScenario();
        }
    }

    private void TriggerEmergency()
    {
        foreach (OptionNode node in choiceNodes)
        {
            if (node != null) node.gameObject.SetActive(false);
        }
        float finalSavings = PlayerPrefs.GetFloat("MonthlyStipend", 0f);
        float emergencyCost = 80.00f; 
        if (finalSavings >= emergencyCost)
        {
            float remaining = finalSavings - emergencyCost;
            float bonus = remaining * 0.02f; 

            scenarioDisplayText.text =
                "DAY 6: EMERGENCY!\nYour school laptop screen shattered. Repair Cost: $80.00\n\n" +
                "RESULT: Your savings absorbed the hit.\n" +
                "Remaining Vault Balance: $" + remaining.ToString("F2") + "\n" +
                "DBS PayLah Bonus Interest: +$" + bonus.ToString("F2") + "\n\n" +
                "FINANCIALLY SECURE";

            scenarioDisplayText.color = Color.green;
            if (tipBot != null) tipBot.PlayEmergencyOutcome(true);
        }
        else
        {
            float debt = emergencyCost - finalSavings;

            scenarioDisplayText.text =
                "DAY 6: EMERGENCY!\nYour school laptop screen shattered. Repair Cost: $80.00\n\n" +
                "RESULT: You only have $" + finalSavings.ToString("F2") + " saved.\n" +
                "Shortfall: $" + debt.ToString("F2") + "\n\n" +
                "This shortfall becomes a high interest loan. You are now trapped in the poverty cycle.\n\n" +
                "IN DEBT";

            scenarioDisplayText.color = Color.red;
            if (tipBot != null) tipBot.PlayEmergencyOutcome(false);
        }
    }
}