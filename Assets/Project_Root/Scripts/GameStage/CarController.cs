using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {
    public Transform _Pos_Fart;
    public GameObject _pfFart;
    public bool _ReadyFart = false;
	public float _FartDur = 2f;
	public GameObject _PlayFartObject = null;
    public float _ActiveColliderTime = 1f;
    public Collider2D _FartCollider;
    public void PlayFart()
    {
        if (_pfFart != null
            && _Pos_Fart != null
            && _ReadyFart == true
		    )
        {
            _ReadyFart = false;
			_PlayFartObject = Instantiate(_pfFart, _Pos_Fart.position, Quaternion.identity ) as GameObject;
			Destroy(_PlayFartObject, _FartDur);

            StartCoroutine(DelayAction(_FartDur, () => { _ReadyFart = true; } ) );

            _FartCollider.enabled = true;
            StartCoroutine(DelayAction(_ActiveColliderTime, () => { _FartCollider.enabled = false; }));
        }
        else
        {
            Debug.Log("_pfFart null!  _PlayFartObject ");
        }
    }

        // delay method coroutine
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }
	// Use this for initialization
	void Start () {


        Ready();
	}
    void Ready()
    {
        _ReadyFart = true;
        _FartCollider.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
