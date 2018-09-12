using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteSummary : MonoBehaviour {

/// this script is to allow the note summary page to populate the notes from each category (jobs, pains, gains ...)
/// it will need reference to the mad libs tracker which hold all the notes

// a ref to the mad libs script (to access the notes created)
public MadLibs_Tracker madlibsScript;
// a ref to the note tracker so we can compare tags
public Note_Tracker noteTracker;
// a reference to the note holder images that will parent the notes we re-create
public GameObject[] noteHolders;



	// a function to populate the notes in the categores
	public void PopulateNotes()
	{
		// if madlibs is null
		if(madlibsScript == null){Debug.Log("No reference to madlibs script found on: " + transform.name); return;}
		// if notetracker is null
		if(noteTracker == null){Debug.Log("No reference to note tracker found on: " + transform.name); return;}
		// if note holders is null
		if(noteHolders == null){Debug.Log("No reference to note holders found on: " + transform.name); return;}

		
		// a string to match the note holder with the correct note
		string categorymatch = "";
		// int to scale the direction (each one will be below or to the opposit side as the other)
		int scaleDirection = 1;
		// a string or list to say if the note has already been created
		string[] listOfSpawnedNotes = {"",};
		// a bool for if the note should spawn on the left side or not
		bool spawnOnLeft = true;

		// for loop with each note holder we have
		for (int i = 0; i < noteHolders.Length; i++ )
		{
			// get a reference to all the notes in madlibs
			GameObject[] allNotesRef = madlibsScript.stickyNotes;
			// a note useage ref to see what note we are not
			int noteNumRef = 0;
			// foreach note check its dropdown
			foreach(GameObject noteRef in allNotesRef)
			{
				// if the note spot is not null then continue
				if(noteRef != null)
				{

					// if we should not use this note, then remove it
					if(noteRef.GetComponent<myNote>().useThisNote == false){ allNotesRef[noteNumRef] = null;}

					// if the name of the note holder contains the word of the note's dropdown category
					if(noteHolders[i].gameObject.transform.name.Contains(noteTracker.noteCategories[noteRef.transform.GetChild(0).GetChild(0).GetComponent<Dropdown>().value]))
					{
						// check if it should also be used (x or check)
						if(noteRef.GetComponent<myNote>().useThisNote == true)
						{
							// then check the value ** this has to compare with all notes || this may be done by removing placed notes 
							
							// check if it has not been added already added to the list
							foreach (string madeNote in listOfSpawnedNotes)
							{
								// if the string checking our note's name exists, then stop this loop
								if(madeNote == noteRef.transform.name){break;}

								// add it (under that note holder and in the correct spot)
								// check if should spawn on left
								if(spawnOnLeft == true)
								{
									// spawn the note
									GameObject cloneNote = Instantiate(noteRef,  noteRef.transform.position, noteRef.transform.rotation);
									// assign the note's parent
									cloneNote.transform.SetParent(noteHolders[i].gameObject.transform);
									// move the note to the correct position
									cloneNote.GetComponent<RectTransform>().anchoredPosition = new Vector3(-27, 161-(60*scaleDirection), 0);
									// increas scale direction
									scaleDirection++;
									// change the next side we spawn it on
									spawnOnLeft = false;
								}// end of should spawn on left
								else
								// should spawn on right
								{
									// spawn the note
									GameObject cloneNote = Instantiate(noteRef,  noteRef.transform.position, noteRef.transform.rotation);
									// assign the note's parent
									cloneNote.transform.SetParent(noteHolders[i].gameObject.transform);
									// move the note to the correct position
									cloneNote.GetComponent<RectTransform>().anchoredPosition = new Vector3(66, 161-(60*scaleDirection), 0);
									// increas scale direction
									scaleDirection++;
									// change the next side we spawn it on
									spawnOnLeft = true;
								}// end of should spawn on right
								

							}// end of for each note made in the string

						}// end of check if this note should be used

					}// end of noteholder contains note category

				}// end of if note is not null

				//increase the note we're on
				noteNumRef++;
			}// end of for each note in all notes

		
			
			
			
			

		}// end of for loop with not holder

	}// end of popuate notes function


}// end of note summary script