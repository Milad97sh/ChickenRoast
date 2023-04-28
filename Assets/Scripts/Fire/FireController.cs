using System;
using ChickenRoast;
using UnityEngine;
using UnityEngine.UI;


namespace ChickenRoast.Fire
{
    public class FireController : MonoBehaviour
    {
        public ParticleSystem fire;
        public Slider fireSlider;

        private int intensity;
        private FireData fireData;
        private Action onFireShutDown;
        private float timer;

        public void Setup(FireData fireData, Action onFireShutDown)
        {
            this.fireData = fireData;
            this.onFireShutDown = onFireShutDown;
            enabled = true;
        }

        private void Start()
        {
            intensity = fireData.startIntensity;
            fireSlider.maxValue = fireData.maxIntensity;
        }

        private void Update()
        {
            HandleFireIntensityInput();
            UpdateIntensityWithTime();
        }

        private void HandleFireIntensityInput()
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
                BlowAirIntoFire();
        }

        private void BlowAirIntoFire()
        {
            intensity += fireData.intensityIncreaseRate;
            if (intensity > fireData.maxIntensity)
                intensity = fireData.maxIntensity;
            UpdateFirePresentation();
        }

        private void UpdateIntensityWithTime()
        {
            if (timer < fireData.intensityDecreaseTimeRate)
                timer += Time.deltaTime;
            else
            {
                intensity--;
                UpdateFirePresentation();
                timer = 0;
                if (intensity <= 0)
                    FireShutDown();
            }
        }

        private void FireShutDown()
        {
            onFireShutDown.Invoke();
            enabled = false;
        }

        private void UpdateFirePresentation()
        {
            var fireSizeBaseOnIntensity = (float) intensity / fireData.maxIntensity;
            fire.transform.localScale = new Vector3(1, fireSizeBaseOnIntensity, 1);
            fireSlider.value = intensity;
        }

        public int GetIntensity() => intensity;
    }
}