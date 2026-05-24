using System.Collections;
using TMPro;
using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public static InstructionsManager instance;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float banishTextTimer = 3f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

    }
    public void GetTextDialogue(string dialogue)
    {
        StartCoroutine(HideTextCoroutine());
        text.text = dialogue;
    }

    private IEnumerator HideTextCoroutine()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(banishTextTimer);
        text.gameObject.SetActive(false);
        yield return null;
    }

    public float GetBanishTextTimer()
    {
        return banishTextTimer;
    }

}
