using System;
using System.Collections.Generic;
using _Scripts.ScriptableObjects;
using UnityEngine;

namespace _Scripts.ArtifactScripts
{
    public class ArtifactManager : MonoBehaviour
    {
        [SerializeField] private List<ArtifactScriptableObject> artifactObjects;
        private List<IArtifactable> _artifacts;

        private void Awake()
        {
            foreach (var t in artifactObjects)
            {
                _artifacts.Add(GetComponent<>);
            }
            
            
        }
    }
}