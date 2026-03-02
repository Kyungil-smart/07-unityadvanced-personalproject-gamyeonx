using UnityEngine;
using UnityEngine.UI;


public class ExpSlider : MonoBehaviour
{
    [SerializeField] private Slider _expSlider;

    public void SetEXP(float current, float max)
    {
        float percent = current / max;
        _expSlider.value = percent;
    }
}
