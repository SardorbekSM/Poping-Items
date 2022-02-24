using System;

using UnityEngine;
using UnityEngine.UI;

using Core;

namespace View
{
    public class ItemView : MonoBehaviour, IClickBehaviour
    {
        [SerializeField] private Button _itemButton;
        [SerializeField] private Transform _patternPlace;
        
        public InteractableType PatternType { get; private set; }
        public event Action ButtonClicked;
        public event Action Reseted;

        public void ChangePosition(Vector2 newPosition)
        {
            transform.position = newPosition;
        }
        
        public void ChangePattern(GameObject pattern, InteractableType type)
        {
            PatternType = type;
            pattern.transform.SetParent(_patternPlace);
            pattern.transform.localPosition = Vector3.zero;
        }

        public void ResetToDefault()
        {
            Reseted?.Invoke();
            ButtonClicked = delegate { };
        }
        
        private void OnEnable()
        {
            _itemButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            ButtonClicked?.Invoke();
            ResetToDefault();
        }

        private void OnDestroy()
        {
            Reseted?.Invoke();
        }

        private void OnDisable()
        {
            _itemButton.onClick.RemoveAllListeners();
        }
    }
}
