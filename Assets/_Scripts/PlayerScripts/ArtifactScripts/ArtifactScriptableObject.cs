using UnityEngine;

namespace _Scripts.PlayerScripts.ArtifactScripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Artifact", fileName = "Artifact ScriptableObject", order = 0)]
    public class ArtifactScriptableObject : ScriptableObject
    {
        private string _artifactName;
        private ArtifactRarity _artifactRarity;
        private string _artifactFlavorText;
        private string _artifactDescription;

        public string GetArtifactName()
        {
            return _artifactName;
        }

        public ArtifactRarity GetArtifactRarity()
        {
            return _artifactRarity;
        }

        public string GetArtifactFlavorText()
        {
            return _artifactFlavorText;

        }

        public string GetArtifactDescription()
        {
            return _artifactDescription;
        }
    }
}