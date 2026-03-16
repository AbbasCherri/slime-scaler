using _Scripts.ArtifactScripts;
using _Scripts.PlayerScripts.ArtifactScripts;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Artifact", fileName = "Artifact ScriptableObject", order = 0)]
    public class ArtifactScriptableObject : ScriptableObject
    {
        [Header("Artifact Settings")]
        [SerializeField]private string artifactName;
        [SerializeField] private ArtifactRarity artifactRarity;
        [SerializeField] private string artifactFlavorText;
        [SerializeField] private string artifactDescription;

        public string GetArtifactName()
        {
            return artifactName;
        }

        public ArtifactRarity GetArtifactRarity()
        {
            return artifactRarity;
        }

        public string GetArtifactFlavorText()
        {
            return artifactFlavorText;

        }

        public string GetArtifactDescription()
        {
            return artifactDescription;
        }
    }
}