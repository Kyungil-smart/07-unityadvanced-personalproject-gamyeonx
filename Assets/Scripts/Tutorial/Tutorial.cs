using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private AudioSource _uiSource;
    [SerializeField] private AudioClip _uiClickSfx;

    private Button NewGame;

    private void Awake()
    {
        NewGame = transform.Find("NewGame")?.GetComponent<Button>();

        if (NewGame != null)
            NewGame.onClick.AddListener(OnClickNewGame);
    }

    public void OnClickNewGame()
    {
        _uiSource.PlayOneShot(_uiClickSfx);
        Invoke(nameof(Reload), 0.2f);
    }

    private void Reload()
    {
        SceneManager.LoadScene(2);
    }
}
