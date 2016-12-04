using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnterCommand : MonoBehaviour {

	public InputField textfield;
	public Text log;
	public Text story;
	public string[] previousCommands = new string[8];
	public int numCommands = 0;
	int commandPointer = 0;
	bool commandEnable = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Parse the string if there's anything to parse.
		if (Input.GetKeyDown(KeyCode.Return) && textfield.text.ToString() != "") {

			string stringToParse = textfield.text.ToString().ToUpper();
			string stringToStore = textfield.text.ToString();
			textfield.text = "";


			if (stringToParse == "EXIT") {
				//Exit the program and editor
				Application.Quit();
			}
			else if (stringToParse == "CLEAR") {
				//Clears the log fields and keeps them clean.
				ClearText();
			}
			else {
				//parse the command and hand the command to the scene event handler for a response.
				//String.Split ignores all spaces and only grabs the words between them.
				string[] commands = stringToParse.Split(' ');

				//Add it to the input list of commands previously done
				AddToCommandList(stringToStore);


				//Pass the command to the log for review
				log.text = log.text + "\n<color=#7FBF38D1>>" + stringToStore + "</color>";
				//Pass the command into the event handler for the Fungus scene.

			}
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			//The command pointer should always point to the last entry before the arrow keys were pressed.
			if (commandEnable == false) {
				commandEnable = true;
			}
			else {
				if (commandPointer > 0) {
					commandPointer--;
				}
			}

			//Format the text field to be the string entered previously, using the command pointer.
			textfield.text = previousCommands[commandPointer];
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow)) {
			//Down arrow is disabled unless up arrow was pressed first
			if (commandEnable == true) {
				if (commandPointer < numCommands) {
					commandPointer++;
				}

				if (commandPointer == 8) {
					textfield.text = "";
				}
				else {
					textfield.text = previousCommands[commandPointer];
				}
			}
		}
	}


	void AddToCommandList(string stringToAdd) {
		if (numCommands <= 7) {
			previousCommands[numCommands] = stringToAdd;
			numCommands++;
			commandPointer = numCommands;
		}
		else {

			//Shift commands down the list.
			for (int i = 0; i < 7; i++) {
				previousCommands[i] = previousCommands[i + 1];
			}

			previousCommands[7] = stringToAdd;

			commandPointer = 7;
		}
	}

	void ClearText() {
		log.text = "";
		textfield.text = "";
		story.text = "";
	}
}
