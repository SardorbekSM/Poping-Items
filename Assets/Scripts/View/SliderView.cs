using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void ChangeValue(float value)
        {
            _slider.value = value;
        }

        public void CreateSliderSpan(float minValue, float maxValue)
        {
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
            ChangeValue(minValue);
        }
    }
}
