/*	
	 _	  _				
	| |  | |                                       
	| |__| | __ _ _ __ _ __ ___   ___  _ __  _   _ 
	|  __  |/ _` | '__| '_ ` _ \ / _ \| '_ \| | | |
	| |  | | (_| | |  | | | | | | (_) | | | | |_| |
	|_|  |_|\__,_|_|  |_| |_| |_|\___/|_| |_|\__, | 
	                                          __/ |
	                                         |___/   v 0.3




	Present by
	Baptiste Billet

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


[RequireComponent(typeof(AudioSource))]
[System.Serializable]
public class HarmonyAudioSource : MonoBehaviour {

    [HideInInspector]
    public SoundManager soundManager;

	[HideInInspector]
	public AudioSource audioSource;

	[HideInInspector]
	[SerializeField]
	public bool playlistMode = false;
	[HideInInspector]
	[SerializeField]
	public bool loopPlaylist=false;
	[HideInInspector]
	[SerializeField]
	public bool randomMode = false;

	public List<AudioClip> offPrintList = new List<AudioClip>();

	public int currentClipNumber=0;

	[HideInInspector]
	public bool isPlaying=false;

	[HideInInspector]
	public bool isPaused=false;

	[SerializeField]
	public List<AudioClip> playList = new List<AudioClip>();

	[HideInInspector]
	public float playbackTime;

    #region Textures
    public Texture loopTexture;
	[HideInInspector]
	public Texture randomTexture;
	[HideInInspector]
	public Texture playTexture;
	[HideInInspector]
	public Texture stopTexture;
	[HideInInspector]
	public Texture pauseTexture;
	[HideInInspector]
	public Texture previousTexure;
	[HideInInspector]
	public Texture nextTexture;

	[HideInInspector]
	public Texture loopTextureSelected;
	[HideInInspector]
	public Texture randomTextureSelected;
	[HideInInspector]
	public Texture playTextureSelected;
	[HideInInspector]
	public Texture stopTextureSelected;
	[HideInInspector]
	public Texture pauseTextureSelected;

	[HideInInspector]
	public Texture plusTexture;
	[HideInInspector]
	public Texture minusTexture;

	[HideInInspector]
	public Texture upTexture;
	[HideInInspector]
	public Texture downTexture;
	[HideInInspector]
	public Texture deleteTexture;
	[HideInInspector]
	public Texture cleanTexture;
    #endregion

    public Color activeColor;

	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();

		if (audioSource.playOnAwake || audioSource.isPlaying )
        {
			isPlaying = true;
			audioSource.Play();
			StartCoroutine(PlayingLoop());
		}
    }

    public void SetSoundManager(SoundManager _soundManager)
    {
        soundManager = _soundManager;
    }

	public void EnabledPlaylistMode( bool _PlaylistMode )
	{
		playlistMode = _PlaylistMode;
    }

	public void EnabledLoopPlaylist(bool _LoopPlaylist )
	{
		loopPlaylist = _LoopPlaylist;
	}

	public void Play()
	{
		if ( audioSource.isPlaying == false )
		{
			if ( randomMode == true )
			{
				if ( offPrintList.Count == 0 )
				{
					RandomizeTheOffSetList();
					PlayRandomMode();
				}
			}
			else
			{
				audioSource.Play();
			}
		}

		isPlaying = true;
		StartCoroutine(PlayingLoop());
		
	}

	public void Pause()
	{
		if ( audioSource.isPlaying == true )
		{
			audioSource.Pause();
			StopAllCoroutines();
			isPlaying = false;
			isPaused = true;
		}
	}

	public void UnPause()
	{
		if ( audioSource.isPlaying == false )
		{
			audioSource.UnPause();
			isPlaying = true;
			StartCoroutine(PlayingLoop());
			isPaused = false;
		}

	}

	public void Stop()
	{
		StopAllCoroutines();
		audioSource.Stop();
		isPlaying = false;
		isPaused = false;
	}

	public void NextClip()
	{
		if ( randomMode == true )
		{
			if ( offPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				isPlaying = true;
			}
			PlayRandomMode();
		}
		else
		{
			if ( currentClipNumber + 1 > playList.Count - 1 )
			{
				currentClipNumber = 0;
			}
			else
			{
				currentClipNumber++;
			}

			if ( audioSource.isPlaying )
			{
				audioSource.clip = playList [currentClipNumber];
				audioSource.Play();
			}
		}
	}

	public void PreviousClip()
	{
		if(randomMode == true)
		{
			if ( offPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				isPlaying = true;
			}
			PlayRandomMode();
		}
		else
		{
			if ( currentClipNumber - 1 < 0 )
			{
				currentClipNumber = playList.Count-1;
			}
			else
			{
				currentClipNumber--;
			}

			if ( audioSource.isPlaying )
			{
				audioSource.clip = playList [currentClipNumber];
				audioSource.Play();
			}
		}

	}

	public void SetClip(int _clipIndex)
	{

		if(playList[_clipIndex]==null)
		{
			Debug.LogError("NULL");
			return;
		}

		if ( randomMode )
		{
			if ( offPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				isPlaying = true;
			}
			
		}
		else
		{
			

			if ( _clipIndex > -1 && _clipIndex < playList.Count )
			{
                currentClipNumber = _clipIndex;

				audioSource.clip = playList [currentClipNumber];

				if ( audioSource.isPlaying )
				{
					audioSource.Play();
					isPlaying = true;
					isPaused = false;
				}
			}
		}
	}

	public void SetRandomClip()
	{
		if ( randomMode )
		{
			if ( offPrintList.Count == 0 )
			{
				RandomizeTheOffSetList();
				isPlaying = true;
			}
			else
			{
				currentClipNumber = Random.Range(0, offPrintList.Count + 1);
				PlayRandomMode();
			}
		}
		else
		{
			currentClipNumber = Random.Range(0, playList.Count + 1);

			if ( audioSource.isPlaying )
			{
				audioSource.clip = playList [currentClipNumber];
				audioSource.Play();
			}
		}
	}

	public void SetRandomMode( bool _enable)
	{
		randomMode = _enable;

		if(randomMode == true)
		{
			RandomizeTheOffSetList();
		}

    }

	private void PlayRandomMode()
	{
		currentClipNumber = Random.Range(0, offPrintList.Count + 1);
		audioSource.clip = playList [currentClipNumber];
		audioSource.Play();

		offPrintList.RemoveAt(currentClipNumber);
	}

	private void RandomizeTheOffSetList()
	{
		offPrintList = new List<AudioClip>();

		foreach ( AudioClip _clip in playList )
		{
			offPrintList.Add(_clip);
		}

		offPrintList = offPrintList.RandomList();
	}

	public List<AudioClip> GetPlaylist()
	{
		return playList;
	}

	public void AddClip(AudioClip _clip = null)
	{
		playList.Add(_clip);
	}

	public void SubClip( AudioClip _clip = null )
	{
		if ( _clip != null )
		{
			if ( playList.Contains(_clip) )
			{
				playList.Remove(_clip);
			}
		}
		else
		{
			if ( playList.Count > 1 )
			{
				playList.RemoveAt(playList.Count - 1);
			}
		}
	}

	public void DeleteClip( AudioClip _clip)
	{
		if ( playList.Count == 1 )
		{
			playList [0] = null;
		}
		else
		{
			if ( playList.Contains(_clip) )
			{
				playList.Remove(_clip);
			}
		}
	}

	public void ResetPlaylist()
	{
		playList.Clear();
		playList.Add(null);
	}

	public void SwapClip( AudioClip _clip1, AudioClip _clip2 )
	{
		if ( playList.Contains(_clip1) && playList.Contains(_clip2) )
		{

			int _position1 = playList.IndexOf(_clip1);
			int _position2= playList.IndexOf(_clip2);

			playList [_position1] = _clip2;

			playList [_position2] = _clip1;
		}
	}

	public void SwapUpClip(AudioClip _clip)
	{
		if(playList.Contains(_clip))
		{
			int _positionClip = playList.IndexOf(_clip);

			if(_positionClip>0)
			{
				SwapClip(_clip, playList [_positionClip - 1]);
			}

		}
	}

	public void SwapDownClip( AudioClip _clip )
	{
		if ( playList.Contains(_clip) )
		{
			int _positionClip = playList.IndexOf(_clip);

			if ( _positionClip < playList.Count-1 )
			{
				SwapClip(_clip, playList [_positionClip + 1]);
			}

		}
	}

	IEnumerator PlayingLoop()
	{
		while( isPlaying == true)
		{
			if( playlistMode )
			{

				if ( !audioSource.isPlaying )
				{

					if ( randomMode == false )
					{

						if ( currentClipNumber + 1 > playList.Count - 1 )
						{
							if ( loopPlaylist )
							{
								currentClipNumber = 0;
								audioSource.clip = playList [currentClipNumber];
								audioSource.Play();
							}
						}
						else
						{
							Debug.Log("a");
							currentClipNumber++;
							audioSource.clip = playList [currentClipNumber];
							audioSource.Play();
						}
					}
					else
					{
						if ( offPrintList.Count <= 0 )
						{
							if ( loopPlaylist )
							{
								RandomizeTheOffSetList();
								PlayRandomMode();
							}
						}
						else
						{
							currentClipNumber = Random.Range(0, offPrintList.Count + 1);
							audioSource.clip = playList [currentClipNumber];
							audioSource.Play();

							offPrintList.RemoveAt(currentClipNumber);
						}

					}
				}
			}
			else
			{
				if(!audioSource.isPlaying)
				{ 
					isPlaying = false;
				}
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	void Update()
	{
		if(audioSource.isPlaying)
		{
			playbackTime = audioSource.time;
        }
	}

	public void CleanPlaylist()
	{
		for(int i=0; i<playList.Count; i++)
		{
			if( playList[i]==null)
			{
				playList.RemoveAt(i);
				i = 0;
			}
		}

		if(playList.Count==0)
		{
			playList.Add(null);
        }

	}

	public void ClearPlaylist( )
	{
		if(playList.Count>0)
		{
			AudioClip _clip = playList[0];

			if(_clip == null)
			{
				_clip = audioSource.clip;
			}

			playList.Clear();

			playList.Add(_clip);

			audioSource.clip = _clip;

			if ( isPlaying )
			{
				if(isPaused)
				{
					audioSource.UnPause();
				}
				else
				{
					audioSource.Play();
				}
				
			}
			else
			{
				Stop();

			}

			

			

		}
		
	}


    public void DestroyAfterPlaying()
    {
        StartCoroutine(BurnAfterPlaying());
    }
    
    IEnumerator BurnAfterPlaying()
    {
        while (audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        soundManager.Source.Remove(audioSource);

        Destroy(gameObject);
    }

}


public static class RandomListExtension
{
	public static List<T> RandomList<T>( this List<T> _list )
	{
		List<int> _ListStraightOrder = new List<int>();

		for ( int i = 0; i < _list.Count; i++ )
		{
			_ListStraightOrder.Add(i);
		}

		List<int> _ListRandomOrder = new List<int>();

		for ( int i = 0; i < _list.Count; i++ )
		{
			int y = Random.Range(0, _ListStraightOrder.Count-1);
			_ListRandomOrder.Add(_ListStraightOrder [y]);

			_ListStraightOrder.RemoveAt(y);
		}

		List<T> _OldList = new List<T>();

		for ( int i = 0; i < _list.Count; i++ )
		{
			_OldList.Add(_list [_ListRandomOrder [i]]);
		}

		_list = _OldList;

		return _list;
	}
}