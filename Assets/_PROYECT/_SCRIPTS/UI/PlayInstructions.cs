using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInstructions : MonoBehaviour
{
    [SerializeField] private List<InstructionSO> instructionsList = new List<InstructionSO>();
    [SerializeField] private int instructionTurn;


    
    private void Start()
    {
    }
    public enum InstructionsForm
    {
        consecutive,
        byInteract
    }
    [SerializeField] private InstructionsForm form;
    public void PlayInstructionText()
    {
        switch (form)
        {
            case InstructionsForm.consecutive:
                StartCoroutine(ConsecutiveInstructions());
                break;
            case InstructionsForm.byInteract:
               
                    PlayInstruction(instructionTurn);
                    instructionTurn++;
                break;
        }
    }

    IEnumerator ConsecutiveInstructions()
    {
        for (int i = 0; instructionTurn < instructionsList.Count; i++)
        {
            instructionsList[instructionTurn].PlayInstruction();
            yield return new WaitForSeconds(InstructionsManager.instance.GetBanishTextTimer());
            instructionTurn++;
        }
        if(instructionTurn>= instructionsList.Count)
        {
            instructionTurn = 0;
        }
    }
    private void PlayInstruction(int i)
    {
        instructionsList[i].PlayInstruction();
        
    }
}
