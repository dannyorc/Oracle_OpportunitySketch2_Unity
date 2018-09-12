using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note_Tracker : MonoBehaviour {

    /// this script tracks the important rules/important parts of the note creations and their categories.

    // all possible categories for sticky notes
    [HideInInspector]
    public string[] noteCategories = { "", "Gains", "Pains", "Jobs" , "Pain Killers", "Products & Services", "Gain Creators" };  
    // the gameobject ref to clone all of our sticky notes
    public GameObject stickyNoteObj;
    // a colelction/reference to all notes made
    private Image[] allNotes;
    // the size of every note
    [HideInInspector]
    [Range(0.05f, 1.0f)]
    public float noteSize = 0.2f;
    // a variable to track each note's id 
    // (this may cause an issue when loading saved notes) ** may allow for a look through the mad libs notes ref and count the length of the array
    private int nextNoteId = 0;
    // a reference to the madlibs gameObject and script
    private MadLibs_Tracker madLibsScript;


    // a function to add a note to the screen
    public void AddNote(Transform parent)
    {
        // if there is not sticky note ref to clone then  stop
        if(stickyNoteObj == null) { Debug.Log("no sticky note object to ref on: " + transform.name); return; }
        // if we have no madlibs script ref then stop
        if(GameObject.Find("MadLibs_Manager") == null){ Debug.Log("no reference to mad libs script found. on: " + transform.name); return;}

        // if we havent gotten the madlibs reference yet
        if(madLibsScript == null)
        {
            // get the reference to the mad libs script
            madLibsScript = GameObject.Find("MadLibs_Manager").GetComponent<MadLibs_Tracker>();
        }// end of havent gotten mad libs ref script
       
        // make a clone ref for the new sticky
        GameObject stickyClone;
        // clone the reference sticky
        stickyClone = Instantiate(stickyNoteObj, stickyNoteObj.transform.position, stickyNoteObj.transform.rotation);
        // set the parent to the canvas
        stickyClone.transform.SetParent(parent);
        // set the scale to the appropriate size
        stickyClone.GetComponent<RectTransform>().localScale = new Vector3(noteSize, noteSize, noteSize);
        // set the position to be correct now      
        stickyClone.GetComponent<RectTransform>().anchoredPosition = new Vector3(-200, 200, 0);
        // assign a new name to the sticky note
        stickyClone.transform.name = stickyClone.tag + "_" + nextNoteId;
        // set the id of the sticky note
        stickyClone.GetComponent<myNote>().noteId = nextNoteId;
        // increase the next note id number
        nextNoteId++;
        // add the note to the list of notes created in madlibs
        madLibsScript.AddStickys(stickyClone);
    }// end of add note


    // a function to update the note size
    public void UpdateNoteSize(Slider sizeSlider)
    {
        // update the size
        noteSize = sizeSlider.value;
    }// end of Update note size

     


}// end of not tracker
