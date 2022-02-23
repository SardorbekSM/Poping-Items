using System;
using Core;
using Core.WaiterAsync;
using Data;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ItemView : MonoBehaviour, ISpawnableObject<IPooler<GameObject>>, IClickBehaviour
    {
        [SerializeField] private Button _itemButton;
        [SerializeField] private Transform _patternPlace;
        private IPooler<GameObject> _pooler;
        
        public InteractableType PatternType { get; private set; }
        public event Action ButtonClicked;
        public event Action Reseted;

        private void OnEnable()
        {
            _itemButton.onClick.AddListener(OnButtonClicked);
        }

        public void SetValue(IPooler<GameObject> value)
        {
            gameObject.SetActive(true);

            _pooler = value;
        }

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
            _pooler.Return(gameObject);
            gameObject.SetActive(false);
            Destroy(_patternPlace.GetChild(0).gameObject);
            
            Reseted?.Invoke();
            ButtonClicked = delegate { };
        }

        public void OnButtonClicked()
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
