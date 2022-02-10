using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private float _fadeTime;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Text _titleText;

        private Action _restartButtonCallback;
        
        private void Awake()
        {
            Hide();
            
            _restartButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Hide();
            _restartButtonCallback?.Invoke();
        }
        
        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(OnButtonClick);
        }
        
        public void Activate(Action buttonClickCallback)
        {
            _restartButtonCallback += buttonClickCallback;

            StartFadeAnimation();
            Show();
        }
        
        private void StartFadeAnimation()
        {
            GraphicFade(_backgroundImage, _backgroundImage.color.a);
            GraphicFade(_titleText);
            GraphicFade(_restartButton.image);
        }

        private void GraphicFade(Graphic graphic, float alpha = 1f)
        {
            graphic.DOFade(0f, 0f);
            graphic.DOFade(alpha, _fadeTime);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}