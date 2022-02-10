using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizerPooler : Pooler
{
    private readonly IRandomizer _randomizer;
        
    public RandomizerPooler(GameObject[] poolingObjects, IRandomizer randomizer) : base(poolingObjects)
    {
        _randomizer = randomizer;
    }
        
    protected override int GetIndex(int maxValue)
    {
        _randomizer.SetMax(maxValue);
            
        return _randomizer.GetIndex();
    }
}
