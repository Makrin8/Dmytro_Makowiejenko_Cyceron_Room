using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SceneInitializer : MonoBehaviour
{
    [Header("Player Settings")]
    public Vector3 playerStartPos = new Vector3(0, 1, 0);
    public Vector3 minBounds = new Vector3(0, 2, 0);
    public Vector3 maxBounds = new Vector3(5, 5, 12);

    [Header("Icon Settings")]
    public int iconCount = 6;
    public Sprite[] iconSprites;       // Спрайты для первого этапа
    public Sprite emptyIcon;
    public Sprite[] correctSprites;    // Спрайты для второго этапа

    [Header("Timer Settings")]
    public Sprite[] secondSprites;
    public Sprite endSprite00;
    public int timerDuration = 60;

    [Header("Final Menu Settings")]
    public Sprite[] finalResultSprites;

    [Header("Selection Menu Settings")]
    public Sprite[] choiceSprites;

    void Start()
    {
        CreatePlayer();
        CreateIcons();
        CreateCanvas();
    }

    void CreatePlayer()
    {
        GameObject player = new GameObject("Player");
        player.transform.position = playerStartPos;

        CharacterController cc = player.AddComponent<CharacterController>();

        GameObject camHolder = new GameObject("CameraHolder");
        camHolder.transform.parent = player.transform;
        camHolder.transform.localPosition = Vector3.zero;

        GameObject mainCam = new GameObject("Main Camera");
        Camera cam = mainCam.AddComponent<Camera>();
        mainCam.transform.parent = camHolder.transform;
        mainCam.transform.localPosition = Vector3.zero;

        FpsController fps = player.AddComponent<FpsController>();
        fps.cameraHolder = camHolder.transform;
        fps.minBounds = minBounds;
        fps.maxBounds = maxBounds;
    }

    void CreateIcons()
    {
        GameObject iconParent = new GameObject("IconsParent");
        GameObject iconPoints = new GameObject("IconPoints");

        for (int i = 0; i < iconCount; i++)
        {
            GameObject point = new GameObject("IconPoint_" + (i + 1));
            point.transform.parent = iconPoints.transform;
            point.transform.position = new Vector3(i * 1.5f, 1f, 0); // простое размещение по линии

            GameObject icon = new GameObject("IconPrefab_" + (i + 1));
            icon.transform.parent = iconParent.transform;
            icon.transform.position = point.transform.position;

            Image img = icon.AddComponent<Image>();
            IconRandomizer ir = icon.AddComponent<IconRandomizer>();
            ir.possibleSprites = iconSprites;
            ir.emptyIcon = emptyIcon;

            MatchIcon mi = icon.AddComponent<MatchIcon>();
            if (i < correctSprites.Length) mi.correctSprite = correctSprites[i];

            Billboard bb = icon.AddComponent<Billboard>();

            Button btn = icon.AddComponent<Button>();
            btn.onClick.AddListener(mi.OnClickIcon);
        }
    }

    void CreateCanvas()
    {
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<StandaloneInputModule>();
        }

        // Timer
        GameObject timerGO = new GameObject("TimerIcon");
        timerGO.transform.parent = canvasGO.transform;
        Image timerImg = timerGO.AddComponent<Image>();
        TimerManager tm = timerGO.AddComponent<TimerManager>();
        tm.timerImage = timerImg;
        tm.secondSprites = secondSprites;
        tm.endSprite00 = endSprite00;
        tm.duration = timerDuration;
        tm.isSecondStage = false;

        // SelectionMenu
        GameObject selMenu = new GameObject("SelectionMenu");
        selMenu.transform.parent = canvasGO.transform;
        selMenu.SetActive(false);
        SelectionMenu sm = selMenu.AddComponent<SelectionMenu>();
        sm.menuRoot = selMenu;

        List<Image> choices = new List<Image>();
        for (int i = 0; i < choiceSprites.Length; i++)
        {
            GameObject btnGO = new GameObject("ChoiceButton_" + (i + 1));
            btnGO.transform.parent = selMenu.transform;
            Image img = btnGO.AddComponent<Image>();
            img.sprite = choiceSprites[i];
            Button b = btnGO.AddComponent<Button>();
            int index = i; // локальная копия для замыкания
            b.onClick.AddListener(() => sm.ChooseOption(index));
            choices.Add(img);
        }
        sm.choiceButtons = choices.ToArray();

        // FinalMenu
        GameObject finalMenuGO = new GameObject("FinalMenu");
        finalMenuGO.transform.parent = canvasGO.transform;
        finalMenuGO.SetActive(false);
        FinalMenu fm = finalMenuGO.AddComponent<FinalMenu>();
        fm.rootPanel = finalMenuGO;

        GameObject resultImgGO = new GameObject("ResultImage");
        resultImgGO.transform.parent = finalMenuGO.transform;
        Image resultImg = resultImgGO.AddComponent<Image>();
        fm.resultImage = resultImg;
        fm.resultSprites = finalResultSprites;

        GameObject restartBtnGO = new GameObject("RestartButton");
        restartBtnGO.transform.parent = finalMenuGO.transform;
        Button restartBtn = restartBtnGO.AddComponent<Button>();
        restartBtn.onClick.AddListener(fm.RestartGame);
    }
}
