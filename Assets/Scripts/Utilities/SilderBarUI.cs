using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SilderBarUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fillImage;

    private float _maxHP = 100f;
    private float _currentHP = 100f;

    void Update()
    {
        // 테스트용: 키 누르면 HP 감소
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _currentHP -= 20f;
            Debug.Log($"currentHP: {_currentHP}");  // HP 줄어드는지
            SetHP(_currentHP, _maxHP);
        }
    }

    public void SetHP(float current, float max)
    {
        float percent = current / max;
        Debug.Log($"percent: {percent}");
        _slider.value = percent;
        Debug.Log($"slider.value: {_slider.value}"); // 슬라이더 값 들어가는지
        Debug.Log($"fillImage: {_fillImage}");

        if (percent > 0.6f)
            _fillImage.color = Color.green;
        else if (percent > 0.3f)
            _fillImage.color = Color.orange;
        else
            _fillImage.color = Color.red;
    }
}
