using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
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
        StartCoroutine(Reload());
    }

    public void OnClickGameQuit()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
        StartCoroutine(QuitGame());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator QuitGame()
    {
        yield return new WaitForSecondsRealtime(0.2f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
