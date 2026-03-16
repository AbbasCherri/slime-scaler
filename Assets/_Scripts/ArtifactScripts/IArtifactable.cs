using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.ArtifactScripts
{
    public interface IArtifactable
    {
        public void Ability();
        public void Deactivate();
    }
}