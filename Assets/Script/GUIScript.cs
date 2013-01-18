using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIScript : MonoBehaviour {

	public GUIStyle style;	
	// Draw the score and end screen /affiche le score et l'ecran de fin
    void OnGUI() {
        if(GameManager.ball>=0)
			GUI.Label(new Rect(20, 20, 100, 50), GameManager.ball.ToString(),style);
		if(GameManager.ball < 0){
			string str = "End Game\nPress R to start again";
			GUI.Label(new Rect(Screen.width/2-150, Screen.height/2-150, 300, 300), str,style);
		}
    }
}
