using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeastScript : MonoBehaviour {

	public  float distance;
	public  int layerMask;
	public  float exploringAcc;
	public  float chargingForce;

	private bool charging = false;

	private NavMeshAgent nma;
	private Rigidbody rb;


	private Vector3 RandomNavSphere(){
		Vector3 randomDirection = transform.position + UnityEngine.Random.insideUnitSphere * distance;
		NavMeshHit hit;
		if (NavMesh.SamplePosition (randomDirection, out hit, 0.1f, NavMesh.AllAreas)) {
			return hit.position;
		} else {
			return transform.position;
		}
	}

	// Use this for initialization
	void Start () {
		nma = GetComponent<NavMeshAgent> ();
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!charging) {
			
			int playerMask = 1 << 9;
			RaycastHit playerHit;
			if (Physics.Raycast (transform.position, transform.TransformDirection (Vector3.forward), out playerHit, 20.0f, playerMask)) {
				nma.ResetPath ();
				rb.AddForce ((playerHit.transform.position - transform.position) * chargingForce);
				charging = true;
			} else if (!nma.hasPath) {
				Debug.Log (gameObject.name + " starting new path");
				Vector3 goTo = RandomNavSphere ();
				nma.SetDestination (goTo);
			}
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.name == "Terrain" || other.gameObject.name == "frog") {
			charging = false;
			rb.velocity = Vector3.zero;
		}
	}
}
