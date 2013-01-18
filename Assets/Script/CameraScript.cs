/*
 * The camera follows the path of the ball on the x-axis
 * When the ball hits a collider, the camera is released and goes smoothly to the center of the screen (Lerp function)
 * 
 * La caméra suit la balle jusqu'à ce qu'elle heurte un collider. 
 * Elle vient alors se placer au centre de l'échaffaudage(Fonction Lerp)
 * */
using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Transform  sphere;
	Transform _transform;
	public bool center;
	float lerp;
	Vector3 centerPosition;
	void Start () {
		sphere = GameObject.Find ("Sphere").GetComponent<Transform>();
		_transform = GetComponent<Transform>();
		center =false;
		centerPosition = new Vector3(15f,5f,-12f);
	}
	
	void Update () {
		if(!center)
			_transform.position = new Vector3(sphere.position.x,5f,-12f);
		else{
			lerp += Time.deltaTime*0.01f; 
			_transform.position = Vector3.Lerp(_transform.position,centerPosition,lerp);
		}
	}
}
