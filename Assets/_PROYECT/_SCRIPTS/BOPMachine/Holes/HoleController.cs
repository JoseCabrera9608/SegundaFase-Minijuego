using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoleController : MonoBehaviour, IPointerClickHandler
{
    public bool isFill;
    public bool isCurrentTurn;
    private Material startMaterial;
    [SerializeField] private Material alertMaterial;
    [SerializeField] private Material errorMaterial;
    [SerializeField] private MeshRenderer visualMeshRender;

    public UnityEvent<HoleController> onHoleClicked;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked");
        onHoleClicked?.Invoke(this);
    }
    public void FlashError()
    {
        StartCoroutine(FlashErrorRoutine());
    }

    IEnumerator FlashErrorRoutine()
    {
        visualMeshRender.material = errorMaterial;
        yield return new WaitForSeconds(0.5f);
        visualMeshRender.material = startMaterial;
    }

}
