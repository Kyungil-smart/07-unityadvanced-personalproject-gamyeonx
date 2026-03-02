using UnityEngine;
using UnityEngine.UI;

public class ExeSilder : MonoBehaviour
{
    [SerializeField] private Slider _exeSlider;

    public void SetEXE(float current, float max)
    {
        float percent = current / max;
        _exeSlider.value = percent;
    }
}
