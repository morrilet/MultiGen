using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : Singleton<AudioManager> 
{
	public AudioClip[] effects;
	public AudioClip[] lemmyFootstep;
	public AudioClip[] lemmyBeegun;
	public AudioClip[] music;

	public AudioSource musicSource;
	public AudioSource effectSource;

	private float effectStartPitch; //The default pitch of the effect source.

	public override void Awake()
	{
		isPersistant = true;

		effectStartPitch = effectSource.pitch;

		base.Awake ();
	}

	public void PlayMusic(string songName)
	{
		StopMusic ();
		for (int i = 0; i < music.Length; i++) 
		{
			if (music [i].name == songName) 
			{
				musicSource.clip = music [i];
				musicSource.loop = true;
				musicSource.Play ();
			}
		}
	}

	public void PlayMusicWithIntro(string introName, string songName)
	{
		StartCoroutine (PlayMusicWithIntroCoroutine(introName, songName));
	}

	IEnumerator PlayMusicWithIntroCoroutine(string introName, string songName)
	{
		StopMusic ();

		for (int i = 0; i < music.Length; i++) 
		{
			if (music [i].name == introName)
			{
				musicSource.clip = music [i];
				musicSource.loop = false;
				musicSource.Play ();
				yield return new WaitForSeconds (music [i].length);
			}
		}
		for (int i = 0; i < music.Length; i++) 
		{
			if (music [i].name == songName)
			{
				musicSource.clip = music [i];
				musicSource.loop = true;
				musicSource.Play ();
			}
		}
	}

	public void StopMusic()
	{
		musicSource.Stop ();
	}

	public void PlaySoundEffect(string clipName)
	{
		for (int i = 0; i < effects.Length; i++) 
		{
			if (effects [i].name == clipName) 
			{
				effectSource.pitch = effectStartPitch;
				effectSource.PlayOneShot (effects [i]);
			}
		}
	}

	public void PlaySoundEffectVariation(string clipName, float minPitch, float maxPitch)
	{
		float pitch = Random.Range (minPitch, maxPitch);
		for (int i = 0; i < effects.Length; i++) 
		{
			if (effects [i].name == clipName) 
			{
				effectSource.pitch = pitch;
				effectSource.PlayOneShot (effects [i]);
			}
		}
	}

	public void PlayRandomFromList(AudioClip[] fxList)
	{
		AudioClip randomClip = fxList [Random.Range (0, fxList.Length)];
		PlaySoundEffect (randomClip.name.ToString());
	}
}