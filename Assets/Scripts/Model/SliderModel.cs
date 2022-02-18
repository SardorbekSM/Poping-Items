using Data;

namespace Model
{
    public class SliderModel
    {
        private readonly FillBarData _fillBarData;
        
        public int FillMin => _fillBarData.FillMin;
        public int FillMax => _fillBarData.FillMax;
        public int Step => _fillBarData.Step;
        public int RequiredItemsCount => _fillBarData.RequiredItemsCount;

        public SliderModel(FillBarData fillBarData)
        {
            _fillBarData = fillBarData;
        }
    }
}