using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingButton : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSource;
    [SerializeField] private AudioClip _uiClickSfx;

    private Button titleButton;
    private Button quitButton;

    void Awake()
    {
        titleButton = transform.Find("BackButton")?.GetComponent<Button>();
        quitButton = transform.Find("PowerButton")?.GetComponent<Button>();

        if (titleButton != null)
            titleButton.onClick.AddListener(OnClickTitle);

        if (quitButton != null)
            quitButton.onClick.AddListener(OnClickGameQuit);
    }

    public void OnClickTitle()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
        Invoke(nameof(Reload), 0.2f);
    }

    public void OnClickGameQuit()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
        Invoke(nameof(QuitGame), 0.2f);
    }

    private void Reload()
    {
        SceneManager.LoadScene(0);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
