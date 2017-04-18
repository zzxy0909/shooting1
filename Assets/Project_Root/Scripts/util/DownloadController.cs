using UnityEngine;
using System.Collections;

using System.IO;

public class DownloadController : MonoBehaviour {
    public bool _ViewLog = false;
    private string _log;
    void OnGUI()
    {
        if (_ViewLog == true)
        {
            GUI.Label(new Rect(10, 70, 600, 600), _log);
        }
    }

    public string[] _CheckFiles;
    public string[] _DownloadURLs;

    public bool _DownloadOK = false;

    public UILabel _lbMessage;
    public UISlider _DownloadBar;


	// Use this for initialization
	void Start () {

        StartCoroutine(IE_DownloadAll());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator IE_DownloadAll()
    {
        for (int i = 0; i < _CheckFiles.Length; i++)
        {
            string saveFileName = _CheckFiles[i];
            if (CheckSaveFile(saveFileName) == false)
            {
                yield return StartCoroutine(IE_DownloadFile(_DownloadURLs[i], saveFileName));
            }
        }

        _DownloadOK = true;
    }

    public bool _StartupDownload = false;
    public bool CheckSaveFile(string a_saveFileName)
    {
        if (_StartupDownload == true)
        {
            return false;
        }
        string filename = Application.persistentDataPath + "/" + a_saveFileName;
        if (File.Exists(filename))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator IE_DownloadFile(string a_fullpath, string a_saveFileName)
    {
        byte[] bytes = null;
        WWW www = new WWW(a_fullpath);

        this._DownloadBar.gameObject.SetActive( true );
        this._lbMessage.text = a_saveFileName;
        while (!www.isDone)
        {
            this._DownloadBar.value = www.progress;
            yield return null;
        }
        this._DownloadBar.value = 1.0f;

        //        yield return www;

        bytes = www.bytes;

        _log += "\n www.error = [" + www.error + "] www.size = " + www.size;

        if (bytes != null)
        {
            try
            {
                string filename = Application.persistentDataPath + "/" + a_saveFileName;
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length); _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
                }
            }
            catch (System.Exception e)
            {
                _log += "\nTest Fail with Exception " + e.ToString();
                _log += "\n\n Did you copy File into StreamingAssets ?\n";
            }
        }
    }
}
