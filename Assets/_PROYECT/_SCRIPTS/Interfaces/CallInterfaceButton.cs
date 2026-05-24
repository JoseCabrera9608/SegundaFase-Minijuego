using UnityEngine;

public class CallInterfaceButton : MonoBehaviour, I_interact
{

    [SerializeField] private GameObject interactObject;

    [ContextMenu("InteractButton")]
    public void Interact()
    {
        
        if(interactObject.TryGetComponent(out I_interact interact))
        {
            Debug.Log("Tiene interact");
            interact.Interact();
        }
    }

   
}
