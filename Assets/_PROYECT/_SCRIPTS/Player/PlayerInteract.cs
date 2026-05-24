using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactLayerMask;
    [SerializeField] private Transform cameraHolder;
    private InputReader inputReader;
    void Start()
    {
        inputReader = GetComponent<InputReader>();
        InputReader.onInteract += CallInteract;
    }

    private void CallInteract()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        if (Physics.Raycast(ray, out RaycastHit raycastHit, interactDistance, interactLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out I_interact interactable))
            {
                interactable.Interact();
            }
        }
        Debug.Log("Call Interact"); 
    }

    private void OnDrawGizmos()
    {
        if (cameraHolder == null) return;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * interactDistance);
    }
}
