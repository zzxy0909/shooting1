using UnityEngine;
using System.Collections;

public class DelayJoint : MonoBehaviour {
    public GameObject _JointRoot;
    public Vector2 _DelayTimeMinMax = new Vector2(1f, 2f);
    public float _DelayTime = 0.5f;

	// Use this for initialization
	void Start () {
        if (_JointRoot == null
            && transform.parent != null)
        {
            _JointRoot = transform.parent.gameObject;
            TweenPosition.Begin(transform.gameObject, 0f, Vector3.zero).method = UITweener.Method.Linear;
        }
        else
        {
//            TweenPosition.Begin(transform.gameObject, 0f, _JointRoot.transform.position).method = UITweener.Method.Linear;
        }
        
        SetDelayTime();
        
	}
    void SetDelayTime()
    {
        _DelayTime = Random.Range(_DelayTimeMinMax.x, _DelayTimeMinMax.y);
        _nextJoint = Time.time + _DelayTime;
    }

    float _nextJoint = 0f;
	// Update is called once per frame
	void Update () {
        if (Time.time > _nextJoint
                )
        {
            SetDelayTime();
            JointMove();
        }
	}
    void JointMove()
    {
        if (_JointRoot == null
            && transform.parent != null)
        {
            TweenPosition.Begin(transform.gameObject, _DelayTime / 2f, Vector3.zero).method = UITweener.Method.Linear;
        }
        else
        {
            TweenPosition.Begin(transform.gameObject, _DelayTime / 2f, _JointRoot.transform.position).method = UITweener.Method.Linear;
        }
    }
}
