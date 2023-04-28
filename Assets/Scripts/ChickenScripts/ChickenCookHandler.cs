using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace ChickenRoast.Chicken
{
    public class ChickenCookHandler
    {
        private Dictionary<ChickenSideDirection, float> chickenOutSides;
        private readonly ChickenData chickenConfig;
        private float insideHeat;
        private readonly Action onChickenBurned;
        private readonly Action onChickenCooked;
        private readonly List<CustomScrollBar> outSideSliders;
        private readonly Slider insideSlider;

        public ChickenCookHandler(ChickenData chickenConfig, List<CustomScrollBar> outSideSliders, Slider insideSlider, Action onChickenBurned, Action onChickenCooked)
        {
            this.chickenConfig = chickenConfig;
            this.outSideSliders = outSideSliders;
            this.insideSlider = insideSlider;
            this.onChickenBurned = onChickenBurned;
            this.onChickenCooked = onChickenCooked;

            InitialChickenSides();
            InitialUIBars();

            void InitialChickenSides()
            {
                chickenOutSides = new Dictionary<ChickenSideDirection, float>(4)
                                  {
                                      {ChickenSideDirection.Up, 0},
                                      {ChickenSideDirection.Right, 0},
                                      {ChickenSideDirection.Down, 0},
                                      {ChickenSideDirection.Left, 0}
                                  };
            }

            void InitialUIBars()
            {
                foreach (CustomScrollBar outSideSlider in outSideSliders)
                    outSideSlider.Setup(chickenConfig.startCookingValue, chickenConfig.startBurnValue, chickenConfig.maxBurnValue);
                insideSlider.maxValue = this.chickenConfig.startCookingValue * 4;
            }
        }

        public void CookCurrentSideOfChicken(int fireIntensity, ChickenSideDirection currentSide)
        {
            chickenOutSides[currentSide] += fireIntensity * Time.deltaTime;
            outSideSliders[(int) currentSide].UpdateBar(chickenOutSides[currentSide]);

            if (chickenOutSides[currentSide] >= chickenConfig.startBurnValue)
            {
                onChickenBurned?.Invoke();
                return;
            }

            if (chickenOutSides[currentSide] > chickenConfig.startCookingValue)
                if (IsChickenCooked())
                    onChickenCooked?.Invoke();
        }

        public void CookInsideOfChicken(int fireIntensity)
        {
            insideHeat += fireIntensity * Time.deltaTime;
            insideSlider.value = insideHeat;
        }

        private bool IsChickenCooked()
        {
            return IsAllSideOfChickenCooked() && IsInsideOfChickenCooked();

            bool IsAllSideOfChickenCooked() => chickenOutSides.Values.All(sideHeat => sideHeat >= chickenConfig.startCookingValue);
            bool IsInsideOfChickenCooked() => insideHeat >= chickenConfig.startCookingValue * 4;
        }
    }
}