using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReStart : MonoBehaviour
{
    [SerializeField] private SilderBarUI _playerUI;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickGameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
