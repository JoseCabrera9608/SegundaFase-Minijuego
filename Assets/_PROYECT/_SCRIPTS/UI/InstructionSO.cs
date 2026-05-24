using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/InstructionsText")]
public class InstructionSO : ScriptableObject
{
    public string Instruction;

    public void PlayInstruction()
    {
        InstructionsManager.instance.GetTextDialogue(Instruction);
    }
}
