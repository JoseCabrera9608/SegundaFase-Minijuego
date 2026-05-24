using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class CapController : MonoBehaviour, I_interact
{
    [SerializeField] private List<HoleController> holesList = new List<HoleController>();
    [SerializeField] private bool isInteracted;

    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private int turn;
    
    private void Start()
    {
        textMeshPro.gameObject.SetActive(false);
    }
    [ContextMenu("Interact")]
    public void Interact()
    {
        if (!isInteracted)
        {
            isInteracted = true;
            textMeshPro.gameObject.SetActive (true);
            textMeshPro.text = "Turn: " + (turn + 1);
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
            textMeshPro.text = "Turn: " + (turn+1);
        }
        if(turn>= holesList.Count)
        {
            turn = 0;
            isInteracted = false;
            textMeshPro.gameObject.SetActive(false);
        }
        
    }
    
}
