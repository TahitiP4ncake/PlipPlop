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

using System.Collections.Generic;
using System.IO;
using UnityEngine.Audio;


using UnityEditor;


public class HarmonyWindow : EditorWindow
{
    // Global Window
    static int tab;

    public static bool IsFreeMode;

    // Windows
    #region Windows

    [MenuItem("Window/Harmony/General")]
    public static void ShowWindowGeneral()
    {
        tab = 0;
        HarmonyWindow window = (HarmonyWindow)EditorWindow.GetWindow(typeof(HarmonyWindow));
        window.Show();
    }

    [MenuItem("Window/Harmony/Triggers")]
    public static void ShowWindowTriggers()
    {
        tab = 1;
        HarmonyWindow window = (HarmonyWindow)EditorWindow.GetWindow(typeof(HarmonyWindow));
        window.Show();
    }

    #endregion

    // General
    #region General

    // Stock here, all the directories in child from Sounds
    static private string[] AudioClipsDirectories;

    static private List<string> Categories = new List<string>();

    // The SoundManager prefab
    static public GameObject SoundManager;

    static public SoundManager SoundManagerScript;

    static private Harmony HarmonyScript;

    // To verify that all the folders and files are in the right place
    static bool Verify()
    {
        // Verify if the directory AudioClips exist
        if (!Directory.Exists("Assets/AudioClips/"))
        {
            Debug.LogError("NO AudioClips DIRECTORY FOUND");
            HarmonyEditorPopupFolderSounds.ShowPopUp();
            return false;
        }

        // Get all the directories of the AudioClips folder
        AudioClipsDirectories = Directory.GetDirectories("Assets/AudioClips/");



        // Get all the categories as Directories names
        foreach (string _s in AudioClipsDirectories)
        {
            Categories.Add(_s.Remove(0, 18));
        }

        // Verify if the prefab SoundManager exist
        if (!File.Exists("Assets/AudioClips/SoundManager.prefab"))
        {
            Debug.LogError("NO SOUNDMANAGER FOUND");

            HarmonyEditorPopUpSoundManager.ShowPopUp();

            return false;
        }
        else
        {
            SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
        }

        SoundManagerScript = SoundManager.GetComponent<SoundManager>();

        if (SoundManagerScript == null)
        {
            SoundManager.AddComponent<SoundManager>();
        }

        HarmonyScript = SoundManager.GetComponent<Harmony>();

        if (HarmonyScript == null)
        {
            SoundManager.AddComponent<Harmony>();
        }

        // Verify if there are categories
        if (Categories.Count == 0)
        {
            Debug.LogError("NO CATEGORIES FOUND IN HARMONY");
            return false;
        }

        return true;
    }

    // The main process
    public static void ActualizeHarmonyProcess()
    {
        // Do all the verification
        if (Verify() == false)
        {
            return;
        }

        Assign();

    }

    // If all are in place, set all clips 
    static void Assign()
    {
        // For each folder
        foreach (string _category in AudioClipsDirectories)
        {
            // Get all the files of the folder
            string[] Files = Directory.GetFiles(_category);

            if (Files.Length > 0)
            {
                AddClipsToSoundManager(Files);
            }

        }
    }

    static void AddClipsToSoundManager(string[] Files)
    {
        // For each files 
        foreach (string _s in Files)
        {

            AudioClip _Clip = ((AudioClip)AssetDatabase.LoadAssetAtPath(_s, typeof(AudioClip)));

            if (_Clip != null)
            {

                // If the clip is not in the librairy yet
                if (!IsThisClipUnknowed(_Clip))
                {
                    //Add to list
                    SoundManagerScript.ListClips.Add(_Clip);
                }
            }
        }
    }


    // Verify if this clip is unknowed
    static bool IsThisClipUnknowed(AudioClip _newClip)
    {
        foreach (AudioClip _Clip in SoundManagerScript.ListClips)
        {
            if (_newClip == _Clip)
            {
                foreach (AudioSource _source in SoundManagerScript.Source)
                {
                    if (_source.clip == _newClip)
                    {
                        _source.clip = _Clip;
                    }
                }
                return true;
            }
        }

        return false;

    }

    static public void CreateAudioSource(GameObject GO_SoundManger)
    {
        foreach (AudioClip _clip in SoundManagerScript.ListClips)
        {

            if (!IsSourceKnowed(_clip))
            {

                GameObject _go = new GameObject();
                AudioSource _source;
                _go.AddComponent<AudioSource>();
                _go.AddComponent<HarmonyAudioSource>();

                _source = _go.GetComponent<AudioSource>();

                _source.playOnAwake = false;

                _source.loop = false;

                _source.clip = _clip;

                _go.GetComponent<HarmonyAudioSource>().playList.Add(_clip);

                _go.name = _clip.name;

                _go.transform.parent = GO_SoundManger.transform;

                GO_SoundManger.GetComponent<SoundManager>().Source.Add(_source);
            }
        }
    }

    static public bool IsSourceKnowed(AudioClip _clip)
    {
        foreach (AudioSource _Source in SoundManagerScript.Source)
        {
            if (_Source.clip == _clip)
            {
                return true;
            }
        }

        return false;
    }

    void OnGUIGeneral()
    {
        EditorGUILayout.Space();

        if (GUILayout.Button("Actualize the Librairy"))
        {
            ActualizeHarmonyProcess();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Clean Sources"))
        {
            CleanSources();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (GUILayout.Button("Clean Clips"))
        {
            CleanClips();
        }

        EditorGUILayout.Space();
    }

    public static void CleanSources()
    {

        if (HarmonyEditor.SoundManager != null)
        {
            if (HarmonyEditor.SoundManagerScript != null)
            {
                GameObject GO_SoundManager;
                GO_SoundManager = Instantiate(HarmonyEditor.SoundManager);

                GO_SoundManager.GetComponent<SoundManager>().Source.Clear();

                PrefabUtility.ReplacePrefab(GO_SoundManager, HarmonyEditor.SoundManager, ReplacePrefabOptions.ConnectToPrefab);
                DestroyImmediate(GO_SoundManager);

            }
            else
            {
                HarmonyEditor.SoundManagerScript = HarmonyEditor.SoundManager.GetComponent<SoundManager>();
                CleanSources();
            }
        }
        else
        {
            // Verify if the prefab SoundManager exist
            if (File.Exists("Assets/AudioClips/SoundManager.prefab"))
            {
                HarmonyEditor.SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
                CleanSources();
            }
            else
            {
                Debug.LogError("CAN'T FIND SOUNDMANAGER");
            }
        }
    }

    //[MenuItem("Harmony/Clean/Clips")]
    public static void CleanClips()
    {
        if (HarmonyEditor.SoundManager != null)
        {
            if (HarmonyEditor.SoundManagerScript != null)
            {
                GameObject GO_SoundManager;
                GO_SoundManager = Instantiate(HarmonyEditor.SoundManager);

                GO_SoundManager.GetComponent<SoundManager>().ListClips.Clear();

                PrefabUtility.ReplacePrefab(GO_SoundManager, HarmonyEditor.SoundManager, ReplacePrefabOptions.ConnectToPrefab);
                DestroyImmediate(GO_SoundManager);
            }
            else
            {
                HarmonyEditor.SoundManagerScript = HarmonyEditor.SoundManager.GetComponent<SoundManager>();
                CleanClips();

            }
        }
        else
        {
            // Verify if the prefab SoundManager exist
            if (File.Exists("Assets/AudioClips/SoundManager.prefab"))
            {
                HarmonyEditor.SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
                CleanClips();
            }
            else
            {
                Debug.LogError("CAN'T FIND SOUNDMANAGER");
            }
        }

    }

    #endregion

    // Triggers work
    #region Triggers

    public bool m_IsDestroyingAfterPlaying = false;

    public HarmonyCollisionCategory m_CollisionCategory;

    public List<string> m_Layers = new List<string>();

    public List<string> m_Tags = new List<string>();

    public List<string> m_Names = new List<string>();

    public bool m_IsWorkingOnce = false;

    public enum ColliderCategory
    {
        Sphere,
        Cube,
        Capsule,
        Mesh
    }

    private ColliderCategory m_ColliderCategory;

    private Collider m_Collider;

    private Mesh m_Mesh;

    // The SoundManager prefab
    public GameObject SoundTrigger;

    public HarmonySoundTrigger m_HarmonySoundTrigger;

    private void CreateHarmonyTrigger()
    {
        // Base
        SoundTrigger = new GameObject();

        SoundTrigger.name = "HarmonySoundTrigger";

        // Add Components
        SoundTrigger.AddComponent<HarmonySoundTrigger>();

        SoundTrigger.AddComponent<AudioSource>();

        SoundTrigger.AddComponent<HarmonyAudioSource>();

        switch (m_ColliderCategory)
        {
            case ColliderCategory.Capsule:
                SoundTrigger.AddComponent<CapsuleCollider>();
                break;

            case ColliderCategory.Cube:
                SoundTrigger.AddComponent<BoxCollider>();
                break;

            case ColliderCategory.Sphere:
                SoundTrigger.AddComponent<SphereCollider>();
                break;

            case ColliderCategory.Mesh:
                SoundTrigger.AddComponent<MeshCollider>();
                break;
        }

        // Get the Collider
        m_Collider = SoundTrigger.GetComponent<Collider>();

        m_Collider.isTrigger = true;

        // For Mesh Collider Only
        if (m_ColliderCategory == ColliderCategory.Mesh && m_Mesh != null)
        {
            m_Collider = SoundTrigger.GetComponent<MeshCollider>();
            SoundTrigger.GetComponent<MeshCollider>().sharedMesh = m_Mesh;
            SoundTrigger.GetComponent<MeshCollider>().convex = true;
            m_Collider.isTrigger = true;
        }

        // Get the Script
        m_HarmonySoundTrigger = SoundTrigger.GetComponent<HarmonySoundTrigger>();

        m_HarmonySoundTrigger.Initialize(m_IsDestroyingAfterPlaying, m_CollisionCategory, m_IsWorkingOnce, SoundTrigger.GetComponent<AudioSource>(), m_Collider, m_Mesh);
    }

    public void OnGUITriggers()
    {
        EditorGUILayout.LabelField("Create New Sound Trigger", EditorStyles.boldLabel);

        EditorGUILayout.Space();

        m_IsDestroyingAfterPlaying = EditorGUILayout.Toggle("Destroy after playing ?", m_IsDestroyingAfterPlaying);

        EditorGUILayout.Space();

        m_ColliderCategory = (ColliderCategory)EditorGUILayout.EnumPopup("Type of Collider:", m_ColliderCategory);

        if (m_ColliderCategory == ColliderCategory.Mesh)
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            m_Mesh = (Mesh)EditorGUILayout.ObjectField(m_Mesh, typeof(Mesh), true);
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.Space();

        m_IsWorkingOnce = EditorGUILayout.Toggle("Collider work only once ?", m_IsWorkingOnce);

        EditorGUILayout.Space();


        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Create"))
        {
            CreateHarmonyTrigger();
        }
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
    }

    #endregion


    void Start()
    {
        IsFreeMode = !UnityEditorInternal.InternalEditorUtility.HasPro();
    }

    // GUI
    void OnGUI()
    {
        tab = GUILayout.Toolbar(tab, new string[] { "General", "Triggers" });

        switch (tab)
        {
            case 0:
                OnGUIGeneral();
                break;

            case 1:
                OnGUITriggers();
                break;
        }
    }

}


// Popup Work
public class HarmonyEditorPopupFolderSounds : EditorWindow
{
    public static void ShowPopUp()
    {
        HarmonyEditorPopupFolderSounds window = ScriptableObject.CreateInstance<HarmonyEditorPopupFolderSounds>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
        window.ShowPopup();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("There is no AudioClips folder in your project, do you want us to create one ?", EditorStyles.wordWrappedLabel);
        GUILayout.Space(50);
        if (GUILayout.Button("Agree!"))
        {
            AssetDatabase.CreateFolder("Assets", "AudioClips");

            HarmonyEditor.HarmonyProcess();

            this.Close();
        }

        if (GUILayout.Button("No"))
        {
            this.Close();
        }

    }
}

public class HarmonyEditorPopUpSoundManager : EditorWindow
{
    public static void ShowPopUp()
    {
        HarmonyEditorPopUpSoundManager window = ScriptableObject.CreateInstance<HarmonyEditorPopUpSoundManager>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
        window.ShowPopup();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("There is no SoundManager Prefab in your Assets/AudioClips folder, do you want us to create one ?", EditorStyles.wordWrappedLabel);
        GUILayout.Space(50);
        if (GUILayout.Button("Agree!"))
        {
            GameObject SoundManager = new GameObject();

            SoundManager.name = "SoundManager";

            SoundManager.AddComponent<Harmony>();
            SoundManager.AddComponent<SoundManager>();

            PrefabUtility.CreatePrefab("Assets/AudioClips/SoundManager.prefab", SoundManager);

            DestroyImmediate(SoundManager);

            HarmonyEditor.HarmonyProcess();

            this.Close();
        }

        if (GUILayout.Button("No"))
        {
            this.Close();
        }

    }
}


public class HarmonyEditor : EditorWindow
{

    //[MenuItem("Harmony/Actualize")]
    public static void ShowWindow()
    {
        HarmonyProcess();

    }


    // Stock here, all the directories in child from Sounds
    static private string[] AudioClipsDirectories;

    static private List<string> Categories = new List<string>();

    // The SoundManager prefab
    static public GameObject SoundManager;

    static public SoundManager SoundManagerScript;

    static private Harmony HarmonyScript;

    // To verify that all the folders and files are in the right place
    static bool Verify()
    {
        // Verify if the directory AudioClips exist
        if (!Directory.Exists("Assets/AudioClips/"))
        {
            Debug.LogError("NO AudioClips DIRECTORY FOUND");
            HarmonyEditorPopupFolderSounds.ShowPopUp();
            return false;
        }

        // Get all the directories of the AudioClips folder
        AudioClipsDirectories = Directory.GetDirectories("Assets/AudioClips/");


        // Get all the categories as Directories names
        foreach (string _s in AudioClipsDirectories)
        {
            Categories.Add(_s.Remove(0, 18));
        }

        // Verify if the prefab SoundManager exist
        if (!File.Exists("Assets/AudioClips/SoundManager.prefab"))
        {
            Debug.LogError("NO SOUNDMANAGER FOUND");

            HarmonyEditorPopUpSoundManager.ShowPopUp();

            return false;
        }
        else
        {
            SoundManager = ((GameObject)AssetDatabase.LoadAssetAtPath("Assets/AudioClips/SoundManager.prefab", typeof(GameObject)));
        }

        SoundManagerScript = SoundManager.GetComponent<SoundManager>();

        if (SoundManagerScript == null)
        {
            SoundManager.AddComponent<SoundManager>();
        }

        HarmonyScript = SoundManager.GetComponent<Harmony>();

        if (HarmonyScript == null)
        {
            SoundManager.AddComponent<Harmony>();
        }

        // Verify if there are categories
        if (Categories.Count == 0)
        {
            Debug.LogError("NO CATEGORIES FOUND IN HARMONY");
            return false;
        }

        return true;
    }

    // The main process
    public static void HarmonyProcess()
    {
        // Do all the verification
        if (Verify() == false)
        {
            return;
        }

        Assign();

        GenerateSources();

    }

    // If all are in place, set all clips 
    static void Assign()
    {
        // For each folder
        foreach (string _category in AudioClipsDirectories)
        {
            // Get all the files of the folder
            string[] Files = Directory.GetFiles(_category);

            if (Files.Length > 0)
            {
                AddClipsToSoundManager(Files);
            }

        }
    }

    static void AddClipsToSoundManager(string[] Files)
    {

        // For each files 
        foreach (string _s in Files)
        {

            AudioClip _Clip = ((AudioClip)AssetDatabase.LoadAssetAtPath(_s, typeof(AudioClip)));

            if (_Clip != null)
            {

                // If the clip is not in the librairy yet
                if (!IsThisClipUnknowed(_Clip))
                {
                    //Add to list
                    SoundManagerScript.ListClips.Add(_Clip);
                }
            }
        }
    }


    // Verify if this clip is unknowed
    static bool IsThisClipUnknowed(AudioClip _newClip)
    {
        foreach (AudioClip _Clip in SoundManagerScript.ListClips)
        {
            if (_newClip == _Clip)
            {
                foreach (AudioSource _source in SoundManagerScript.Source)
                {
                    if (_source.clip == _newClip)
                    {
                        _source.clip = _Clip;
                    }
                }
                return true;
            }
        }

        return false;

    }

    static void GenerateSources()
    {
        GameObject GO_SoundManager;
        GO_SoundManager = Instantiate(SoundManager);

        //SoundManager GO_SoundManagerScript = GO_SoundManager.GetComponent<SoundManager>();

        CreateAudioSource(GO_SoundManager);


        PrefabUtility.ReplacePrefab(GO_SoundManager, SoundManager, ReplacePrefabOptions.ConnectToPrefab);
        DestroyImmediate(GO_SoundManager);
    }

    static public void CreateAudioSource(GameObject GO_SoundManger)
    {
        foreach (AudioClip _clip in SoundManagerScript.ListClips)
        {

            if (!IsSourceKnowed(_clip))
            {

                GameObject _go = new GameObject();
                AudioSource _source;
                _go.AddComponent<AudioSource>();
                _go.AddComponent<HarmonyAudioSource>();

                _source = _go.GetComponent<AudioSource>();

                _source.playOnAwake = false;

                _source.loop = false;

                _source.clip = _clip;

                _go.GetComponent<HarmonyAudioSource>().playList.Add(_clip);

                _go.name = _clip.name;

                _go.transform.parent = GO_SoundManger.transform;

                GO_SoundManger.GetComponent<SoundManager>().Source.Add(_source);
            }
        }
    }

    static public bool IsSourceKnowed(AudioClip _clip)
    {
        foreach (AudioSource _Source in SoundManagerScript.Source)
        {
            if (_Source.clip == _clip)
            {
                return true;
            }
        }

        return false;
    }


}
