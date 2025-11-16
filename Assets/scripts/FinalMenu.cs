using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalMenu : MonoBehaviour
{
    public static FinalMenu Instance;

    public GameObject rootPanel;
    public Image resultImage;
    public Sprite[] resultSprites;

    private void Awake()
    {
        Instance = this;
        rootPanel.SetActive(false);
    }

    public void ShowResult(int count)
    {
        if (count < resultSprites.Length)
            resultImage.sprite = resultSprites[count];
        else
            resultImage.sprite = resultSprites[resultSprites.Length - 1];

        rootPanel.SetActive(true);
    }

    // метод кнопки "Перезапуск"
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
