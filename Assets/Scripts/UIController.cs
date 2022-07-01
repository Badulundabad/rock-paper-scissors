using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject winnerMessage;

    void Start()
    {
        Messenger<Sprite>.AddListener(GameEvents.SHOW_WINNER, ShowWinner);
        Messenger.AddListener(GameEvents.SHOW_MENU, ShowMenu);
        Messenger.AddListener(GameEvents.HIDE_MENU, HideMenu);
        restartButton.SetActive(false);
        winnerMessage.SetActive(false);
    }

    private void OnDestroy()
    {
        Messenger<Sprite>.RemoveListener(GameEvents.SHOW_WINNER, ShowWinner);
        Messenger.RemoveListener(GameEvents.SHOW_MENU, ShowMenu);
        Messenger.RemoveListener(GameEvents.HIDE_MENU, HideMenu);
    }

    private void Update()
    {
        if (Input.anyKeyDown && winnerMessage.activeSelf)
        {
            winnerMessage.SetActive(false);
            restartButton.SetActive(false);
            menu.SetActive(true);
        }
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

    private void ShowWinner(Sprite sprite)
    {
        winnerMessage.GetComponent<Image>().sprite = sprite;
        winnerMessage.SetActive(true);
    }
}
