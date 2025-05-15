using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class NarratorManager : MonoBehaviour
{

	public TextMeshProUGUI narratorText;

	[TextArea(3, 10)]
	public string[] nmLines;

	public float textSpeed;
	public int nmIndex;
	public bool doneTalking;
	public bool bttnTrig;
	public float pauseAfterLine = 1.5f;

	// using narrator class
	private string[] lines;
	public int index;
	private bool pauseBttnTrig;
	// public Narrator secondLines;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		// lines = new Queue<string>();
		narratorText.text = string.Empty;
		doneTalking = true;
		nmIndex = 0;

		StartNarratorManager();
	}

	void Update()
	{
		//Checck if narrator is done talking
		if (doneTalking)
		{
			Debug.Log("index : " + index + " && lines.Length ; " + lines.Length);

			//just go to displaynextline
			DisplayNextline();
		}
	}

	public void StartNarratorManager()
	{

		bttnTrig = false;
		// set up lines and index for each narrator option 
		if (doneTalking)
		{
			lines = nmLines;
			index = nmIndex;
			doneTalking = false;

			// DisplayElements(lines);

			DisplayNextline();
			nmIndex++;

		}
	}

	public void StartNarrator(Narrator narrator) //()
	{
		bttnTrig = true;
		// set up lines and index for each narrator option 
		// if (doneTalking)
		// {
		// doneTalking = false;
		lines = narrator.lines;
		index = narrator.index;

		// DisplayElements(narrator.lines);

		StopAllCoroutines();
		DisplayNextline();
		// narrator.index++;
		// }
	}

	public void DisplayNextline()
	{
		Debug.Log("index in DPL: " + index);
		doneTalking = false;

		if (index < (lines.Length)) // 0 < (3), 1<(3), 2<(3) 
		{
			// index++;
			narratorText.text = string.Empty;
			StopAllCoroutines();
			StartCoroutine(Typeline());
			// doneTalking = true;
		}
		else
		{
			Debug.Log("Extra Line");
			// narratorText.text = "No More Lines";
			doneTalking = true;

			DisplayExtraLine();

		}

	}

	// IEnumerator Typeline (string line)
	IEnumerator Typeline()
	{
		Debug.Log("index in TL: " + index);
		foreach (char c in lines[index].ToCharArray())
		{
			narratorText.text += c;
			yield return new WaitForSeconds(textSpeed);
		}
		index++;

		// wait for reader to finish reading
		yield return new WaitForSeconds(pauseAfterLine);


		// if not trigger set bool to true
		// if(!bttnTrig)
		if (!pauseBttnTrig)
			doneTalking = true;


	}

	// Debug: Display the lines[] on unity console
	private void DisplayElements(string[] lines)
	{
		// Diplay all lines elements
		foreach (string line in lines)
		{
			Debug.Log(line);
		}

	}

	public void PauseTrigger(Narrator narrator)
	{
		// stop the update 
		// once call do one text
		// increment the index
		// after it finish line clear text until next call
		// check if the index is equal to three then reset it 


		// bttnTrig = true;
		pauseBttnTrig = true;
		lines = narrator.lines;
		index = narrator.index;
		// StopAllCoroutines();
		DisplayNextline();
		Debug.Log("PT Line [" + nmIndex + " ] is : " + lines[nmIndex]);

		// check if pressed three times


		// narratorText.text = string.Empty;
		doneTalking = false;
		narrator.index++;

		Debug.Log("Pause index : " + index);

		if (index == 2)
		{
			Debug.Log("Inside pause trigger if statement");
			// bttnTrig=false;
			pauseBttnTrig = false;
			narrator.index = 0;
			narratorText.text = string.Empty;
			doneTalking = true;

			// StartNarrator(secondLines);

		}

	}

	void DisplayExtraLine()
	{
		/*
		*** Have an array of at least 10 lines
		*** Randomize between the lines and choose one
		*** Clear the narrator text and 
		*** Display the chosen line 
		*** 
		*/
		// const int sleLength = 10; 
		// Added "..." and "   " for spacing
		
		string[] extraLines = 	
					{"Still here? Wow, your patience is *really* something."
					,"   "
					,"You know, I'm starting to think you like doing nothing."
					,"   "
					,"Nothing happening? Well, you're not helping either, are you?"
					,"...   "
					,"Wow, not even a click? Are you just here to watch me talk?"
					,"   "
					,"Don't be shy. I know you want to click. What's stopping you?"
					,"   "
					,"   "
					,"You're waiting for a sign? Here it is: *click something already.*"
					,"   "
					,"Is this your idea of fun? Just sitting there like a mannequin?"
					,"...   "
					,"   "
					,"Look, I'm fine with this, but I'm *really* not sure you are."
					,"   "
					,"You've been here a while. Should I send you a participation trophy?"
					,"   "
					,"Still nothing? Well, I guess I'll just keep talking then..."};
		int tmpIndex = 0;
		string tmpStr = "";
		string[] tmpLine = {""};
		var random = new System.Random();
		tmpIndex = random.Next(0,extraLines.Length);
		Debug.Log("random int : " + tmpIndex);
		Debug.Log("eL : " + extraLines[tmpIndex]);
		tmpStr = extraLines[tmpIndex];
		tmpLine[0] = tmpStr;
		Debug.Log("tL : " + tmpLine[0]);

		lines = tmpLine;
		index=0;
		narratorText.text = string.Empty;
		StopAllCoroutines();
		StartCoroutine(Typeline());


	}
	
}
