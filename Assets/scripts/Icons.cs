using UnityEngine;
using UnityEngine.UI;

public class IconRandomizer : MonoBehaviour
{
    public Sprite[] possibleSprites; // рабочие спрайты
    public Sprite emptyIcon;         // пустая иконка
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        IconsResetManager.Register(this);
    }

    void Start()
    {
        Randomize();
    }

    public void Randomize()
    {
        img.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
    }

    public void ResetIcon()
    {
        img.sprite = emptyIcon;
    }
}

public class Billboard : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target == null)
            target = Camera.main.transform;

        transform.LookAt(transform.position + target.forward);
    }
}

public static class IconsResetManager
{
    public static IconRandomizer[] icons;

    public static void Register(IconRandomizer icon)
    {
        if (icons == null)
            icons = new IconRandomizer[0];

        var newList = new IconRandomizer[icons.Length + 1];
        icons.CopyTo(newList, 0);
        newList[newList.Length - 1] = icon;
        icons = newList;
    }

    public static void ResetAllIcons()
    {
        if (icons == null) return;

        foreach (var icon in icons)
            icon.ResetIcon();
    }
}
