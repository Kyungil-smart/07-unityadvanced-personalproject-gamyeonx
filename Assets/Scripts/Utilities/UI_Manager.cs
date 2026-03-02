using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    [SerializeField] private SilderBarUI _hpUI;
    [SerializeField] private MpSilder _mpUI;
    [SerializeField] private ExpSlider _expUI;
    [SerializeField] private TMP_Text _levelText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    public void UpdatePlayerHP(float current, float max)
    {
        _hpUI.SetHP(current, max);
    }
    public void UpdatePlayerMP(float current, float max)
    {
        _mpUI.SetMP(current, max);
    }
    public void UpdatePlayerEXP(float current, float max)
    {
        _expUI.SetEXP(current, max);
    }

    public void UpdateLevel(int Lv)
    {
        _levelText.text = "Lv " + Lv;
    }
}
