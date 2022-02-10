using System;
using System.Threading;
using Core.Position;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class ItemView : MonoBehaviour, ISpawnableObject<IPooler<GameObject>>, IClickBehaviour
{
    [SerializeField] private Button _itemButton;
    [SerializeField] private float _lifeTime;

    public event Action ButtonClicked;

    private IPooler<GameObject> _pooler;
    private CancellationTokenSource _cancellation;

    public void SetValue(IPooler<GameObject> value)
    {
        _cancellation = new CancellationTokenSource();
        
        gameObject.SetActive(true);

        _pooler = value;
        
        _itemButton.onClick.AddListener(OnButtonClicked);

        Begin(_cancellation.Token);
    }
    
    private async UniTask Begin(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime), cancellationToken: token);
        ResetToDefault();
    }

    public void ChangePosition(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    public void ResetToDefault()
    {
        _cancellation.Dispose();
        gameObject.SetActive(false);
        _pooler.Return(gameObject);
    }

    public void OnButtonClicked()
    {
        ResetToDefault();
        ButtonClicked?.Invoke();
    }
}
