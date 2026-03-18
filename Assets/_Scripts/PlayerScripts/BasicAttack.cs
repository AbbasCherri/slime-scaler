using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.PlayerScripts
{
    public class BasicAttack : MonoBehaviour
    {
        [Header("Spawn Information")] 
        [SerializeField] private GameObject bulletSpawn;
        [SerializeField] private GameObject bulletPrefab;
        [Header("Attack Information")]
        [SerializeField] private float shotCooldown;
        [SerializeField] private float scaleLose;
        [SerializeField] private float scaleGain;
        private float _prevShotTime;
        
        [Header("Audio")]
        [SerializeField] private AudioClip shootSound;


        private void Update()
        {
            
            _prevShotTime += Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.F) && _prevShotTime >= shotCooldown)
            {
                _prevShotTime = 0;
                Fire();
            }
        }

        private void Fire()
        {
            Instantiate(bulletPrefab, bulletSpawn.transform.position, bulletPrefab.transform.rotation);
            PlayerHealth.GetInstance().Damage(scaleLose);
            
            if (AudioManager.Instance != null && shootSound != null)
            {
                AudioManager.Instance.sfxSource.PlayOneShot(shootSound, 0.3f); 
            }
        }
        
    }
}
