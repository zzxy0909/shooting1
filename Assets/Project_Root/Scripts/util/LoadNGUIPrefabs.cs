using UnityEngine;
using System.Collections;

using System.IO;

// 메모리 관리 측면에서 인스터스 할당후 다음 씬으로 넘어갈때, 온디스트로이 에서 에셋번들을 언로드 할 필요 있을것 같음.
//
//
public class LoadNGUIPrefabs : MonoBehaviour {
    public bool _ViewLog = false;
    private string _log;
    void OnGUI()
    {
        if (_ViewLog == true)
        {
            GUI.Label(new Rect(10, 70, 600, 600), _log);
        }
    }

    public string _saveFileName = "UI_Prefabs01.unity3d";


    // Use this for initialization
	void Start () {
        
	}

    public DownloadController _DownloadController;
    bool _StartLoadObject = false;
    // Update is called once per frame
	void Update () {

        if (_DownloadController)
        {
            if (_DownloadController._DownloadOK)
            {
                if (_StartLoadObject == false)
                {
                    _StartLoadObject = true;
                    StartCoroutine(LoadObject_WWWAttach());
                }
            }
        }
	}
    public IEnumerator LoadObject_Attach()
    {
        string filename;

        filename = Application.persistentDataPath + "/" + _saveFileName;

        FileStream oFS = File.Open(filename, FileMode.Open);
        if (oFS == null)
        {
            Debug.LogError("File Open Error - " + filename);
        }
        else
        {
            int nFileLength = (int)oFS.Length;
            byte[] arAssetBundleBytes = new byte[nFileLength];
            oFS.Read(arAssetBundleBytes, 0, nFileLength);
            oFS.Close();

            AssetBundleCreateRequest oAcr = AssetBundle.LoadFromMemoryAsync(arAssetBundleBytes);
            while (oAcr.isDone == false)
            {
                this._LoadingBar.value = oAcr.progress;
                yield return new WaitForSeconds(0.5f);
                _log += ", " + this._LoadingBar.value;
            }
            this._LoadingBar.value = 1f;
            // ===============
            GameObject obj = (GameObject)Instantiate(oAcr.assetBundle.mainAsset);
            _log += "www.assetBundle.mainAsset =" + obj.name;
            obj.transform.parent = this.transform.parent;
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.transform.localPosition = Vector3.zero;

            this._LoadingBar.gameObject.SetActive( false );
            //===============================
        }
    }
    public IEnumerator LoadObject_WWWAttach()
    {
        _log += "LoadFile Start";
        string filename;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        filename = "file://" + Application.persistentDataPath + "/" + _saveFileName;
#elif UNITY_ANDROID
		filename = "file://" + Application.persistentDataPath + "/" + _saveFileName;
#endif
        WWW www = new WWW(filename);
        yield return www;

        _log += "LoadSceneFile Complete: " + filename;

        if (www != null)
        {
            GameObject obj = (GameObject) Instantiate( www.assetBundle.mainAsset );
            _log += "www.assetBundle.mainAsset =" + obj.name;
            obj.transform.parent = this.transform.parent;
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.transform.localPosition = Vector3.zero;

            this._LoadingBar.gameObject.SetActive( false );
        }
    }


    void Awake()
    {
        /*        if (CheckSaveFile() == false)
                {
                    StartCoroutine(IE_DownloadFile());
                }
                else
                {
                    _DownloadOK = true;
                }
         * */
    }

    public UISlider _LoadingBar;
    public string _AssetBundlePath;
//    public bool _DownloadOK = false;
    public bool _StartupData = false;
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
        WWW www = new WWW(_AssetBundlePath);

        this._LoadingBar.gameObject.SetActive( true );
        while (!www.isDone)
        {
            this._LoadingBar.value = www.progress;
            yield return null;
        }
        this._LoadingBar.value = 1.0f;

//        yield return www;

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

 //               _DownloadOK = true;

            }
            catch (System.Exception e)
            {
                _log += "\nTest Fail with Exception " + e.ToString();
                _log += "\n\n Did you copy File into StreamingAssets ?\n";
            }
        }
    }
}
