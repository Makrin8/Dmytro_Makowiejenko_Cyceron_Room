using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerManager : MonoBehaviour
{
    public Image timerImage;
    public Sprite[] secondSprites; // 0..59
    public Sprite endSprite00;     // 00:00
    public int duration = 60;
    public bool isSecondStage = false; // переключение этапа

    void Start()
    {
        StartCoroutine(TimerRoutine());
    }

    IEnumerator TimerRoutine()
    {
        int second = 0;

        while (second < duration)
        {
            timerImage.sprite = secondSprites[second];
            yield return new WaitForSeconds(1f);
            second++;
        }

        timerImage.sprite = endSprite00;

        if (isSecondStage)
        {
            int correctAnswers = MatchManager.CountCorrect();
            FinalMenu.Instance.ShowResult(correctAnswers);
            yield break;
        }
        else
        {
            // первый этап завершён, сброс всех иконок
            IconsResetManager.ResetAllIcons();
            yield return new WaitForSeconds(1f);
            StartCoroutine(TimerRoutine());
        }
    }
}
