using System.Collections;
using Unity.Cinemachine;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameResetManager : MonoBehaviour
{
    public static GameResetManager Instance;

    [SerializeField] private Animator fadeInAnimator;
    private float fadeInMultiplierSpeed = 1f;
    [SerializeField] private CinemachineCamera cinemachineOnDeadCamera;
    bool isReseting;
    private void Start()
    {
        Instance = this;
    }

    public void OnResetGame()
    {
        if (isReseting) { return; }
        isReseting = true;
        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        cinemachineOnDeadCamera.Priority = 20;
        yield return new WaitForSeconds(5f);
        fadeInAnimator.SetFloat("Multiplier", -fadeInMultiplierSpeed);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }


}
