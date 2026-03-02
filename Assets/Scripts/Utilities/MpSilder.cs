using UnityEngine;
using UnityEngine.UI;

public class MpSilder : MonoBehaviour
{
    [SerializeField] private Slider _mpSlider;

    public void SetMP(float current, float max)
    {
        float percent = current / max;
        _mpSlider.value = percent;
    }
}
