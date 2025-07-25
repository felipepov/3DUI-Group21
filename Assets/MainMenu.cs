using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    public Button optionsButton;
    public Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(OnPlayClicked);
        optionsButton.onClick.AddListener(OnOptionsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    void OnPlayClicked()
    {
        Debug.Log("Play button clicked");
        SceneManager.LoadScene("Game"); // Replace with your scene name
    }

    void OnOptionsClicked()
    {
        Debug.Log("Options button clicked");
        // Show options panel, for example
    }

    void OnQuitClicked()
    {
        Debug.Log("Quit button clicked");
        Application.Quit();

        // In the editor, simulate quit:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
