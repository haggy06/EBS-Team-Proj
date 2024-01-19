using UnityEngine;

internal class PlayerHash
{
    private static int damaged = Animator.StringToHash("Damaged");
    public static int Damaged => damaged;


    private static int stress_UP = Animator.StringToHash("Stress_UP");
    public static int Stress_UP => stress_UP;


    private static int stress_DOWN = Animator.StringToHash("Stress_DOWN ");
    public static int Stress_DOWN => stress_DOWN;


    private static int walking = Animator.StringToHash("Walking");
    public static int Walking => walking;


    private static int running = Animator.StringToHash("Running");
    public static int Running => running;


    private static int exhausted = Animator.StringToHash("Exhausted");
    public static int Exhausted => exhausted;
}
