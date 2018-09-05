using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpportunityCreation : MonoBehaviour {

	/// this opportunity creation script focuses on the ability to name, and create new opportunities.
	/// this may need a script to later reference that holds all the already made opportunities (which this would send info into)

	// dropdown containing all existing opportunities and the ability to create a new one
	public Dropdown oppDropdown;
	// the text we type in that will add a new opportunity
	public InputField oppInput;
	// the button to create or resume a previous opportunity
	public Button oppButton;


	// update
	private void Update()
	{
		// if we are missing any of the UI references || stop
		// dropdown
		if(oppDropdown == null){Debug.Log("No ref to opportunity dropdown"); return;}
		// text
		if(oppInput == null){Debug.Log("No ref to opportunity text input"); return;}
		// button
		if(oppButton == null){Debug.Log("No ref to opportunity button"); return;}
	}// end of update


	// a function to create a new opportunity
	public void CreateOpportunity()
	{
		// if opportunity dropown value does not equal 0 then stop (it means we are not on create opportunity)
		if(oppDropdown.value != 0){return;}
		// if there is no text in the opportunity text then we have not given the opportunity a name
		if(string.IsNullOrEmpty(oppInput.text)){Debug.Log("Please give a name to the new opportuntiy"); return;}

		 // create a list to convert the string
            List<string> NewOpp = new List<string>();
            // add the string to the list
            NewOpp.Add(oppInput.text);
            // add everything in the list to the options in dropdown
            oppDropdown.AddOptions(NewOpp);
			// clear the text in the name enter spot
			oppInput.text = "";

	}// end of create opportunity function


	// a function to update the button between 	Create, and Resume
	public void UpdateButton()
	{
		// if the dropwdown value = 0 then make it say "Create"
		if(oppDropdown.value == 0){oppButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Create";}
		//if it does not = 0 then make it say "Resume"
		if(oppDropdown.value != 0){oppButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Resume";}
	}// end of Update button

}// end of opportunity creation
