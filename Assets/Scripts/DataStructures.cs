namespace ChickenRoast
{
    [System.Serializable]
    public class FireData
    {
        public int startIntensity;
        public int maxIntensity = 100;
        public int intensityIncreaseRate = 5;
        public float intensityDecreaseTimeRate = 0.5f;
    }

    [System.Serializable]
    public class ChickenData
    {
        public int startCookingValue = 200;
        public int startBurnValue = 400;
        public int maxBurnValue = 500;
    }
}