using System;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Sound
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [Header("Audio Sources")]
        public AudioSource musicSource;
        public AudioSource sfxSource;

        [Header("Default Music")]
        public AudioClip backgroundMusic;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            musicSource = GetComponentInChildren<AudioSource>();
            backgroundMusic = Resources.Load<AudioClip>("Music/bgmusic"); 

            if (backgroundMusic != null)
            {
                PlayMusic(backgroundMusic);
            }
            else
            {
                Debug.LogError("AudioClip not found in Resources!");
            }
        }

        private void PlayMusic(AudioClip clip)
        {
            if (!clip) return;

            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.pitch = 1.0f;
            musicSource.volume = .2f;
            musicSource.Play();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }
        
       
        public void PlaySfx(AudioClip clip)
        {
            if (!clip) return;

            sfxSource.pitch = Random.Range(0.9f, 1.1f);
            sfxSource.PlayOneShot(clip);
        }
    }
}