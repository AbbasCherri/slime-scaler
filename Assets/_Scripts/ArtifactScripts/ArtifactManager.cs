using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.ArtifactScripts
{
    public class ArtifactManager : MonoBehaviour
    {
        private List<MonoBehaviour> _artifacts;


        private static ArtifactManager _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;

            _artifacts = new List<MonoBehaviour>();

            var swifty = GetComponent<SwiftyArtifact>();
            if (swifty != null) _artifacts.Add(swifty);


            foreach (var artifact in _artifacts)
            {
                artifact.enabled = false;
            }
        }

        public static ArtifactManager GetInstance()
        {
            return _instance;
        }

        public void ActivateArtifact(MonoBehaviour artifact)
        {
            if (!artifact) return;
    
            var index = GetIndex(artifact);
            if (index != -1)
            {
                _artifacts[index].enabled = true;
            }
            else 
            {
                artifact.enabled = true;
            }
        }

        public void DeactivateArtifact(MonoBehaviour artifact)
        {
            if (!artifact) return;

            var index = GetIndex(artifact);
            if (index != -1)
            {
                _artifacts[index].enabled = false;
            }
            else
            {
                artifact.enabled = false;
            }
        }

        private int GetIndex(MonoBehaviour artifact)
        {
            if (_artifacts == null) return -1;

            for (var i = 0; i < _artifacts.Count; i++)
            {
                if (_artifacts[i] == artifact) 
                {
                    return i;
                }
            }

            return -1;
        }

        public List<MonoBehaviour> GetActiveArtifacts()
        {
            return _artifacts.Where(t => t.enabled).ToList();
        }
    }
}