using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float playerSpeed;
    [Range(0f,1f)]
    public float mouseSensitivity;
    public float minLookRotation = 80f;
    public float maxLookRotation = 80f;
    public Transform camHolder;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
