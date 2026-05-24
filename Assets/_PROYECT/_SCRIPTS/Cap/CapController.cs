using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class CapController : MonoBehaviour, I_interact
{
    [SerializeField] private List<HoleController> holesList = new List<HoleController>();
    [SerializeField] private bool isInteracted;


    [SerializeField] private int turn;
    [ContextMenu("Interact")]
    public void Interact()
    {
        if (!isInteracted)
        {
            isInteracted = true;
            StartCoroutine(Sequence());
        }
    }

    IEnumerator Sequence()
    {
        
        for (int i = 0; i < holesList.Count; i++)
        {
            holesList[i].isCurrentTurn = true;
            holesList[i].StartChangeColor();
            yield return new WaitForSeconds(2f);
            holesList[i].isCurrentTurn = false;
            turn++;
        }
        if(turn>= holesList.Count)
        {
            turn = 0;
            isInteracted = false;
        }
        
    }
    
}
