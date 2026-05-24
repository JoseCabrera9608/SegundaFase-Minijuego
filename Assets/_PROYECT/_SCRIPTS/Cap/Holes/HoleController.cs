using System.Collections;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    public bool isFill;
    public bool isCurrentTurn;
    private Material startMaterial;
    [SerializeField] private Material alertMaterial;
    [SerializeField] private MeshRenderer visualMeshRender;
    public int turn;
    void Start()
    {
        startMaterial = visualMeshRender.material;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator ChangeColor()
    {
        while (!isFill || isCurrentTurn)
        {
            visualMeshRender.material = alertMaterial;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Regresa Al Material Original");
            visualMeshRender.material = startMaterial;
            
        }

    }
    public void StartChangeColor()
    {
        StartCoroutine(ChangeColor());
    }
    public void StopChangeColor()
    {
        StopCoroutine(ChangeColor());
    }
    
}
