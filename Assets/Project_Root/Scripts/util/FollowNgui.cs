using UnityEngine;
using System.Collections;

public class FollowNgui : MonoBehaviour {
    public bool _isFollow = false;
    public Transform _FromObj;
    public Vector3 _thisOffset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayFollow()
    {
        _isFollow = true;

    }
    public void StopFollow()
    {
        _isFollow = false;
    }

    Camera _FromCam = null;
    void LateUpdate()
    {
        if (_FromObj != null
            && _isFollow == true)
        {
            //월드좌표의 카메라객체입니다.
            if (_FromCam == null)
            {
                _FromCam = NGUITools.FindCameraForLayer(_FromObj.gameObject.layer);
                if (_FromCam == null)
                    return;
            }
            //GUI객체의 카메라 객체입니다.
            Camera guiCam = UICamera.currentCamera;
            if (guiCam == null)
                return;

            //타겟의 포지션을 NGUI월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 월드좌표로 변환합니다.
            Vector3 targetPos = transform.position + _thisOffset;
            Vector3 pos = _FromCam.ViewportToWorldPoint(guiCam.WorldToViewportPoint(targetPos));
            //Z는 0으로...
            pos.z = 0f;

            _FromObj.position = pos;
        }
    }
    // fromobj에 ngui obj를 둔다.
    public void SetPos_FromObj()
    {
        if (_FromObj != null)
        {
            //월드좌표의 카메라객체입니다.
            if (_FromCam == null)
            {
                _FromCam = NGUITools.FindCameraForLayer(_FromObj.gameObject.layer);
                if (_FromCam == null)
                    return;
            }
            //GUI객체의 카메라 객체입니다.
            Camera guiCam = UICamera.currentCamera;
            if (guiCam == null)
                return;
            Debug.Log("~~~~~~~~~~~~~~~~~~ SetPos_FromObj ");

            //타겟의 포지션을 월드좌표에서 ViewPort좌표로 변환하고 다시 ViewPort좌표를 NGUI월드좌표로 변환합니다.
            Vector3 targetPos = _FromObj.position;
            Vector3 pos = guiCam.ViewportToWorldPoint(_FromCam.WorldToViewportPoint(targetPos));
            //Z는 0으로...
            pos.z = 0f;

            transform.position = pos;

            Debug.Log("~~~~~~~~~~~~~~~~~~ SetPos_FromObj " + pos);
        }
    }
}
