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
	// text for the opportunity title
	public Text oppTitle;
	// text for the opportunity context
	public Text oppContext;

	// the parent that will hold the buttons for context
	private GameObject buttonHolder;
	//  a prefab of the buttons to spawn
	public GameObject contextButtonPrefab;
	// the set number of buttons to create
	private int numberOfButtons = 6;
	// the size of the context buttons
	private float contextSize = 0.02f;

	// need a ref to the canvas holding a the valuepropositon pair
	public GameObject valuePropPair;
	// the parent of the valuePropPair
	public GameObject valuePropParent;


	// update
	private void Update()
	{
		// if we are missing any of the UI references || stop
		// dropdown
		if(oppDropdown == null){Debug.Log("No ref to opportunity dropdownon: " + transform.name); return;}
		// text
		if(oppInput == null){Debug.Log("No ref to opportunity text inputon: " + transform.name); return;}
		// button
		if(oppButton == null){Debug.Log("No ref to opportunity buttonon: " + transform.name); return;}
		// text title
		if(oppTitle == null){Debug.Log("No ref to opportunity title: " + transform.name); return;}
		// text context
		if(oppContext == null){Debug.Log("No ref to opportunity context: " + transform.name); return;}
		// obj value prop pair
		if(valuePropPair == null){Debug.Log("No ref to value prop pair prefab: " + transform.name); return;}
		// tobj value parent
		if(valuePropParent == null){Debug.Log("No ref to value prop parent: " + transform.name); return;}
	}// end of update


	// a function to create a new opportunity
	public void CreateOpportunity()
	{


		// if opportunity dropown value does not equal 0 (it means we are not on create opportunity)
		if(oppDropdown.value != 0)
		{
			
			// load the ResumeOpportunities function
			ResumeOpportunities(oppDropdown.options[oppDropdown.value].text);
			// stop 
			return;
		}// end of dropdown not on 0

		// if there is no text in the opportunity text then we have not given the opportunity a name
		if(string.IsNullOrEmpty(oppInput.text)){Debug.Log("Please give a name to the new opportuntiy"); return;}

		 // create a list to convert the string
            List<string> NewOpp = new List<string>();
            // add the string to the list
            NewOpp.Add(oppInput.text);
            // add everything in the list to the options in dropdown
            oppDropdown.AddOptions(NewOpp);
			// load the ResumeOpportunities function
			ResumeOpportunities(oppInput.text);		
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

	// a function to resume opportunities
	public void ResumeOpportunities(string opportunityName)
	{
		
			// then update the title based on dropdown option 
			oppTitle.text = opportunityName;
		

		// if there is not reference to the button holder than we need to stop || if there is then add it
		if(buttonHolder == null){ if(GameObject.Find("ContextButtonHolder")== null){Debug.Log("No Button Holder found on: " + transform.name); return;} else{buttonHolder = GameObject.Find("ContextButtonHolder");}}

		// for loop X number of times (to create buttons)
		for(int i = 1; i < numberOfButtons; i++)
		{
			// if air bnb then pull up a hard coded one
			// each button will be assigned a value so the name it has doesnt matter

			// if there is no reference to the button then stop the loop
			if(contextButtonPrefab == null){Debug.Log("No reference to context button prefab found on: " + transform.name); break;}
			// make it
			GameObject buttonClone = Instantiate(contextButtonPrefab, contextButtonPrefab.transform.position, contextButtonPrefab.transform.rotation);
			// set parent to canvas
			buttonClone.transform.SetParent(buttonHolder.transform.parent);
			// give new position
			buttonClone.GetComponent<RectTransform>().anchoredPosition = new Vector3(-395 + (140*i), -164, 0);
			// set parent again
			buttonClone.transform.SetParent(buttonHolder.transform);
			 // set the scale to the appropriate size
        	buttonClone.GetComponent<RectTransform>().localScale = new Vector3(contextSize, contextSize, contextSize);
			// assign the button a name based on it's number instantiated
			buttonClone.transform.name = buttonClone.transform.name + "_" + i;

			// add an event to the button
			//buttonClone.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ContextButtons("hello"));
        	buttonClone.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate {ContextButtons(buttonClone.transform.name); });

		}// end of forloop create buttons

	}// end of resume opportunities function


	// a function for context buttons to call
	public void ContextButtons(string name)
	{
		// get a list of the inputs
		GameObject[] contextInputs = GameObject.FindGameObjectsWithTag("ContextInputs");
		// find their parent, then find their first child
		GameObject[] inputParentChild = new GameObject[contextInputs.Length];
		for(int p = 0; p<contextInputs.Length; p++)
		{
			// if the button's name contains the button name we pressed
			if(name == contextInputs[p].transform.parent.name)
			{
				// if there is no text in the box || stop
				if(string.IsNullOrEmpty(oppContext.text)){Debug.Log("nothing written in the context"); return;}
				// assign the context text
				oppContext.text = contextInputs[p].GetComponent<InputField>().text;
				// call the function to create a customer value proposition
				ValuePropositionPair(oppContext.text);
				// turn off the login page
				transform.GetChild(1).gameObject.SetActive(false);
				// stop loop
				break;
			}// end of if names match up
		
		}// end of for loop context inputs.length
	
	}// end of context buttons


	// a function to create value proposition || it takes the context of the object to make it under
	public void ValuePropositionPair(string context)
	{
		// obj value prop pair
		if(valuePropPair == null){Debug.Log("No ref to value prop pair prefab: " + transform.name); return;}
		// obj parent
		if(valuePropParent == null){Debug.Log("No ref to value prop parent: " + transform.name); return;}

		// make a clone
		GameObject valueClone = Instantiate(valuePropPair, valuePropPair.transform.position, valuePropPair.transform.rotation);
		// rename it with its context name
		valuePropPair.transform.name = valuePropPair.transform.name + "_" + context;
		// parent it
		valueClone.transform.SetParent(valuePropParent.transform);

	}// end of create value proposition

}// end of opportunity creation
