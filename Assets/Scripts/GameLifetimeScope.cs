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
    [SerializeField] private ItemsData _itemsData;
    [SerializeField] private LevelData _levelData;
    [SerializeField] private ItemsOffsetData _itemsOffsetData;
    [SerializeField] private IterationPatternsData _iterationPatternsData;

    protected override void Configure(IContainerBuilder builder)
    {
        BindCore(builder);
        BindComponents(builder);
        BindInstance(builder, _itemsData);
        BindInstance(builder, _levelData);
        BindInstance(builder, _itemsOffsetData);
        BindInstance(builder, _iterationPatternsData);
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
        
        builder.Register<LevelModel>(Lifetime.Singleton).AsSelf();
        builder.Register<ItemModel>(Lifetime.Singleton).AsSelf();
        builder.Register<MainCameraBorders>(Lifetime.Singleton).AsSelf();
        builder.Register<PatternModel>(Lifetime.Singleton).AsSelf();
        builder.Register<SliderController>(Lifetime.Singleton).AsSelf();
        builder.Register<IterationModel>(Lifetime.Singleton).AsSelf();
        builder.Register<LevelController>(Lifetime.Singleton).AsSelf();
        builder.Register<ItemController>(Lifetime.Singleton).AsSelf(); // Как можно передвигать строки с помощью alt как в VS

        builder.RegisterEntryPoint<GameProcess>();
    }
    
    private void BindInstance<T>(IContainerBuilder builder, T instance)
    {
        builder.RegisterInstance(instance);
    }
}
