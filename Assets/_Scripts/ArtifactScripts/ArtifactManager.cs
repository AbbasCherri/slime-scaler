using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.ArtifactScripts
{
    public class ArtifactManager : MonoBehaviour
    {
        public ArtifactScriptableObject equippedArtifact;
        private ArtifactManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            _instance = this;
        }

        private void Start()
        {
            if (equippedArtifact != null)
            {
                equippedArtifact.ApplyEffect(gameObject);
            }
        }

        public void SwapArtifact(ArtifactScriptableObject newArtifact)
        {
            if (equippedArtifact != null) 
                equippedArtifact.RemoveEffect(this.gameObject);

            equippedArtifact = newArtifact;
            equippedArtifact.ApplyEffect(this.gameObject);
        }
    }
}