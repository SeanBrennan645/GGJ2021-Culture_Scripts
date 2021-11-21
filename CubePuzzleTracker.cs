using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzleTracker : MonoBehaviour
{
    private int sideCount = 0;
    private int currentSideVal = 0;
    private char[] deleteChars = { 'C', 'u', 'b', 'e', 'S', 'i', 'd' };
    private AudioSource doorOpen;

    private void Start()
    {
        doorOpen = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        string currentSide;
        if(other.transform.name.StartsWith("CubeSide"))
        {
            currentSide = other.transform.name.TrimStart(deleteChars);
            currentSideVal = int.Parse(currentSide);
            //Debug.Log("CurrentSideVal:" + currentSideVal);
            if(currentSideVal == (sideCount + 1))
            {
                sideCount++;
                //Debug.Log("Current Side Count: " + sideCount);
            }
            else
            {
                sideCount = 1;
                //Debug.Log("Side Count reset to:" + sideCount);
            }
            if(sideCount == 6)
            {
                doorOpen.Play();
                //Debug.Log("Puzzle Complete!");
                if(this.GetComponent<PuzzleBase>())
                {
                    this.GetComponent<PuzzleBase>().PuzzleSolved = true;
                }
            }
        }
    }
}
