using UnityEngine;
using System.Collections;

public class wt_PlayerMeleeCheck : MonoBehaviour {
    public wt_PlayerController _PlayerController;
    public float _checkRange = 4.5f;
    public LayerMask _CheckLayer;

	// Use this for initialization
	void Start () {

        _PlayerController = transform.parent.GetComponent<wt_PlayerController>();
	}
    // delay method coroutine
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }
	
	// Update is called once per frame
	void Update () {

        if (_PlayerController == null)
        {
            _PlayerController = transform.parent.GetComponent<wt_PlayerController>();
        }
	}

    public bool CheckMelee()
    {
        if (_PlayerController != null)
        {
            Collider2D[] arrColl = Physics2D.OverlapCircleAll(transform.position, _checkRange, _CheckLayer.value);
            foreach (Collider2D col in arrColl)
            {
                if (col.tag == "Enemy")
                {
                    _PlayerController._FireType = E_FireType.melee;
                    return true;
                }
            }
            
        }
        return false;
    }

}
