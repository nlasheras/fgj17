using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public enum SoundEffect
{
    Cough
}

public class SoundManager : MonoBehaviour 
{
    private static SoundManager s_instance;
    public AudioSource m_effectsSource;
    public AudioSource m_crowdSource;
    public AudioSource m_horseSource;
    public AudioMixer m_mixer;

    public AudioClip m_crowd;
    public AudioClip m_cough;
    public AudioClip m_horse;

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

        if (m_effectsSource == null)
           m_effectsSource = GetComponent<AudioSource>();

        m_crowdSource.clip = m_crowd;
        m_crowdSource.loop = true;
        m_crowdSource.Play();
    }

    void OnDisable()
    {
        s_instance = null;
    }
	
	public void PlaySound(SoundEffect fx, float delay = 0.0f)
	{
        m_effectsSource.clip = ChooseClip(fx);
        m_effectsSource.loop = false;
        m_effectsSource.PlayDelayed(delay);
	}

    public void StartCarriage()
    {
        m_horseSource.clip = m_horse;
        m_horseSource.loop = true;
        m_horseSource.Play();
    }

    public void StopCarriage()
    {
        m_horseSource.Stop();
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
            case SoundEffect.Cough: return m_cough;
        }
        return null;
    }
}
