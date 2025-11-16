using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class MatchIcon : MonoBehaviour
{
    public Sprite correctSprite;
    private Image img;

    public bool isEmpty = true;
    public Sprite chosenSprite;

    private void Awake()
    {
        img = GetComponent<Image>();
        MatchManager.Register(this);
    }

    public void OnClickIcon()
    {
        if (isEmpty)
            SelectionMenu.Instance.Open(this);
    }

    public void SetSelectedSprite(Sprite chosen)
    {
        img.sprite = chosen;
        chosenSprite = chosen;
        isEmpty = false;
    }

    public bool IsCorrect()
    {
        return chosenSprite == correctSprite;
    }
}

public static class MatchManager
{
    private static MatchIcon[] icons = new MatchIcon[0];

    public static void Register(MatchIcon icon)
    {
        var newList = new MatchIcon[icons.Length + 1];
        icons.CopyTo(newList, 0);
        newList[newList.Length - 1] = icon;
        icons = newList;
    }

    public static int CountCorrect()
    {
        int count = 0;
        foreach (var icon in icons)
        {
            if (icon.IsCorrect())
                count++;
        }
        return count;
    }
}
