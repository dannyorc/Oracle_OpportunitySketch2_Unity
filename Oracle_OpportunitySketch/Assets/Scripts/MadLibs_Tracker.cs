using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MadLibs_Tracker : MonoBehaviour
{
    /// this script is intended to track all the sticky notes and their assigned categories. This information will then be available to the madlibs dropdowns
    ///  
    /// it requires reference to all mad libs that need reference to all sticky notes
    /// Production Services
    /// Pains
    /// Gains
    /// 

    // a list of all the sticky notes available
    public GameObject[] stickyNotes;
    // a public set of dropdowns
    // production services
    [Header("Dropdown Products & Services")]
    public Dropdown ddPS;
    // Pains
    [Header("Dropdown Pains")]
    public Dropdown ddPs;
    // Gains
    [Header("Dropdown Gains")]
    public Dropdown ddGs;
    // Verbs
    [Header("Dropdowns Verbs")]
    public Dropdown[] ddVerbs;

    // a list of verbs for the pains and gains
    private string[] verbs = { "", "Opening", "Becoming", "Having",};

  
    // start
    private void Start()
    {       
        // set up the verb options
        AssignVerbs();

    }// end of start

    // update
    private void Update()
    {
        // if we are missing a reference to any of the above needed dropdowns then stop
        if(ddPs == null || ddPS == null || ddGs == null) { Debug.Log("missing a reference to one, or multiple of the dropdowns"); return; }
        
        // we need to constantly have an updated list of all the sticky notes and to know when one is deleted or created. there are several ways to do this, likely better than the one
        // I plan to implement. I will use a timer that every 5 seconds will grab all the sticky notes to a new list replacing the old list of all the sticky notes

       

    }// end of Update

    // a function to add a sticky note
    public void AddStickys(GameObject newSticky)
    {
         
        // each note needs an id to reference later

        // when one is created it will go to this Get Sticky function and state the "stickyNotes[]" array as a new array that equals itself + this new note 
        //(the length must equal its previous length + 1, and that note's id can be access later when deleting itself from the array)

        // temporary blank array the size of the current array +1
        GameObject[] resize = new GameObject[stickyNotes.Length+1];
        // add the new gameobject to it
        resize[resize.Length-1] = newSticky;
        // loop through each of stickynote's objects
         for (int i = 0; i < stickyNotes.Length; i++ ) 
         {
            // assign the note
            resize[i] = stickyNotes[i];
        }// end of loop through sitckynote obj
        // update our sticky note array
        stickyNotes = resize;

        // update the notes text
        UpdateDropDowns();
    }// end of add stickys


    // a function to remove stickys from the array
    public void RemoveStickys(GameObject sticky)
    {
        // this function will probablly take the gameobject from the "myNoteCollision" script or at least an integeder/id and remove it (maybe)
        // the notes will need to be removed if deleted, but a reference to the spot they had may still be needed (not sure yet)

       
        // loop through each sticky note in the stickynotes array
        for (int i = 0; i < stickyNotes.Length; i++ ) 
         {
             // if it has the same name is this obj and is not null
             if(stickyNotes[i] != null && stickyNotes[i].transform.name == sticky.transform.name)
             {
                 // remove it
                 stickyNotes[i] = null;
                 // stop the loop
                break;
             }// end of has the same name

         }// end fof loop stickynotes
        
    }// end of remove stickys




    // a function to re-assign all possible options for the dropdowns
    public void UpdateDropDowns()
    {
        // for each gameObject in sicky note
        foreach (GameObject sticky in stickyNotes)
        {
            // for each child the note as
            foreach(Transform child in sticky.transform.GetChild(0))
            {
                // if the child's name == x
                if(child.name == "InputField_Details")
                {
                    // for each child in my note
                    foreach(Transform myNoteText in child)
                    {         
                        // if the my note text name == Y
                        if(myNoteText.name == "Text_Info")
                         {
                         // get a ref to the text component
                          Text noteText = myNoteText.GetComponent<Text>();
                          // create a list to convert the string
                          List<string> SomeName = new List<string>();
                          // add the list to the string
                          SomeName.Add(noteText.text);
                        
                          // check which category it was in 
                          // pains
                         if(sticky.GetComponent<myNote>().myTagString == "Pains") 
                         {
                             // a ref if we have this option already
                             bool haveThis = false;

                            //  //Take each entry in the message List
                            // for (int i = 1; i < ddPs.options; i++ ) 
                            // {
                            //     // if an existing option = the option we want to add
                            //     if(message == SomeName)
                            //     {
                            //         // say we do have this
                            //         havethis = true;
                            //         // stop
                            //         break;
                            //     }
                            // }// end of for each dropdown
                             
                             // if not, add it
                          ddPs.AddOptions(SomeName); 
                         }// end of pains

                         // gains
                         if (sticky.GetComponent<myNote>().myTagString == "Gains") 
                         { 
                             ddGs.AddOptions(SomeName);
                         }// end of gains

                        // production services
                         if (sticky.GetComponent<myNote>().myTagString == "Products & Services") 
                         { 
                             ddPS.AddOptions(SomeName); 
                        }// end of production srrvices

                         // stop looking
                         break;
                        }// end of the childs name == Y  

                    }// end of for each child in mynote    

                }// end of child's name == X     

            }// end of for each child

        }// end of for each gameobject in stickynotes

    }// end of update dropdowns


    // a function to assign possible verbs
    private void AssignVerbs()
    {
        // if we have no ref to verbs dorpdowns then stop
        if (ddVerbs.Length == 0 || ddVerbs == null) { Debug.Log("dropdown Verbs has no reference"); return; }

        // for each verb dropdown
        foreach (Dropdown dp in ddVerbs)
        {
            // create a list to convert the string
            List<string> listToString = new List<string>();
            // clear all the current options
            dp.ClearOptions();           

            // for each string in our list of verbs
            foreach (string verbOption in verbs)
            {
                // add the list to the string
                listToString.Add(verbOption);
                // add everything in the list to the options in dropdown
                dp.AddOptions(listToString);

            }// end of for each string

        }// end of for each dropdown

    }// end of assign verbs



}// end of madlibs tracker