using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
	private Vector3 originPosition;
	private Quaternion originRotation;
	private Vector3 originPosition_old;
	private Quaternion originRotation_old;
	public bool _play = false;
	public float[] _start_decay = {0.005f,0.005f,0.005f} ;
	public float[] _start_intensity = {0.05f,0.1f,0.3f};
	public float shake_decay = 0.002f;
	public float shake_intensity = 0.3f;
	
/*	int ntest = 0;
	void OnGUI (){
		if (GUI.Button (new Rect (20,40,80,20), "Shake"))
		{
			Shake(ntest);
			ntest =  (ntest+1) % _start_decay.Length;
		}
	}
*/
	
	void Update ()
	{
		
	}
	void LateUpdate()
	{
		if(_play)
		{
			if(transform.parent == null)
			{
				_play = false;
				return;
			}
			
			if (shake_intensity > 0){				
				transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
				transform.localRotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .2f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
				shake_intensity -= shake_decay;
			}else{
				try
				{
//				    GameWorld.Instance._Player_Manager._Player_FSM_Controller.SetStartCameraPosition();
				
				transform.localPosition = originPosition_old;
				transform.localRotation = originRotation_old;
				
				}catch{
				}
				_play = false;
			}
		}		
	}
	
	public void Shake(int nType)
	{
	  	originPosition_old = transform.localPosition;
		originRotation_old = transform.localRotation;
	  	originPosition = transform.localPosition;
		originRotation = transform.localRotation;
		
		
		shake_intensity = _start_intensity[nType];
		shake_decay = _start_decay[nType];
		
		_play = true;
	}
}