using Data;

namespace Model
{
    public class SliderModel
    {
        public float FillMax => _fillBarData.FillMax;
        public float Step => _fillBarData.Step;

        private readonly FillBarData _fillBarData;

        public SliderModel(FillBarData fillBarData)
        {
            _fillBarData = fillBarData;
        }
    }
}