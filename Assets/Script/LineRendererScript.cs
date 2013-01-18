using UnityEngine;
using System.Collections;

public class LineRendererScript : MonoBehaviour {

	LineRenderer line;
	void Start () {
		line = GameObject.Find ("Renderer").GetComponent<LineRenderer>();
		line.SetVertexCount(2);
		line.SetWidth(0.1F, 0.1F);
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void DrawLine(Vector3 start, Vector3 end){
		line.enabled = true;
		// Two vertices from sphere to mouse position / deux vertex de la balle au pointeur de la souris
		line.SetPosition(0, start);
		line.SetPosition(1, end);
	}
	public void EnableLine(bool action){
		line.enabled = action;
	}
}
