using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myNote_Collisions : MonoBehaviour {

    /// this script is to handle collisions the note may have like which category it is in or deleting it
    /// 

    // needs a ref to my note to tell if we should delete or not
    private myNote myNoteScript;
    // needs a ref to note tracker script
    private Note_Tracker myNoteTracker;
    // a reference to the madlibs manager
    private MadLibs_Tracker madlibsTracker;

    // start
    private void Start()
    {
        // get the reference to the script
        myNoteScript = GetComponent<myNote>();
        myNoteTracker = transform.root.GetComponent<Note_Tracker>();
        
    }// end of start

    // constantly detect if it is colliding with a trigger
    private void OnTriggerStay2D(Collider2D trig)
    {
        // if we have no madlibs script ref then stop
        if(GameObject.Find("MadLibs_Manager") == null){ Debug.Log("no reference to mad libs script found."); return;}
        // if we havent gotten the madlibs reference yet
        if(madlibsTracker == null)
        {
            // get the reference to the mad libs script
            madlibsTracker = GameObject.Find("MadLibs_Manager").GetComponent<MadLibs_Tracker>();
        }// end of havent gotten mad libs ref script


        // if we have no ref then stop
        if(myNoteScript == null) { Debug.Log("No ref to 'myNote' script"); return; }
        // if no ref to my note tracker then stop
        if (myNoteTracker == null) { Debug.Log("No ref to 'Note_Tracker' script"); return; }

        //Debug.Log("Trigged with name: " + trig.gameObject.name);
        Debug.Log("Trigged with tag: " + trig.gameObject.tag);

        // if we are done dragging the not
        if (myNoteScript.amDragging == false)
        {
            // if it's touching the trash
            if (trig.name.Contains("Trash")) 
            { 
                // remove this obj from the madlibs
                madlibsTracker.RemoveStickys(gameObject);
                // destroy this from the game
                Destroy(gameObject, 0); 
            }// end of is touching trash

        }// end of done dragging

        // this variable is to track the number/id of the tag we are about to get
        int tracker = 0;

        // for each possible option in note tracker for tags
        foreach(string tag in myNoteTracker.noteCategories)
        {
            // if the triggered object has a tag in there
            if(trig.gameObject.tag == tag)
            {
                // assign it
                myNoteScript.categories.value = tracker;
                // assign the note tag string
                myNoteScript.myTagString = tag;
                // stop
                break;
            }// end of if obj.tag is equal to tag we are comparing
            else
            // if there is no match 
            {
                //increase our tracker
                tracker++;
            }// end of no match

        }// end of for each tag

    }// end of on trigger stay 2d

    // trigger collision exit
    private void OnTriggerExit2D(Collider2D trig)
    {
        // assign the tag to be null or the 0 option
        myNoteScript.categories.value = 0;
    }// end of trig2d exit


    }// end of my note collision sciprt
