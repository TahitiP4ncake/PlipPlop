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
using System;


using UnityEditor;

[CustomEditor(typeof(HarmonyAudioSource))]
public class HarmonyAudioSourceEditor : Editor {

	HarmonyAudioSource myTarget;
	private float RandomLoopSize = 50;

	private int timeSeconds;
	private int timeMinutes;

	private int timeMaxSeconds;
	private int timeMaxMinutes;

	//private int playListCount=1;

	Vector2 playListScrollPos;

	Color m_Blue = new Color(0.294f,0.486f,0.819f);

    string PathProMode = "Assets/Harmony/HarmonyExtra/ProMode/";
    string PathFreeMode = "Assets/Harmony/HarmonyExtra/FreeMode/";
    string PathMode;

    string PathSelected = "Assets/Harmony/HarmonyExtra/Selected/";

    private void OnEnable()
    {
        #region Textures

        // Check the free mode
        if (HarmonyWindow.IsFreeMode)
        {
            PathMode = PathFreeMode;
        }
        else
        {
            PathMode = PathProMode;
        }

        myTarget.loopTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Loop.png", typeof(Texture)));

        myTarget.randomTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Random.png", typeof(Texture)));

        myTarget.playTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Play.png", typeof(Texture)));

        myTarget.stopTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Stop.png", typeof(Texture)));

        myTarget.previousTexure = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Previous.png", typeof(Texture)));

        myTarget.nextTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Next.png", typeof(Texture)));

        myTarget.pauseTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Pause.png", typeof(Texture)));

        myTarget.loopTextureSelected = ((Texture)AssetDatabase.LoadAssetAtPath(PathSelected + "LoopSelected.png", typeof(Texture)));

        myTarget.randomTextureSelected = ((Texture)AssetDatabase.LoadAssetAtPath(PathSelected + "RandomSelected.png", typeof(Texture)));

        myTarget.playTextureSelected = ((Texture)AssetDatabase.LoadAssetAtPath(PathSelected + "PlaySelected.png", typeof(Texture)));

        myTarget.stopTextureSelected = ((Texture)AssetDatabase.LoadAssetAtPath(PathSelected + "StopSelected.png", typeof(Texture)));

        myTarget.pauseTextureSelected = ((Texture)AssetDatabase.LoadAssetAtPath(PathSelected + "PauseSelected.png", typeof(Texture)));

        myTarget.plusTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Plus.png", typeof(Texture)));

        myTarget.minusTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Minus.png", typeof(Texture)));

        myTarget.upTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Up.png", typeof(Texture)));

        myTarget.downTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Down.png", typeof(Texture)));

        myTarget.deleteTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Delete.png", typeof(Texture)));

        myTarget.cleanTexture = ((Texture)AssetDatabase.LoadAssetAtPath(PathMode + "Clean.png", typeof(Texture)));

        #endregion
    }

    public override void OnInspectorGUI( )
	{
		// Style
		GUIStyle ClipName = new GUIStyle();
		ClipName.alignment = TextAnchor.MiddleCenter;
        if (HarmonyWindow.IsFreeMode==false)
        {
            ClipName.normal.textColor = Color.white;
        }
        else
        {
            ClipName.normal.textColor = Color.black;
        }

		// Style
		GUIStyle ClipSelected = new GUIStyle();
		ClipSelected.alignment = TextAnchor.MiddleLeft;
		ClipSelected.normal.textColor = m_Blue;

		// Style
		GUIStyle playList = new GUIStyle();
		playList.alignment = TextAnchor.MiddleCenter;
        if (!HarmonyWindow.IsFreeMode==false)
        {
            playList.normal.textColor = Color.white;
        }
        else
        {
            playList.normal.textColor = Color.black;
        }

        // Style
        GUIStyle playListSelected = new GUIStyle();
		playListSelected.alignment = TextAnchor.MiddleCenter;
		playListSelected.normal.textColor = m_Blue;

		// The original script
		myTarget = (HarmonyAudioSource) target;

		// The audioSource
		myTarget.audioSource = myTarget.GetComponent<AudioSource>();



		#region Timers and clip name
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();
		//Time actual
		string _timeAudio="00:00:00";

		if ( myTarget.playbackTime >= 3600 )
		{
			TimeSpan _time = TimeSpan.FromSeconds(myTarget.playbackTime);
			_timeAudio = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
				_time.Hours,
				_time.Minutes,
				_time.Seconds,
				_time.Milliseconds);
		}
		else
		{
			TimeSpan _time = TimeSpan.FromSeconds(myTarget.playbackTime);
			_timeAudio = string.Format("{0:D2}:{1:D2}:{2:D2}",
				_time.Minutes,
				_time.Seconds,
				_time.Milliseconds);
		}

		EditorGUILayout.LabelField(_timeAudio,GUILayout.Width(80));

        // Name
        if (myTarget.audioSource.clip != null)
        {
            EditorGUILayout.LabelField(myTarget.audioSource.clip.name, ClipName);
        }
        else
        {
            if (myTarget.playList.Count > 0)
            {
                if (myTarget.playList[0]!=null)
                {
                    EditorGUILayout.LabelField(myTarget.playList[0].name, ClipName);
                }
                else
                {
                    EditorGUILayout.LabelField(myTarget.gameObject.name, ClipName);
                }
            }
            else
            {
                EditorGUILayout.LabelField(myTarget.gameObject.name, ClipName);
            }
            
        }

		//Time Max
		string _timeMaxAudio="00:00:00";

        if (myTarget.audioSource.clip != null)
        {
            if (myTarget.audioSource.clip.length >= 3600)
            {
                TimeSpan _time = TimeSpan.FromSeconds(myTarget.audioSource.clip.length);
                _timeMaxAudio = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}",
                    _time.Hours,
                    _time.Minutes,
                    _time.Seconds,
                    _time.Milliseconds);
            }
            else
            {
                TimeSpan _time = TimeSpan.FromSeconds(myTarget.audioSource.clip.length);
                _timeMaxAudio = string.Format("{0:D2}:{1:D2}:{2:D2}",
                    _time.Minutes,
                    _time.Seconds,
                    _time.Milliseconds);
            }
        }

		EditorGUILayout.LabelField(_timeMaxAudio,GUILayout.Width(80));

		EditorGUILayout.EndHorizontal();
		#endregion

		#region ProgressBar
		EditorGUILayout.BeginHorizontal();
        if (myTarget.audioSource.clip != null)
        {
            float _max = 1000;
            float _actu = (_max * myTarget.playbackTime) / myTarget.audioSource.clip.length;

            GUILayout.HorizontalSlider(_actu, 0, _max, GUILayout.Width(EditorGUIUtility.currentViewWidth - 35));
        }
		EditorGUILayout.EndHorizontal();
		#endregion ProgressBar

		#region Loop
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.Space();

		if ( myTarget.loopPlaylist == true )
		{

			if ( GUILayout.Button(myTarget.loopTextureSelected, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.EnabledLoopPlaylist(false);
			}
		}
		else
		{
			if ( GUILayout.Button(myTarget.loopTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.EnabledLoopPlaylist(true);
			}
		}
		#endregion

		#region LectureParameters

		// Previous
		if ( GUILayout.Button(myTarget.previousTexure, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
		{
			myTarget.PreviousClip();
		}

		//Play / Pause
		if ( myTarget.isPlaying )
		{
			if ( myTarget.isPaused == false )
			{
				if ( GUILayout.Button(myTarget.pauseTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.Pause();
				}
			}
			else
			{
				if ( GUILayout.Button(myTarget.playTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.Play();
				}
			}
		}
		else
		{
			if ( myTarget.isPaused == false )
			{
				if ( GUILayout.Button(myTarget.playTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.Play();
				}
			}
			else
			{
				if ( GUILayout.Button(myTarget.playTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
				{
					myTarget.UnPause();
				}
			}
		}


		// Stop
		if ( GUILayout.Button(myTarget.stopTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
		{
			myTarget.Stop();
		}

		// Next
		if ( GUILayout.Button(myTarget.nextTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
		{
			myTarget.NextClip();
		}
		#endregion

		#region Random
		if ( myTarget.randomMode == true )
		{
			if ( GUILayout.Button(myTarget.randomTextureSelected, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.SetRandomMode(false);
			}
		}
		else
		{
			if ( GUILayout.Button(myTarget.randomTexture, GUILayout.Width(RandomLoopSize), GUILayout.Height(RandomLoopSize)) )
			{
				myTarget.SetRandomMode(true);
			}
		}
		EditorGUILayout.Space();
		EditorGUILayout.EndHorizontal();

		#endregion

		#region playList list Button

		EditorGUILayout.BeginHorizontal();
		if ( myTarget.playlistMode == true )
		{
			if ( GUILayout.Button("playList", playListSelected, GUILayout.Width(60), GUILayout.Height(20)) )
			{
				myTarget.EnabledPlaylistMode(false);
			}
		}
		else
		{
			if ( GUILayout.Button("playList", playList, GUILayout.Width(60), GUILayout.Height(20)) )
			{
				myTarget.EnabledPlaylistMode(true);
			}
		}

		EditorGUILayout.Space();
        if ( GUILayout.Button(myTarget.plusTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.AddClip();
		}
		if ( GUILayout.Button(myTarget.minusTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.SubClip();
		}
		if ( GUILayout.Button(myTarget.cleanTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.CleanPlaylist();
		}
		if ( GUILayout.Button(myTarget.deleteTexture, GUILayout.Width(20), GUILayout.Height(20)) )
		{
			myTarget.ResetPlaylist();
		}


		EditorGUILayout.EndHorizontal();

		#endregion

		#region Clips

		EditorGUILayout.Space();

		// Scroll
		if ( myTarget.playList.Count > 10 )
		{
			playListScrollPos = EditorGUILayout.BeginScrollView(playListScrollPos, GUILayout.Width(EditorGUIUtility.currentViewWidth - 20), GUILayout.Height(220));
		}
		else
		{
			playListScrollPos = EditorGUILayout.BeginScrollView(playListScrollPos, GUILayout.Width(EditorGUIUtility.currentViewWidth - 20), GUILayout.Height(myTarget.playList.Count*22));
		}

		// playList clips
		int _size = (int)(Mathf.Log10(myTarget.playList.Count-1));

		for (int i=0; i<myTarget.playList.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();

			EditorGUILayout.LabelField(i.ToString(), GUILayout.Width(10+10*_size), GUILayout.Height(20));

			#region ButtonsBefore
			if ( myTarget.playList[i] == myTarget.audioSource.clip)
			{
				if (myTarget.isPlaying)
				{
					if ( GUILayout.Button(myTarget.pauseTextureSelected, GUILayout.Width(20), GUILayout.Height(20)) )
					{
						myTarget.Pause();
					}
				}
				else
				{
					if ( myTarget.isPaused )
					{
						if ( GUILayout.Button(myTarget.playTextureSelected, GUILayout.Width(20), GUILayout.Height(20)) )
						{
							myTarget.UnPause();
						}
					}
					else
					{
						if ( GUILayout.Button(myTarget.playTextureSelected, GUILayout.Width(20), GUILayout.Height(20)) )
						{
							myTarget.Play();
						}
					}
				}
			}	   
			else
			{
				if ( GUILayout.Button(myTarget.playTexture, GUILayout.Width(20), GUILayout.Height(20)) )
				{
					myTarget.Stop();

					myTarget.SetClip(i);
					
					myTarget.Play();
					
				}
			}
			#endregion

			//The clip
			if(myTarget.playList[i] == myTarget.audioSource.clip)
			{
				myTarget.playList [i] = (AudioClip) EditorGUILayout.ObjectField(myTarget.playList [i], typeof(AudioClip), true, GUILayout.Width(EditorGUIUtility.currentViewWidth-155), GUILayout.Height(20));
			}
			else
			{
				myTarget.playList [i] = (AudioClip) EditorGUILayout.ObjectField(myTarget.playList [i], typeof(AudioClip), true, GUILayout.Width(EditorGUIUtility.currentViewWidth-155), GUILayout.Height(20));
			}

			#region ButtonAfter
			if ( GUILayout.Button(myTarget.upTexture, GUILayout.Width(20), GUILayout.Height(20)) )
			{
				myTarget.SwapUpClip(myTarget.playList [i]);
            }

			if ( GUILayout.Button(myTarget.downTexture, GUILayout.Width(20), GUILayout.Height(20)) )
			{
				myTarget.SwapDownClip(myTarget.playList [i]);
			}

			if ( GUILayout.Button(myTarget.deleteTexture, GUILayout.Width(20), GUILayout.Height(20)) )
			{
				myTarget.DeleteClip(myTarget.playList[i]);
			}
			#endregion

			EditorGUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();

		#endregion

		#region Effects
		EditorGUILayout.Space(); EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();

		// EditorGUILayout.LabelField("Effects",ClipName);

		EditorGUILayout.EndHorizontal();
		EditorGUILayout.Space();
		#endregion


		EditorUtility.SetDirty(myTarget);
    }


}
 