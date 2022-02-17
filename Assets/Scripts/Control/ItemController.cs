using Core.Spawner;
using Core.Spawner.Interfaces;
using Data;
using Model;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using View;

namespace Control
{
    public class ItemController : IStartable
    {
        private readonly IPositionGetter _positionGetter;
        private readonly ISpawnerBehaviour _spawnerWithPool;
        private readonly PatternModel _patternModel;

        public ItemController(IPositionGetter positionGetter, ISpawnerBehaviour spawnerWithPool, PatternModel patternModel)
        {
            _positionGetter = positionGetter;
            _spawnerWithPool = spawnerWithPool;
            _patternModel = patternModel;
        }

        public void Start()
        {
            _spawnerWithPool.OnInstantiatedObject += OnSpawned;
        }

        private void OnSpawned(GameObject obj)
        {
            var item = obj.GetComponent<ItemView>();
            //var pattern = obj.GetComponentInChildren<PatternView>();

            var newPattern = _patternModel.GetPattern(out var type);
            
            Assert.IsNotNull(item);

            item.ChangePosition(_positionGetter.GetRandom());
            //pattern.ChangePattern(newPattern, type);
            item.ChangePattern(newPattern, type);

        }
    }
}