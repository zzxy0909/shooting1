using UnityEngine;
using System.Collections;

[System.Serializable]
public class wt_PathAttribute
{
    public float _duration;
    public Transform _ToTarget;
}

public enum E_PathRandomType
{
    List,
    RandomList,

}
public class wt_PathMover : MonoBehaviour {
    public E_PathRandomType _RandomType = E_PathRandomType.List;
    public bool _isPlayPath = false;
    public wt_PathAttribute[] _arrPath;
    public bool _loop = true;

    public void PlayPath()
    {
        if (_isPlayPath == true
            || this.gameObject == null)
        {
            return;
        }
        _isPlayPath = true;
        StartCoroutine(IE_PlayPath());
    }
    void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator IE_PlayPath()
    {
        do
        {
            if (_RandomType == E_PathRandomType.List)
            {
                foreach (wt_PathAttribute p in _arrPath)
                {
                    TweenPosition.Begin(this.gameObject, p._duration, p._ToTarget.position).method = UITweener.Method.Linear;
                    // HOTween.To(transform, p._duration, new TweenParms().Prop("position", p._ToTarget.position, true)); // "position", p._ToTarget.position);
                    yield return new WaitForSeconds(p._duration);
                }
            }
            else if (_RandomType == E_PathRandomType.RandomList)
            {
                int index = Random.Range(0, _arrPath.Length);
                wt_PathAttribute p = _arrPath[index];
                TweenPosition.Begin(this.gameObject, p._duration, p._ToTarget.position).method = UITweener.Method.Linear;
                // HOTween.To(transform, p._duration, new TweenParms().Prop("position", p._ToTarget.position, true));
                yield return new WaitForSeconds(p._duration);
            }
        } while (_loop == true);

        _isPlayPath = false;
    }


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
