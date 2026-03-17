using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    public abstract class ArtifactScriptableObject : ScriptableObject
    {
        public string artifactName;
        public Sprite icon;
        
        public abstract void ApplyEffect(GameObject owner);
        public abstract void RemoveEffect(GameObject owner);
    }
}
