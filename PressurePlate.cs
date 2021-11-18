using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] PuzzleBase puzzle = null;
    [SerializeField] PuzzlePiece piece = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PuzzlePiece>())
        {
            if (collision.gameObject.GetComponent<PuzzlePiece>() == piece)
            {
                puzzle.PuzzleSolved = true;
            }
        }
    }
}
