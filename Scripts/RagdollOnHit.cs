using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class RagdollOnHit : MonoBehaviour {

	public float RagdollThreshold = 50;
	public GameObject RagdollPrefab;

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.layer == LayerMask.NameToLayer("Terrain"))
			return;
		
		if (coll.impulse.magnitude > RagdollThreshold)
		{
			Instantiate(RagdollPrefab, transform.position, transform.rotation);
			GameObject.Destroy(this.gameObject);
		}
	}
}
