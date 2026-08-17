using UnityEngine;

[System.Serializable]
public class ScenarioOption
{
    public string optionText;
    public float cost;
    public int difficultyLevel;
}

[CreateAssetMenu(fileName = "NewScenario", menuName = "LedgerLines/Scenario")]
public class ScenarioData : ScriptableObject
{
    [TextArea(3, 5)]
    public string scenarioDescription;
    public ScenarioOption[] options = new ScenarioOption[3];
}