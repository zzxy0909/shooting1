using UnityEngine;
using System.Collections;

public class CheckSlotPosition : MonoBehaviour {
    public Vector3 _LastPosition;
	// Use this for initialization
	void Start () {
        SetLastPosition();
	}

    public void SetDragEnd()
    {
        Invoke("SetLastPosition", 0.3f);
    }

    public void SetLastPosition()
    {
        _LastPosition = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("~~~~~~~~~~~~~~~~~ OnTriggerEnter " + col.gameObject.name);

        if (col.tag == "slot")
        {
            this.transform.position = _LastPosition;
        }
    }
}
