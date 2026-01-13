using UnityEngine;
using System.Collections;

public class NPCDialogue : MonoBehaviour, IInteractable
{
    [Header("Dialogue Lines")]
    [TextArea] public string[] lines;
    public float lineDelay = 2f;

    [Header("NPC Portrait")]
    public Sprite portrait;

    private bool isTalking = false;

    public bool CanInteract()
    {
        return !isTalking;
    }

    public void Interact()
    {
        if (!isTalking)
            StartCoroutine(DialogRoutine());
    }

    private IEnumerator DialogRoutine()
    {
        isTalking = true;

        for (int i = 0; i < lines.Length; i++)
        {
            DialogManager.Instance.ShowDialog(lines[i], portrait);
            yield return new WaitForSeconds(lineDelay);
        }

        DialogManager.Instance.HideDialog();
        isTalking = false;
    }
}
