using UnityEngine;
using System.Collections;

using System.IO;

public class LoadGameLevel : MonoBehaviour {
    public bool _ViewLog = false;
    private string _log;
    void OnGUI()
    {
        if (_ViewLog == true)
        {
            GUI.Label(new Rect(10, 70, 600, 600), _log);
        }
    }

    void Awake()
    {
        if (CheckSaveFile() == false)
        {
            StartCoroutine(IE_DownloadFile());
        }
        else
        {
            _DownloadOK = true;
        }
    }
	// Use this for initialization
	void Start () {
        
	}
    bool _StartLoadLevel = false;
	// Update is called once per frame
	void Update () {
        if (_DownloadOK)
        {
            if (_StartLoadLevel == false)
            {
                _StartLoadLevel = true;
                StartCoroutine(LoadSceneFile());
            }
        }
	}

    public string _streemingFileName = "GameLevel_00.unity3d";
    public string _fullpath;
    public string _saveFileName = "GameLevel_00.unity3d";
    public bool _DownloadOK = false;
    public bool _StartupData = false;
    public AssetBundle _SceneBundle;
    public string _SceneName = "GameLevel_00";

    public IEnumerator LoadSceneFile()
    {
        _log += "LoadSceneFile Start";
        string filename;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        filename = "file://" + Application.persistentDataPath + "/" + _saveFileName;
#elif UNITY_ANDROID
		    filename =  Application.persistentDataPath + "/" + _saveFileName;
#endif
        WWW www = new WWW(filename);
        yield return www;

        _log += "LoadSceneFile Complete: " + filename;

        if (www != null)
        {
            _SceneBundle = www.assetBundle;

            _log += "www.assetBundle =" + www.assetBundle.name;

            Application.LoadLevel(_SceneName);
        }

    }
    public bool CheckSaveFile()
    {
        if (_StartupData == true)
        {
            return false;
        }

        string filename = Application.persistentDataPath + "/" + _saveFileName;
        if (File.Exists(filename))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator IE_DownloadFile()
    {
        byte[] bytes = null;

        if (string.IsNullOrEmpty(_streemingFileName) == false)
        {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            _fullpath = "file://" + Application.streamingAssetsPath + "/" + _streemingFileName; _log += "asset path is: " + _fullpath;
#elif UNITY_ANDROID
		    _fullpath =  Application.streamingAssetsPath + "/" + _streemingFileName;	            _log += "asset path is: " + _fullpath;
#endif
        }

        WWW www = new WWW(_fullpath);
        yield return www;
        //		if (!string.IsNullOrEmpty(www.error) )
        //		{
        //		    log += " Can't read";
        //		}

        bytes = www.bytes;

        _log += "\n www.error = [" + www.error + "] www.size = " + www.size;

        if (bytes != null)
        {
            try
            {
                string filename = Application.persistentDataPath + "/" + _saveFileName;						
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length); _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
                }

                _DownloadOK = true;

            }
            catch (System.Exception e)
            {
                _log += "\nTest Fail with Exception " + e.ToString();
                _log += "\n\n Did you copy File into StreamingAssets ?\n";
            }
        }
    }
}
