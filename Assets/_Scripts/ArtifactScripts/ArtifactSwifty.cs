using _Scripts.PlayerScripts;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.ArtifactScripts
{
    [CreateAssetMenu(menuName = "Artifact/Swifty")]
    public class ArtifactSwifty : ArtifactScriptableObject
    {
        private const float SpeedIncrease = 1.2f;

        public override void ApplyEffect(GameObject owner)
        {
            PlayerMovement.GetInstance().SetSpeed(SpeedIncrease);
        }

        public override void RemoveEffect(GameObject owner)
        {
            PlayerMovement.GetInstance().ResetSpeed();
        }
    }
}