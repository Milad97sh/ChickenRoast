using UnityEngine;


namespace ChickenRoast
{
    [CreateAssetMenu(fileName = "New Roast Chicken Data",menuName = "Roast Chicken Data")]
    public class Config : ScriptableObject
    {
        public FireData fireData;
        public ChickenData chickenData;
    }
}