using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    public static SelectionMenu Instance;

    public GameObject menuRoot;
    public Image[] choiceButtons;
    private MatchIcon currentIcon;

    private void Awake()
    {
        Instance = this;
        menuRoot.SetActive(false);
    }

    public void Open(MatchIcon icon)
    {
        currentIcon = icon;
        menuRoot.SetActive(true);
    }

    public void ChooseOption(int index)
    {
        Sprite selectedSprite = choiceButtons[index].sprite;
        currentIcon.SetSelectedSprite(selectedSprite);
        menuRoot.SetActive(false);
    }
}
