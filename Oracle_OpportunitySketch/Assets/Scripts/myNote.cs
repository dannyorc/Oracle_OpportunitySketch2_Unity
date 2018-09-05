using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class myNote : MonoBehaviour {

    /// <summary>
    ///  this script tracks an individual note's stats and hold ref to the note tracker
    /// </summary>

    // the script with ref to all notes and options
    [HideInInspector]
    public Note_Tracker noteManager;
    // a ref to the canvas
    private Canvas mainCanvas;
    // the dropdown containing all the category options
    [HideInInspector]
    public Dropdown categories;

    // a variable to track this note's id (to save and delete from later
    [HideInInspector]
    public int noteId = -1;

    // a bool tracking if our mouse is on the note drag spot
    private bool canDragThis;
    // an obj to reference when we click and drag
    [HideInInspector]
    public bool amDragging;

    // trackers for the color picker 
    private bool colorPickerOpen;
    // all the objects we want to change color for
    public Image[] changeTheseColors;

    // the tag/string this note is tagged
    [HideInInspector]
    public string myTagString = "";

    // this note needs a variable for if it's to be used or not (the X or CHECKMARK)
    [HideInInspector]
    public bool useThisNote;

    // a variable to track this note's importants
    [HideInInspector]
    [Range(0,1)]
    public float myValue;
    // the slider that will change this note's value
    private Slider valueSlider;

   
    // Use this for initialization
    private void Start () {
        noteManager = transform.root.GetComponent<Note_Tracker>();
        // if no note manager stop
        if(noteManager == null) { Debug.Log("No Ref to noteManager"); return; }       


        // check each child for the value slider
        foreach (Transform child in transform.GetChild(0))
        {
            // if the name is the same as the categories dropdown || assign it and stop looking
            if (child.name == ("Dropdown_Types")) { categories = child.GetComponent<Dropdown>(); break; }
            // if not then log it
            else { Debug.Log("No Ref to categories/types dropdown"); return; }
        }// end of for each child

        // check each child for the value slider
        foreach (Transform child in transform.GetChild(0))
        {      
            // if the name is the same as the value slider || assign it / assign the value to our variable and stop looking
            if (child.name == ("Slider_Value_Important")) { valueSlider = child.GetComponent<Slider>(); myValue = valueSlider.value; break; }          
        }// end of for each child
                
     
        // set can drag this to false so we cannot drag the item yet
        canDragThis = false;
        // we are not dragging
        amDragging = false;
        // set color picker to off
        colorPickerOpen = false;
        // we do want to use this note
        useThisNote = true;
        // assign main canvas
        mainCanvas = transform.parent.GetComponent<Canvas>();

        // for each option we have in categories (ie gain, pain, jobs...)
        foreach (string option1 in noteManager.noteCategories)
        {
            // create a list to convert the string
            List<string> SomeName = new List<string>();
            // add the list to the string
            SomeName.Add(option1);
            // add everything in the list to the options in dropdown
            categories.AddOptions(SomeName);
        }// end of for each option
		
	}// end of start

    // update function
    private void Update()
    {
        // update my size based on the scale
        GetComponent<RectTransform>().localScale = new Vector3(noteManager.noteSize, noteManager.noteSize, noteManager.noteSize);


        // if we are dragging
        if (amDragging == true)
        {
            // set this objects transform to the mouse's (converted to Vector2)
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(mainCanvas.transform as RectTransform, Input.mousePosition, mainCanvas.worldCamera, out pos);
            transform.position = mainCanvas.transform.TransformPoint(pos);
        }// end of am dragging
        else
        // if we are not dragging
        {
            // my position = mouse position
            transform.position = transform.position;
        }// end of not dragging

        // if the color picker is open
        if (colorPickerOpen == true)
        {
            // if we click
            if (Input.GetMouseButton(0))
            {                          
            
            }// end fo if we click

        }// end of color picker is open

    }// end of Update



    // a function for when we mouse over 
    public void MouseEnter()
    {
        // set can drag this to true so we can drag the item
        canDragThis = true;

    }// end of mouse enter

    // a  function for when we mouse exit
    public void MouseExit()
    {
        // set can drag this to false so we cannot drag the item anymore
        canDragThis = false;
    }// end of mouse exit

    // this function calls when we mouse down on a UI with the event option to mouse over
    public void MoussedDown()
    {
        // if we click but our mouse is not on the obj then stop
        if (canDragThis == false) { return; }
        // if canvas is null then stop
        if(mainCanvas == null) { Debug.Log("no canvas to reference for " + transform.name); return; }
        // my position = mouse position
        amDragging = true;
    }// end of clicked mosue

    // this function calls when we mouse up
    public void MoussedUp()
    {    
        // we are not dragging
        amDragging = false;
       
    }// end of unclicked mouse

    // a function to switch the color picker on and off
    public void BoolColorPicker()
    {
        // whatever the color picker bool =... change it to the opposite
        colorPickerOpen = !colorPickerOpen;
    }// end of color picker function

    // a function to change the note color
    public void ChangeNoteColor(string newColor)
    {
        // new Color can equal "Blue" , "Green", "Yellow"    
       

        // for each color we want to chaneg
        foreach (Image changeMe in changeTheseColors)
        {
            // assign the new color
            if (newColor == "Blue") { changeMe.color = Color.cyan; }
            if (newColor == "Green") { changeMe.color = Color.green; }
            if (newColor == "Yellow") { changeMe.color = Color.yellow; }


        }// end of for each color

    }// end of change note color


    // a function to grab the slider


    // a fucntion to change the note's value
    public void ValueUpdate()
    {
        // if the slider is null then stop
        if(valueSlider == null) { Debug.Log("no slider reference for value"); return; }

        // change the value to the slider's value
        myValue = valueSlider.value;

    }// end of value update 


}// end of myNote script
