using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.


	void Start () 
	{
	}


	void OnExplode(Transform pos)
	{
		// Create a quaternion with a random rotation in the z-axis.
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

		// Instantiate the explosion where the rocket is with the random rotation.
        Instantiate(explosion, pos.position, randomRotation);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		// If it hits an enemy...
		if(col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Hurt(1);

			// Call the explosion instantiation.
			OnExplode(col.transform);

            // GameOver
            GamePlayManager.Instance.GameEnd();
		}
	}
}
