using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using Unity.Cinemachine;
using System;
public class CapController : MonoBehaviour, I_interact
{
    public enum MachineMode
    {
        Memorize,
        Practice,
        Reality
    }
    [Header("MachineMode")]
    public MachineMode machineMode;

    [Header("Holes Variables")]
    [SerializeField] private List<HoleController> holesList = new List<HoleController>();
    [Header("OnPractice Variables")]
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private GameObject boltPrefab;
    [SerializeField] private float boltSeparationFromHole;
    [SerializeField] private CinemachineCamera practiceCamera;


    [Header("OnReality Variables")]
    [SerializeField] private ParticleSystem explosionPrefab;
    
    [Header("Debug Variables")]
    [SerializeField] private int turn;
    [SerializeField] private bool isInteracted = false;

    private int errorCount;
    private bool waitingBolt;
    private bool isActive = false;
    GameObject currentBolt;
    BoltController currentBoltController;
    PlayInstructions playInstructions;
    public static event Action onMachineFocus;
    List<GameObject> boltsInMachine = new List<GameObject>();
    private BoxCollider machineBoxCollider;
    private void Start()
    {
        playInstructions = GetComponent<PlayInstructions>();
        machineBoxCollider = GetComponent<BoxCollider>();
        textMeshPro.gameObject.SetActive(false);
        practiceCamera.Priority = 0;

        if(machineMode == MachineMode.Practice || machineMode == MachineMode.Reality)
        {
            foreach (var hole in holesList)
            {
                hole.onHoleClicked.AddListener(OnSelectedHole);
            }
        }

    }

    public void Interact()
    {
        if (!isInteracted)
        {
            isInteracted = true;
            isActive = true; 
            machineBoxCollider.enabled = false;
            

            switch (machineMode)
            {
                case MachineMode.Memorize:
                    StartCoroutine(Sequence());
                    playInstructions.PlayInstructionText();
                    break;
                case MachineMode.Practice:
                    onMachineFocus?.Invoke();
                    practiceCamera.Priority = 20;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    break;
                case MachineMode.Reality:
                    onMachineFocus?.Invoke();
                    practiceCamera.Priority = 20;
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    break;
            }
            textMeshPro.gameObject.SetActive (true);
            UpdateTextTurn(turn);
        }
    }
    public void UpdateTextTurn(int turn)
    {
        textMeshPro.text = "Turn: " + (turn + 1);
    }

    #region ForMemorizeMode
    IEnumerator Sequence()
    {
        
        for (int i = 0; i < holesList.Count; i++)
        {
            holesList[i].isCurrentTurn = true;
            holesList[i].StartChangeColor();
            yield return new WaitForSeconds(2f);
            holesList[i].isCurrentTurn = false;
            turn++;
            textMeshPro.text = "Turn: " + (turn+1);
        }
        if(turn>= holesList.Count)
        {
            turn = 0;
            isInteracted = false;
            textMeshPro.gameObject.SetActive(false);
        }
        
    }
    #endregion

    #region Practice and RealityMode

    private void OnSelectedHole(HoleController currentHole)
    {
        Debug.Log("OnSelectedHoleCalled");
        if(!isActive || waitingBolt) { return; }
        Debug.Log("OnSelectedHolePass");
        int holeID = holesList.IndexOf(currentHole);
        if (holeID < 0) {  return; }
        if(holeID == turn)
        {
            Debug.Log("Es el hoyo indicado");
            OnCorrectHole(currentHole);
        }
        else
        {
            Debug.Log("Hoyo equivocado");
            OnWrongHole(currentHole);
        }
    }

    void OnCorrectHole(HoleController hole)
    {
        hole.isCurrentTurn = false;
        SpawnBolt(hole);
    }

    void OnWrongHole(HoleController hole)
    {
        switch (machineMode)
        {
            case MachineMode.Practice:
                errorCount++;
                hole.FlashError();
                playInstructions.PlayInstructionText();
                if (errorCount >= 3)
                {
                    isActive = false;
                    isInteracted = false;
                    errorCount = 0;
                    turn = 0;
                    practiceCamera.Priority = 0;
                    ResetBolts();
                    ResetHoles();
                    onMachineFocus.Invoke();
                    machineBoxCollider.enabled = true;

                }
                break;

            case MachineMode.Reality:
                explosionPrefab.Play();
                GameResetManager.Instance.OnResetGame();
                
                break;
        }
    }
    void ResetHoles()
    {
        foreach (var hole in holesList)
        {
            hole.isFill = false;
        }
    }

    void ResetBolts()
    {
        foreach(GameObject bolts in boltsInMachine)
        {
            Destroy(bolts);
        }
    }
    void SpawnBolt(HoleController hole)
    {
        waitingBolt = true;
        Vector3 spawnPos = hole.transform.position + hole.transform.forward * boltSeparationFromHole;
        currentBolt = Instantiate(boltPrefab, spawnPos, hole.transform.rotation);
        boltsInMachine.Add(currentBolt);
        currentBoltController = currentBolt.GetComponent<BoltController>();
        currentBoltController.beingInteracted = true;
        StartCoroutine(WaitForBoltOnPosition(hole));
    }
    IEnumerator WaitForBoltOnPosition(HoleController hole)
    {
        while (currentBoltController != null && !currentBoltController.onPosition)
            yield return null;

        OnBoltPlaced(hole);
    }

    void OnBoltPlaced(HoleController hole)
    {
        waitingBolt = false;
        turn++;
        UpdateTextTurn(turn);
        if (turn >= holesList.Count)
        {
            isActive = false;
            isInteracted = false;
            errorCount = 0;
            turn = 0;
            practiceCamera.Priority = 0;
            ResetBolts();
            ResetHoles();
            onMachineFocus.Invoke();
            textMeshPro.text = "lo lograste";
            machineBoxCollider.enabled = true;
        }

    }

    #endregion


}
