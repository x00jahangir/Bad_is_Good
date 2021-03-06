using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class audio_Manager : MonoBehaviour
{
    public Sounds[] sounds;

    public AudioClip gameOverClip;

    public static audio_Manager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.masterMixtureGroup;
            s.source.pitch = s.pinch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
            audio_Manager.instance.Stop("MainMenu");

        if (SceneManager.GetActiveScene().buildIndex != 1)
            audio_Manager.instance.Stop("bg_sound");

        //9-13 chaos music...
        if (SceneManager.GetActiveScene().buildIndex != 2)
        {
            for (int i = 9; i < 14; i++)
            {
                sounds[i].source.Stop();
            }
        }

        if (SceneManager.GetActiveScene().buildIndex != 3)
            audio_Manager.instance.Stop("FinalChaos");


    }

}
