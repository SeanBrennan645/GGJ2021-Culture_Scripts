using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : ObjectSlider
{
    [SerializeField] PuzzleBase puzzle = null;


    private bool doorOpened = false;
    // Start is called before the first frame update


    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(PuzzleCheck())
        {
            Moving = true;
        }
    }

    void OpenDoor()
    {
        Moving = true;
    }

    bool PuzzleCheck()
    {
        if(puzzle.PuzzleSolved && !doorOpened)
        {
            doorOpened = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
