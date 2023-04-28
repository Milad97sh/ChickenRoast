using UnityEngine;


namespace ChickenRoast.Data
{
    [CreateAssetMenu(fileName = "New Roast Chicken Data",menuName = "Roast Chicken Data")]
    public class DataConfig : ScriptableObject
    {
        public FireData fireData;
        public ChickenData chickenData;
    }
}