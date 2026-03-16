using System;
using _Scripts.PlayerScripts;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.ArtifactScripts
{
    public class SwiftyArtifact : MonoBehaviour, IArtifactable
    {
        [SerializeField] private ArtifactScriptableObject artifact;


        private void OnEnable()
        {
            Ability();
        }

        private void OnDisable()
        {
            Deactivate();
        }

        public void Ability()
        {
            PlayerMovement.GetInstance().SetSpeed(1.2f);
        }

        public void Deactivate()
        {
            PlayerMovement.GetInstance().SetSpeed(1f/1.2f);
        }
    }
}
