using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextRenderBehaviour : MonoBehaviour {

	public int frameTime;

	Text textbox;
	char[] currentString;
	int strLength;
	int strPointer;
	int frames;

	// Use this for initialization
	void Start () {
		textbox = GetComponent<Text>();
		RenderNewString("<color=#7FBF38D1></color>This is a test of the new rendering system.\n<color=#EE3300D1></color>This is testing the new colours.<color=#7FBF38D1></color>\nAnd back to normal.");
	}
	
	// Update is called once per frame
	void Update () {
		if (frames == frameTime) {
			//Render a new character on this frame
			frames = 0;

			if (strPointer == strLength) {
				//Don't render, since the string is finished.
			}
			else {

				//If there's a character bypass, deal with it.
				if (currentString[strPointer] == '<') {
					BracketBypass("");
				}
				else {
					//Extra conditions here.
					RenderCharacter();
				}

				//RenderCharacter();
			}
		}
		else {
			//Skip the frame
			frames++;
		}
	}


	void RenderNewString(string stringToRender) {
		//Turn the string into a series of characters, and start the renderer again.
		currentString = stringToRender.ToCharArray();
		strLength = currentString.Length;

		//Reset the frame renderer
		strPointer = 0;
		//Render the first character immediately.
		frames = frameTime;
		textbox.text = "";
	}


	void BracketBypass(string stringBuilder) {
		//Angle brackets denote escape characters in rich text. Basically, render them all immediately.
		do {
			stringBuilder = stringBuilder + currentString[strPointer];
			strPointer++;
		} while (currentString[strPointer] != '>');

		stringBuilder = stringBuilder + currentString[strPointer];
		strPointer++;

		//If there's another character directly following it, also apply that one.
		if (currentString[strPointer + 1] == '<') {
			BracketBypass(stringBuilder);
		}
		else {
			textbox.text = textbox.text + stringBuilder;
			//RenderCharacter(stringBuilder);
		}
	}


	void RenderCharacter() {
		//Render the next character in the string.
		textbox.text = textbox.text.Remove(textbox.text.Length - 8);

		textbox.text = textbox.text + currentString[strPointer] + "</color>";
		strPointer++;
	}
}
