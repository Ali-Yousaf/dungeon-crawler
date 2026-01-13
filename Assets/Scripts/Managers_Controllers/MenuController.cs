using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject heartPanel;
    public GameObject coinCounter;
    public GameObject keyCounter;


    private void Start()
    {
        menuCanvas.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);

            heartPanel.SetActive(!heartPanel.activeSelf);
            coinCounter.SetActive(!coinCounter.activeSelf);
            keyCounter.SetActive(!keyCounter.activeSelf);
        }
    }
}
