using Data;

namespace Model
{
    public class LevelModel
    {
        private readonly LevelData _levelData;
        
        public int StartValue => _levelData.StartValue;
        public int LevelItemsCount => _levelData.LevelRequiredItemsCount;
        public int IterationItems => _levelData.IterationItemsCount;

        public LevelModel(LevelData levelData)
        {
            _levelData = levelData;
        }
    }
}