using UnityEngine;

public class MovementHandle : MonoBehaviour
{

    private InputReader inputReader;
    private PlayerData playerData;
    private float lookRotation;
    void Start()
    {
        inputReader = GetComponent<InputReader>();
        playerData = GetComponent<PlayerData>();
        playerData.camHolder.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        RotationByMouse();
        Movement();
    }

    private void Movement()
    {
        Vector3 move = transform.right* inputReader.move.x + transform.forward* inputReader.move.y;
        move.y = 0f;
        if(move.sqrMagnitude > 1f)
        {
            move.Normalize();
        }
        transform.position += move * playerData.playerSpeed * Time.deltaTime;
    }

    private void RotationByMouse()
    {
        transform.Rotate(Vector3.up * inputReader.look.x * playerData.mouseSensitivity);
        lookRotation += (-inputReader.look.y * playerData.mouseSensitivity);
        lookRotation = Mathf.Clamp(lookRotation, -playerData.minLookRotation, playerData.maxLookRotation);
        playerData.camHolder.eulerAngles = new Vector3 (lookRotation, playerData.camHolder.transform.eulerAngles.y, playerData.camHolder.transform.eulerAngles.z);
    }


}
