using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (var sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
        }
    }

    private void Start()
    {
        GameEvents.Instance.OnRightHit += Play;
    }

    public void Play(string soundTitle)
    {
        Sound sound = Array.Find(Sounds, sound => sound.Name == soundTitle);
        if (sound == null)
        {
            Debug.LogWarning(soundTitle + " doesn't exist!");
            return;
        }

        sound.Source.Play();
    }

    public void Play(OnRightHitEventArgs args)
    {
        Sound sound = Array.Find(Sounds, sound => sound.Name == args.AudioTitle);
        if (sound == null)
        {
            Debug.LogWarning(args.AudioTitle + " doesn't exist!");
            return;
        }

        sound.Source.Play();
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnRightHit -= Play;
    }
}
