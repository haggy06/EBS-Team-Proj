using UnityEngine;

public enum INDEX_InputKey
{
    Horizintal_Positive,
    Horizintal_Negative,
    Vertical_Positive,
    Vertical_Negative,
    Jump,
    Parry,
    Attack,
    Skill_Fire,
    Skill_Change,
    Heal,
    Inventory,
    Map,

}

[System.Serializable]
public class SettingData
{
    public KeyCode[] inputKey =
    {
        KeyCode.RightArrow, // horizontal_Positive
        KeyCode.LeftArrow, // horizintal_Negative

        KeyCode.UpArrow, // vertical_Positive
        KeyCode.DownArrow, // vertical_Negative

        KeyCode.Z, // jump
        KeyCode.A, // parry

        KeyCode.X, // attack

        KeyCode.C, // skill_Fire
        KeyCode.LeftShift, // skill_Change

        KeyCode.S, // heal

        KeyCode.D, // Inventory
        KeyCode.Tab, // map
    };


}