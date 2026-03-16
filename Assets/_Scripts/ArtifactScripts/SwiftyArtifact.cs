using System;
using _Scripts.PlayerScripts;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.ArtifactScripts
{
    public class SwiftyArtifact : MonoBehaviour, IArtifactable
    {
        [SerializeField] private ArtifactScriptableObject artifact;
        private bool _isInitialized = false;

        
        private void Start()
        {
            Ability();
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                Ability();
            }
        }

        private void OnDisable()
        {
            Deactivate();
        }

        public void Ability()
        {
            var player = PlayerMovement.GetInstance();
            if (player != null)
            {
                player.SetSpeed(1.2f);
            }
        }

        public void Deactivate()
        {
            var player = PlayerMovement.GetInstance();
            if (player != null)
            {
                player.ResetSpeed();
            }
        }
    }
}