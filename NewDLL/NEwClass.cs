using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NewDLL
{
	public class NewClass:MonoBehaviour{		
		public static void SphereExplosion( float radius, float power,GameObject obj, GameObject expParticle, AudioClip expSound )
    	{
	        // Collect all colliders 
			Transform tr = obj.GetComponent<Transform>();
	        Collider[] colliders = Physics.OverlapSphere(tr.position, radius);
	        Instantiate(expParticle, tr.position, Quaternion.identity);
	        obj.audio.PlayOneShot(expSound, 0.7F);
	        foreach (Collider col in colliders)
	        {
	            //Is the collider a rigidbody
	            if (col.rigidbody)
	            { 
	                //Is the rigidbody not the main sphere
	                if (col.rigidbody != obj.rigidbody)
	                { 
	                    //Apply force with power and center at the ball position within the radius
	                    col.rigidbody.AddExplosionForce(power, tr.position, radius);
	                }
	            }
	        }
    	}
		public static void Bouncing(Rigidbody rig, float force,Vector3 start, Vector3 end){
			Vector3 direction = GetDirection(start,end);
			rig.AddForce(direction*force);
		}
		public static Vector3 GetDirection(Vector3 start, Vector3 end)
    	{
        	Vector3 direction = start - end;
	        float dist = Mathf.Clamp(direction.sqrMagnitude, 1f, 25f);
	       	direction *= dist/direction.sqrMagnitude;
			return direction;
    	}
	}
}

