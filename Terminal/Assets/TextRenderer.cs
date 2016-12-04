using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TextRenderer : MonoBehaviour {

	//Text renderer behaviour that renders characters letter by letter, attached to text UI elements in Unity.
	//Uses escape characters to be parsed in pre-processing and during rendering.
	//Current standard format: <Hexadecimal color>String to render.



	public int frameDelay;

	Text textbox;
	char[] currentString;
	int frames;
	int strPointer;
	int strLength;


	//render controls whether the renderer will work or not.
	bool render = false;


	// Use this for initialization
	void Start () {
		textbox = GetComponent<Text>();
		RenderNewString("<CCCC00FF>\nNewStringHere\n\nHello world!");
	}
	
	// Update is called once per frame
	void Update () {

		if (render && frames >= frameDelay) {
			//Add the next character to the text box.
			if (strPointer < strLength) {
				RenderNewCharacter();
			}

			frames = 0;
		}
		else {
			frames++;
		}

	}




	public void RenderNewString(string newText) {
		//Halt rendering, parse the new string, then add it for rendering then start the new string again.
		render = false;

		//Parse the string first
		string[] sectionedString = newText.Split('>');

		//If a preprocessor finds a specific start to any string fed to the 
		foreach (string preprocess in sectionedString) {
			if (preprocess.StartsWith("<")) {
				SetTextColor(preprocess);
			}
			else {
				//Assume this is the string to render.
				currentString = preprocess.ToCharArray();
			}
		}

		//Reset the counter so it will work
		strLength = currentString.Length;
		frames = frameDelay;
		strPointer = 0;
		textbox.text = "";

		render = true;
	}



	void SetTextColor(string newColor) {
		//Set the colour for the text to be added.

		//Trim the marker character from the text
		newColor = newColor.Substring(1);
		

		if (newColor.Length == 8) {
			//Convert all numbers to hex, and then into integer values.
			int r = int.Parse(newColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
			Debug.Log(r);
			int g = int.Parse(newColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
			int b = int.Parse(newColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
			int a = int.Parse(newColor.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);

			//Set the font colour.
			textbox.color = new Color(r,g,b,a);
		}
		//textbox.color = new Color();
	}



	void RenderNewCharacter() {
		//Render the next character and move the string pointer along.
		textbox.text = textbox.text + currentString[strPointer];
		strPointer++;
	}
}
