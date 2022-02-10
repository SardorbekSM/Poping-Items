using Control;
using Core.Position;
using Core.Spawner;
using Data;
using Model;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;
using View;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LoopedSpawnControl _spawnControl;
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private EndGameView _endGameView;
    
    //Data
    [SerializeField] private SpawnData _spawnData;
    [SerializeField] private FillBarData _fillBarData;
    [SerializeField] private ItemsOffsetData _itemsOffsetData;

    protected override void Configure(IContainerBuilder builder)
    {
        BindCore(builder);
        BindComponents(builder);
        BindInstance(builder, _spawnData);
        BindInstance(builder, _fillBarData);
        BindInstance(builder, _itemsOffsetData);
    }

    private void BindComponents(IContainerBuilder builder)
    {
        builder.RegisterInstance(_mainCamera).AsSelf();
        builder.RegisterComponent(_spawnControl).AsSelf();
        builder.RegisterComponent(_sliderView).AsSelf();
        builder.RegisterComponent(_endGameView).AsSelf();
    }
    
    private void BindCore(IContainerBuilder builder)
    {
        builder.Register<UniqueValueRandomizer>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ItemPosition>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<SpawnerWithPool>(Lifetime.Singleton);
        builder.Register<SliderModel>(Lifetime.Singleton);
        builder.Register<SpawnModel>(Lifetime.Singleton);
        builder.Register<ScoreControl>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SliderControl>();
        builder.RegisterEntryPoint<ItemControl>();
    }
    
    private void BindInstance<T>(IContainerBuilder builder, T instance)
    {
        builder.RegisterInstance(instance);
    }
}
