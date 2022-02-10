using System;
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
        private LoopedAction _loopedAction;

        public void SetValue(IPooler<GameObject> value)
        {
            _loopedAction = new LoopedAction();
            _loopedAction.DoAction += ResetToDefault;
            _loopedAction.Begin(_lifeTime);
        
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
            _loopedAction.EndLoop();
            gameObject.SetActive(false);
            _pooler.Return(gameObject);
        }

        public void OnButtonClicked()
        {
            ResetToDefault();
            ButtonClicked?.Invoke();
        }
    }
}
