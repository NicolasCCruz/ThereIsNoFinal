using UnityEngine;

public class NarratorTrigger : MonoBehaviour
{
    public Narrator narrator;
    public Narrator secondLines;
    private bool pressedOnce = false;
    private short pauseTrig = 0;


    public void TriggerNarrator()
    {
        Debug.Log("Inside TriggerNarrator");
        switch (gameObject.tag)
        {
            case "NoBttn":
                    Debug.Log("Switch case NoBttn");
		            FindAnyObjectByType<NarratorManager>().StartNarrator(narrator);    
                    break;
            // case "YesBttn":
            //         Debug.Log("Switch case YesBttn");
		    //         FindAnyObjectByType<NarratorManager>().StartNarrator(narrator);    
            //         break;        
            case "StartBttn":
                    if (pressedOnce)
                    {
                        Debug.Log("Switch case StartBttn pressedOnce");
		                FindAnyObjectByType<NarratorManager>().StartNarrator(secondLines);    
                        break;
                    }
                    Debug.Log("Switch case StartBttn");
                    pressedOnce=true;
		            FindAnyObjectByType<NarratorManager>().StartNarrator(narrator);    
                    break;
            case "ResumeBttn":
                    if (pressedOnce)   
                    {
                        Debug.Log("Switch case ResumeBttn pressedOnce");
		                FindAnyObjectByType<NarratorManager>().StartNarrator(secondLines);    
                        break;
                    }
                    Debug.Log("Switch case ResumeBttn");
                    pressedOnce=true;
		            FindAnyObjectByType<NarratorManager>().StartNarrator(narrator);    
                    break;
                    // Debug.Log("Switch case ResumeBttn");
		            // FindAnyObjectByType<NarratorManager>().StartNarrator(narrator);    
                    // break;
            case "PauseBttn":
                    Debug.Log("Switch case PauseBttn");
                    // if it tries to call after more than 3 times do nothing
                    // if (narrator.index >= 3)
                    // {
                    //     Debug.Log("Pause has been click three times");
                    //     break;
                    // }
                    
                    // if(narrator.index <= 3 )//&& !pauseTrig)
                    if(pauseTrig < 3 )//&& !pauseTrig)
                    {
                        Debug.Log("inside pause if statement + PT index: " + pauseTrig );
                        
                        //check if it has reach th  ree change teh bool to true and reset the index
                        FindAnyObjectByType<NarratorManager>().PauseTrigger(narrator);    
                        pauseTrig++;
                        break;      
                    }
                    
                    
                    narrator.index = 0;
                    break;

            default:
                Debug.Log("Default");
                break;

        }

        // if (gameObject.CompareTag("NoBttn"))
        //     if(narrator.index > 4)
        //         Debug.Log("narrator index = " + narrator.index);
    }





















/*
    public void TriggerNarrator ()
    {
        Debug.Log("Start nTrig");
        FindAnyObjectByType<NarratorManager>().StartNarrator(narrator);
        Debug.Log("End Start nTrig");
    }
*/


}
