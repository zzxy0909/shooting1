using UnityEngine;
using System.Collections;

public class BGMController : MonoBehaviour {
    public AudioClip[] _arrAudioClip;
    public string _Name_BGM1;

    AudioSource _AudioSource;
    void Awake()
    {
        if (GameWorld.Instance._BGMController == null)
        {
            GameWorld.Instance._BGMController = this;
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ GameWorld.Instance._BGMController = this; ");
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ Destroy(this.gameObject); GameWorld.Instance._BGMController ");
            return;
        }
        transform.parent = null;
        DontDestroyOnLoad(this);

        _AudioSource = GetComponent<AudioSource>();        
    }

    AudioClip GetAudioClip(string str)
    {
        foreach (AudioClip clip in _arrAudioClip)
        {
            if (clip.name == str)
            {
                return clip;
            }
        }
        return null;
    }

    void Play_BGM1()
    {
        _AudioSource.clip = GetAudioClip(_Name_BGM1);
        _AudioSource.Play();
    }

	// Use this for initialization
	void Start () {


        Play_BGM1();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
