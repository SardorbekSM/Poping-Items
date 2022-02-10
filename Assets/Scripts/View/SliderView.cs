using Control;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace View
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        [Inject]
        public void Inject(ScoreControl scoreControl)
        {
            _slider.onValueChanged.AddListener(scoreControl.OnScoreChanged);
        }

        public void ChangeValue(float value)
        {
            _slider.value += value;
        }

        public void ChangeSliderMaxCount(float maxValue)
        {
            _slider.maxValue = maxValue;
        }
    }
}
