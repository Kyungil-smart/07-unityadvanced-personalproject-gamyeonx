using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReStart : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSource;
    [SerializeField] private AudioClip _uiClickSfx;

    private Button retryButton;
    private Button quitButton;

    void Awake()
    {
        retryButton = transform.Find("ReButton")?.GetComponent<Button>();
        quitButton = transform.Find("OffButton")?.GetComponent<Button>();

        if (retryButton != null)
            retryButton.onClick.AddListener(OnClickRetryGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(OnClickGameQuit);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnClickRetryGame()
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
