using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuHandler : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject crossButton;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void HowToPlayButton()
    {
        howToPlayPanel.SetActive(true);
    }

    public void CrossButton()
    {
        howToPlayPanel.SetActive(false);
    }


    public void QuitGame()
    {
        print("App closed");
        Application.Quit();
    }
}
