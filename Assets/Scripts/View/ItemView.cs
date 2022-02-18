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
        [SerializeField] private float _lifeTime;
        [SerializeField] private Transform _patternPlace;
        
        private IPooler<GameObject> _pooler;
        private ILoopedAction _loopedActionAsync;

        public InteractableType PatternType { get; private set; }
        public event Action<GameObject> ButtonClicked;

        public void SetValue(IPooler<GameObject> value)
        {
            _loopedActionAsync = new LoopedActionAsync();
            _loopedActionAsync.DoAction += ResetToDefault;
            _loopedActionAsync.Begin(_lifeTime);
        
            gameObject.SetActive(true);

            _pooler = value;
        
            _itemButton.onClick.AddListener(OnButtonClicked);
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
            _itemButton.onClick.RemoveAllListeners();
            _loopedActionAsync.EndLoop();
            _pooler.Return(gameObject);
            gameObject.SetActive(false);
            Destroy(_patternPlace.GetChild(0).gameObject);
            ButtonClicked = delegate { };
        }

        public void OnButtonClicked()
        {
            ButtonClicked?.Invoke(gameObject);
            ResetToDefault();
        }

        private void OnDisable()
        {
            _loopedActionAsync.DoAction -= ResetToDefault;
            _loopedActionAsync.EndLoop();
        }
    }
}
