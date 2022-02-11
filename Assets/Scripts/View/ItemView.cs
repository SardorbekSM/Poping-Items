using System;
using Core.WaiterAsync;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ItemView : MonoBehaviour, ISpawnableObject<IPooler<GameObject>>, IClickBehaviour
    {
        [SerializeField] private Button _itemButton;
        [SerializeField] private float _lifeTime;
    
        public event Action ButtonClicked;

        private IPooler<GameObject> _pooler;
        private LoopedActionAsync _loopedActionAsync;

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

        public void ResetToDefault()
        {
            _loopedActionAsync.EndLoop();
            _pooler.Return(gameObject);
            gameObject.SetActive(false);
        }

        public void OnButtonClicked()
        {
            ResetToDefault();
            ButtonClicked?.Invoke();
        }
    }
}
