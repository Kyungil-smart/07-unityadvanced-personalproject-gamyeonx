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
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _currentHP -= 20f;
            SetHP(_currentHP, _maxHP);
        }
    }

    public void SetHP(float current, float max)
    {
        float percent = current / max;
        _slider.value = percent;

        if (percent > 0.6f)
            _fillImage.color = Color.green;
        else if (percent > 0.3f)
            _fillImage.color = Color.orange;
        else
            _fillImage.color = Color.red;
    }
}
