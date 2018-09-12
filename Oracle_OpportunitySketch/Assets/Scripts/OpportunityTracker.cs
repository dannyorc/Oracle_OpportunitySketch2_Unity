using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpportunityTracker : MonoBehaviour {


/// this script should monitor and track all the opportunities and their stats. an opportunity can be created or imported and it should
/// pass information through this script so it can all be saved later.
/// this includes
/// 	- the opportunity name
/// 	- how many sub-opps it has (customers, workers, admin, hr, ... so on) * they will each be assigned an ID 1-5
/// 	- how many circle charts and how many square charts
///		- how many sticky notes and each one's information (transform, text, color, used/not used, value, which chart they're under - *each note might need record)
/// 	- how many mad libs we have and each one's current filled in info (** im not sure how to do this yet)

// ** might need an ID to assign it **

// an array of oppoertunity names
private string[] opportunities;
// the number of sub opportunities each opp has
private int[] subOppNumber;
// an array for the sub opportunities
private string[] subOpportunities;
// the number of circle charts and square charts (they always come in a pare)
private int[] chartPairNumber;
// an array of all the sticky notes in existance (may need to take from madlibs tracker)
private GameObject[] allStickys;
// the number of madlibs there are
private int[] madLibsNumber;
// an array string of the madlibs section - this might take a list of the assigned variables into 1 string 
// i.e. { Dropdown option/value, text input, text input, dropdown option/value}
private string[] madlibsString;



}// end of opportunity tracker
