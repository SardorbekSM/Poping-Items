using System.Collections.Generic;
using Core.Randomizer;
using UnityEngine;

namespace Core.Spawner
{
    public class RandomizerPooler : Pooler
    {
        private readonly IRandomizer _randomizer;
        
        public RandomizerPooler(IEnumerable<GameObject> poolingObjects, IRandomizer randomizer) : base(poolingObjects)
        {
            _randomizer = randomizer;
        }
        
        protected override int GetIndex(int maxValue)
        {
            _randomizer.SetMax(maxValue);
            
            return _randomizer.GetIndex();
        }
    }
}
