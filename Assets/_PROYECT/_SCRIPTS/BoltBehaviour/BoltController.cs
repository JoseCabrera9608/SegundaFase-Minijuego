using UnityEngine;

public class BoltController : MonoBehaviour, I_interact
{

    [SerializeField] private float movementSpeed = 0.01f;
    [SerializeField] private bool beingInteracted;
    [SerializeField] private Animator boltNutAnimator;
    [SerializeField] private float boltTurnAnimationSpeed;
    private float direction;
    [SerializeField] private bool onPosition;
    //[SerializeField] 
    void Start()
    {
        InputReader.onInteract += Interact;
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBolt();
        boltNutAnimator.SetBool("Interacted", beingInteracted);
    }
    public void Interact()
    {
        beingInteracted = !beingInteracted;
        boltNutAnimator.SetBool("Interacted", beingInteracted);
    }

    public void MoveBolt()
    {
        if (beingInteracted && onPosition == false)
        {
            direction = InputReader.Instance.move.x;
            transform.Translate(Vector3.forward * direction * movementSpeed);
            boltNutAnimator.SetFloat("Direction", -direction*boltTurnAnimationSpeed);
            
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            onPosition = true;
            beingInteracted = false;
            movementSpeed = 0;
            direction = 0;
            if(other.TryGetComponent(out HoleController holeController))
            {
                holeController.isFill = true;
            }

        }
    }
}
