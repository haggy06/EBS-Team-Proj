using UnityEngine;

internal static class ButtonHashes
{
    private static int normal = Animator.StringToHash("Normal");
    public static int Normal => normal;


    private static int highlighted = Animator.StringToHash("Highlighted");
    public static int Highlighted => highlighted;


    private static int pressed = Animator.StringToHash("Pressed");
    public static int Pressed => pressed;


    private static int selected = Animator.StringToHash("Selected");
    public static int Selected => selected;


    private static int disabled = Animator.StringToHash("Disabled");
    public static int Disabled => disabled;
}
