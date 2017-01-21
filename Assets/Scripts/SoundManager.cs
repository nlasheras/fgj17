﻿using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public enum SoundEffect
{
    Crowd
}

public class SoundManager : MonoBehaviour 
{
    private static SoundManager s_instance;
    public AudioSource m_audioSource;
    public AudioMixer m_mixer;

    public AudioClip m_crowd;

    public static SoundManager Instance
    {
        get
        {
            Debug.Assert(s_instance != null);
            return s_instance;
        }
    }

    void OnEnable()
	{
        Debug.Assert(s_instance == null);
        s_instance = this;

        if (m_audioSource == null)
           m_audioSource = GetComponent<AudioSource>();

        PlaySound(SoundEffect.Crowd);
	}

    void OnDisable()
    {
        s_instance = null;
    }
	
	public void PlaySound(SoundEffect fx)
	{
        m_audioSource.PlayOneShot(ChooseClip(fx));
	}

    public void CrowdHappy()
    {
        m_mixer.FindSnapshot("CrowdHappy").TransitionTo(2.0f);
    }

    public void CrowdUnhappy()
    {
        m_mixer.FindSnapshot("CrowdUnhappy").TransitionTo(2.0f);
    }

    private AudioClip ChooseClip(SoundEffect fx)
    {
        switch(fx)
        {
            case SoundEffect.Crowd: return m_crowd;
        }
        return null;
    }
}