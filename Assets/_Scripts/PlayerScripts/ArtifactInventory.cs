using System;
using System.Collections.Generic;
using _Scripts.ArtifactScripts;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class ArtifactInventory : MonoBehaviour
    {
        private MonoBehaviour[] _artifacts;

        private void Awake()
        {
            _artifacts = new  MonoBehaviour[2];
            _artifacts[0] = GetComponent<SwiftyArtifact>();
        }

        private void Update()
        {

                if (Input.GetKeyDown(KeyCode.M))
                {
                    ArtifactManager.GetInstance().ActivateArtifact(_artifacts[0]);
                }

                if (Input.GetKeyDown(KeyCode.N))
                {
                    ArtifactManager.GetInstance().DeactivateArtifact(_artifacts[0]);
                }
        }
    }
}