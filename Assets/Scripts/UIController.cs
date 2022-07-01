using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject restartButton;

    void Start()
    {
        restartButton.SetActive(false);
    }

    public void ShowMenu()
    {
        menu.SetActive(true);
        restartButton.SetActive(true);
    }

    public void HideMenu()
    {
        menu.SetActive(false);
        restartButton.SetActive(false);
    }
}
