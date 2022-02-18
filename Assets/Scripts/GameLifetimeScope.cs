using Control;
using Core;
using Core.CameraUtils;
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
    [SerializeField] private LevelPatternsData _levelPatternsData;

    protected override void Configure(IContainerBuilder builder)
    {
        BindCore(builder);
        BindComponents(builder);
        BindInstance(builder, _spawnData);
        BindInstance(builder, _fillBarData);
        BindInstance(builder, _itemsOffsetData);
        BindInstance(builder, _patternsData);
        BindInstance(builder, _levelPatternsData);
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
        builder.Register<SpawnerWithPool>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<GameObjectFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        
        builder.Register<SliderModel>(Lifetime.Singleton).AsSelf();
        builder.Register<ItemModel>(Lifetime.Singleton).AsSelf();
        builder.Register<MainCameraBorders>(Lifetime.Singleton).AsSelf();
        builder.Register<PatternModel>(Lifetime.Singleton).AsSelf();
        builder.Register<SliderController>(Lifetime.Singleton).AsSelf();
        builder.Register<LevelModel>(Lifetime.Singleton).AsSelf();
        builder.Register<LevelController>(Lifetime.Singleton).AsSelf();
        builder.Register<ItemController>(Lifetime.Singleton).AsSelf(); // Как можно передвигать строки с помощью alt как в VS

        builder.RegisterEntryPoint<GameProcess>();
    }
    
    private void BindInstance<T>(IContainerBuilder builder, T instance)
    {
        builder.RegisterInstance(instance);
    }
}
