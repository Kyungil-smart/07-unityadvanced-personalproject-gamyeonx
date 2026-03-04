using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleSceneButton : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSource;
    [SerializeField] private AudioClip _uiClickSfx;

    private Button NewGame;
    private Button LoadGame;
    private Button Credits;
    private Button Exit;

    private void Awake()
    {
        NewGame = transform.Find("NewGame")?.GetComponent<Button>();
        LoadGame = transform.Find("LoadGame")?.GetComponent<Button>();
        Credits = transform.Find("Credits")?.GetComponent<Button>();
        Exit = transform.Find("Exit")?.GetComponent<Button>();

        if (NewGame != null)
            NewGame.onClick.AddListener(OnClickNewGame);

        if (LoadGame != null)
            LoadGame.onClick.AddListener(OnClickLoadGame);

        if (Credits != null)
            Credits.onClick.AddListener(OnClickCredits);

        if (Exit != null)
            Exit.onClick.AddListener(OnClickExit);
    }
    public void OnClickNewGame()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
        Invoke(nameof(Reload), 0.2f);
    }

    public void OnClickLoadGame()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
    }

    public void OnClickCredits()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
        Invoke(nameof(EndCredits), 0.2f);
    }

    public void OnClickExit()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
        Invoke(nameof(QuitGame), 0.2f);
    }

    private void Reload()
    {
        SceneManager.LoadScene(1);
    }

    private void EndCredits()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
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
