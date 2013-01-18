using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DotScript : MonoBehaviour {

	[SerializeField]
	GameObject dot;
	List <GameObject> dots = new List<GameObject>();
	Transform sphere;
	void Start () {
		sphere = GameObject.Find ("Sphere").GetComponent<Transform>();
	}
	
	public void StartDrawDot(){
		InvokeRepeating("DrawDot",0.001f,0.1f); //Start drawing dots /Dessiner les points
	}
	public void StopDrawDot(){
		CancelInvoke();
	}
	void DrawDot(){
		GameObject obj = (GameObject)Instantiate (dot,sphere.position,sphere.rotation);
		dots.Add(obj);
	}	
	public void RemoveDots(){
		for(int i = 0; i<dots.Count;i++)Destroy(dots[i]);
		dots.Clear ();
	}
}
