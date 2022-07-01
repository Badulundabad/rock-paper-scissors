using UnityEngine;

[RequireComponent(typeof(GameController))]
[RequireComponent(typeof(UIController))]
public class Controllers : MonoBehaviour
{
    public static UIController UI { get; private set; }
    public static GameController Game { get; private set; }

    void Start()
    {
        UI = GetComponent<UIController>();
        Game = GetComponent<GameController>();
    }
}
