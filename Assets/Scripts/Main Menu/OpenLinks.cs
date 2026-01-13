using UnityEngine;

public class OpenLinks : MonoBehaviour
{
    public string ytURL = "https://www.youtube.com/channel/UC7KITlQNDJzdMbTiUMZ_K3A";
    public string itchURL = "https://monsterduke.itch.io/";
    public string instaURL = "https://google.com";

    public void OpenYouTube()
    {
        Application.OpenURL(ytURL);
    }

    public void OpenItch()
    {
        Application.OpenURL(itchURL);
    }

    public void OpenInstagram()
    {
        Application.OpenURL(instaURL);
    }
}
