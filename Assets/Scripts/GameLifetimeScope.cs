using Control;
using Core;
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
    [SerializeField] private SliderView _sliderView;
    [SerializeField] private EndGameView _endGameView;
    
    //Data
    [SerializeField] private SpawnData _spawnData;
    [SerializeField] private FillBarData _fillBarData;
    [SerializeField] private ItemsOffsetData _itemsOffsetData;
    [SerializeField] private PatternsData _patternsData;

    protected override void Configure(IContainerBuilder builder)
    {
        BindCore(builder);
        BindComponents(builder);
        BindInstance(builder, _spawnData);
        BindInstance(builder, _fillBarData);
        BindInstance(builder, _itemsOffsetData);
        BindInstance(builder, _patternsData);
    }

    private void BindComponents(IContainerBuilder builder)
    {
        builder.RegisterInstance(_mainCamera).AsSelf();
        builder.RegisterComponent(_sliderView).AsSelf();
        builder.RegisterComponent(_endGameView).AsSelf();
    }
    
    private void BindCore(IContainerBuilder builder)
    {
        builder.Register<UniqueValueRandomizer>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PositionGetter>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ScoreController>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<SpawnerWithPool>(Lifetime.Singleton).AsImplementedInterfaces();
        
        builder.Register<SliderModel>(Lifetime.Singleton).AsSelf();
        builder.Register<SpawnModel>(Lifetime.Singleton).AsSelf();
        builder.Register<PatternModel>(Lifetime.Singleton).AsSelf();
        builder.Register<SpawnController>(Lifetime.Singleton).AsSelf();
        builder.Register<SliderController>(Lifetime.Singleton).AsSelf();

        builder.RegisterEntryPoint<ItemController>();
        builder.RegisterEntryPoint<GameProcess>();
    }
    
    private void BindInstance<T>(IContainerBuilder builder, T instance)
    {
        builder.RegisterInstance(instance);
    }
}
