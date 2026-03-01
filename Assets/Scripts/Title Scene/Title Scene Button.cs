using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TitleSceneButton : MonoBehaviour
{
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
            NewGame.onClick.AddListener(OnClickCredits);

        if (Exit != null)
            Exit.onClick.AddListener(OnClickExit);
    }
    public void OnClickNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OnClickLoadGame()
    {
    }

    public void OnClickCredits()
    {
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }
}
