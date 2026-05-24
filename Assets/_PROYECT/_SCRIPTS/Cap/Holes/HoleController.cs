using System.Collections;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    //public bool isFill;
    public bool isCurrentTurn;
    private Material startMaterial;
    [SerializeField] private Material alertMaterial;
    [SerializeField] private MeshRenderer visualMeshRender;
    //public int turn;
    void Start()
    {
        startMaterial = visualMeshRender.material;
        
    }

    IEnumerator ChangeColor()
    {
        while (isCurrentTurn)
        {
            visualMeshRender.material = alertMaterial;
            yield return new WaitForSeconds(0.5f);
            visualMeshRender.material = startMaterial;
            yield return new WaitForSeconds(0.5f);
        }
        visualMeshRender.material = startMaterial;

    }

    [ContextMenu("StartChangeColor")]
    public void StartChangeColor()
    {
        StartCoroutine(ChangeColor());
    }
    
    
}
