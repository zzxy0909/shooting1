using UnityEngine;
using System.Collections;

public class Done_RandomRotator : MonoBehaviour 
{
	public float tumble;
	
	void Start ()
	{
        GetComponent<Rigidbody2D>().AddTorque( tumble * Random.Range(-100f, 100f) ); //  Random.insideUnitSphere* tumble;
	}
}