using System;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;


namespace ChickenRoast.Chicken
{
    public enum ChickenSideDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public class ChickenController : MonoBehaviour
    {
        public Transform skewer;
        public GameObject rawChicken;
        public GameObject burnedChicken;
        public List<CustomScrollBar> outSideSliders;
        public Slider insideSlider;

        private Func<int> getFireIntensity;
        private ChickenRotateHandler rotateHandler;
        private ChickenCookHandler cookHandler;

        private int currentSideValue = 1;
        private ChickenSideDirection CurrentSide => (ChickenSideDirection) (currentSideValue % 4);

        public void Setup(ChickenData chickenConfig, Func<int> getFireIntensity, Action onChickenCooked, Action onChickenBurned)
        {
            this.getFireIntensity = getFireIntensity;
            rotateHandler = new ChickenRotateHandler(skewer, onRotateComplete: UpdateCurrentSide);
            cookHandler = new ChickenCookHandler(chickenConfig, outSideSliders, insideSlider, onChickenBurned: HandleChickenBurn, onChickenCooked);
            enabled = true;

            void HandleChickenBurn()
            {
                rawChicken.SetActive(false);
                burnedChicken.SetActive(true);
                onChickenBurned?.Invoke();
            }
        }

        void UpdateCurrentSide(int direction)
        {
            currentSideValue += direction;
            if (currentSideValue < 0)
                currentSideValue = 4;
        }

        private void Update()
        {
            rotateHandler.HandleRotateInputs();
            cookHandler.CookCurrentSideOfChicken(getFireIntensity(), CurrentSide);
            cookHandler.CookInsideOfChicken(getFireIntensity());
        }
    }
}