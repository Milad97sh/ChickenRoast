using UnityEngine;
using UnityEngine.UI;


namespace ChickenRoast
{
    public class CustomScrollBar : MonoBehaviour
    {
        public Scrollbar scrollbar;
        public Image splitter1;
        public Image splitter2;
        public int scrollbarSizeOffset;

        private int maxBurnValue;

        public void Setup(int startCookingValue, int startBurnValue, int maxBurnValue)
        {
            scrollbar.value = 0;
            this.maxBurnValue = maxBurnValue;

            var scrollbarSize = scrollbar.GetComponent<RectTransform>().rect.width - scrollbarSizeOffset;
            EvaluateAndDefineBarSplitterSize(splitter1, startCookingValue);
            EvaluateAndDefineBarSplitterSize(splitter2, startBurnValue);

            void EvaluateAndDefineBarSplitterSize(Image splitter, int targetSize)
            {
                var value = (targetSize * scrollbarSize) / maxBurnValue;
                splitter.rectTransform.sizeDelta = new Vector2(value, splitter.rectTransform.sizeDelta.y);
            }
        }

        public void UpdateBar(float value)
        {
            var v = value / maxBurnValue;
            scrollbar.value = v;
        }
    }
}