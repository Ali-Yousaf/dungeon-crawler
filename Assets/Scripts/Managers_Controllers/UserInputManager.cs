using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInputManager : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField nameInput;
    public TMP_InputField ageInput;
    public TMP_InputField funFactInput;

    [Header("UI Panel")]
    public GameObject inputPanel;

    [Header("Player Menu Panel")]
    public TextMeshProUGUI MenuplayerName;
    public TextMeshProUGUI MenuplayerAge;
    public TextMeshProUGUI MenuplayerFF;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Game")
        {
            inputPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SubmitForm()
    {
        string playerName = nameInput.text;
        string playerAge = ageInput.text;
        string playerFunFact = funFactInput.text;

        Debug.Log("Name: " + playerName);
        Debug.Log("Age: " + playerAge);
        Debug.Log("Fun Fact: " + playerFunFact);

        MenuplayerName.text = "Name:  " + playerName;
        MenuplayerAge.text = "Age:  " + playerAge;
        MenuplayerFF.text = "Fun Fact:  " + playerFunFact;

        inputPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
