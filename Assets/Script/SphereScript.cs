using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NewDLL;

[RequireComponent (typeof (Rigidbody))]
public class SphereScript : BaseClass {
#region variable
	bool bounce, callBounce,exp,callExplosion;
	
	//public GUIStyle style;	
	public GameObject explosion;
	public AudioClip sound;
#endregion	
	#region Start
	public override void Start () {	
		base.Start();
		exp = true;
		bounce  = true;
	}
	#endregion
	#region Update
	public override void Update () {
		base.Update ();
		if(thrown) 							// Is the ball gone
			// Explosion function /Fonction pour l'explosion
			if(flying){
				if(Input.GetMouseButtonDown(0)&&exp){
					callExplosion = true;
					exp = false;
				}
				//Function to modify path of ball/ Fonciton pour modifier la trajectoire de la balle
				if(Input.GetMouseButtonDown(1) && bounce){
					RaycastHit hit;
					if(Physics.Raycast(other.ScreenPointToRay(Input.mousePosition), out hit)) {
						end = hit.point;
						end.z = 1f;
					}	
					callBounce = true;
					bounce = false;
				}
			}
		}
	#endregion
	#region FixedUpdate
	public override void FixedUpdate(){
		if(ready){
			_rigidbody.isKinematic = false;
			_rigidbody.AddForce(dir*force);
			ready = false;
		}
		// Have we changed the path? 
		if(callBounce){
			NewClass.Bouncing(gameObject.rigidbody, force,end, _transform.position);
			callBounce =false;
		}
		if(callExplosion){
			NewClass.SphereExplosion(10.0f, 1000.0f,gameObject, explosion, sound );
			callExplosion = false;
		}
	}	
	#endregion
	#region fonctions
	protected override void SetStart(){
        base.SetStart();
		exp = true;
		bounce = true;
    }

#endregion
}
