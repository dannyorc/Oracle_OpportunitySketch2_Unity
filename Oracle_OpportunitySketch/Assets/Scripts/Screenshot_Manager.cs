using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class Screenshot_Manager : MonoBehaviour {

    /// a script to take screenshots || ** This NEEDS to be updated to work with HTML5 / WebGL builds
    /// 

    private string filePath = "";
    private string folderName = "OpportunitySketch_";
    private int folderID = 0;
    private string fileType = ".png";
    private int screenshotID = 0;

    // update
    private void Update()
    {
         Debug.Log("1* Dont forget to comment this line out when you want to take screenshots"); return;
         
        // if we dont have a file path yet, make one
        if(filePath == "") { CreateFolder(); }

    }// end of update

    // the folder function
    private void CreateFolder()
    {
        Debug.Log("2* Dont forget to comment this line out when you want to take screenshots"); return;

        // get the path name
        filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/" + folderName + folderID;
        // if there is not already one
        if (!Directory.Exists(filePath))
        {
            Debug.Log("No existing folder in path: " + filePath);
            // create one
            Directory.CreateDirectory(filePath);
        }// end of not already one
        else
        // if we do have one
        {
            Debug.Log("Found folder in path: " + filePath);
            // increase the id
            folderID++;
            // call this function again
            CreateFolder();
            // stop
            return;
        }// end of do have folder

        // call the screenshot function
        //Screenshot();
    }// end of create folder function

    // a fucntion to take screenshots
    public void Screenshot()
    {
        //print("System.DateTime.Now.TimeOfDay: " + System.DateTime.Now.TimeOfDay); 

        // convert system time and date to variables
        //System.DateTime theTime = System.DateTime.Now;
        //string date = theTime.Year + "_" + theTime.Month + "_" + theTime.Day;
        //string time = date + "_" + theTime.Hour + ":" + theTime.Minute + ":" + theTime.Second;
        //date += "Z";
        //time += "Z";
        // an attempt to save with date and time
        //ScreenCapture.CaptureScreenshot(filePath + "/" + date + time + fileType);

        // save the screenshot to the fodler path with an id    
        ScreenCapture.CaptureScreenshot(filePath + "/_" + screenshotID + fileType);
        // increase the id to save multiple screenshots in 1 session
        screenshotID++;

        

    }// end of Screenshot


}// end of screenshot manager
