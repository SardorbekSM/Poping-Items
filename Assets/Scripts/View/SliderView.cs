using Control;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace View
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void Start()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

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

        private void OnValueChanged(float sliderValue)
        {
            Debug.Log("Value: " + sliderValue);
        }
    }
}
