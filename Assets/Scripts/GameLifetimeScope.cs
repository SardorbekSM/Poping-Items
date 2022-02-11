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
        builder.RegisterComponent(_sliderView).AsSelf();
        builder.RegisterComponent(_endGameView).AsSelf();
    }
    
    private void BindCore(IContainerBuilder builder)
    {
        builder.Register<UniqueValueRandomizer>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PositionGetter>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ScoreControl>(Lifetime.Singleton).AsImplementedInterfaces();
        
        builder.Register<SpawnerWithPool>(Lifetime.Singleton).AsSelf();
        builder.Register<SliderModel>(Lifetime.Singleton).AsSelf();
        builder.Register<SpawnModel>(Lifetime.Singleton).AsSelf();
        builder.Register<SpawnControl>(Lifetime.Singleton).AsSelf();
        builder.Register<SliderControl>(Lifetime.Singleton).AsSelf();

        builder.RegisterEntryPoint<ItemControl>();
        builder.RegisterEntryPoint<GamePrecess>();
    }
    
    private void BindInstance<T>(IContainerBuilder builder, T instance)
    {
        builder.RegisterInstance(instance);
    }
}
