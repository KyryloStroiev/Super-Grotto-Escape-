using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{

    public TextMeshProUGUI DialogText;
    public TextAsset DialogueFile;

    public event Action EndDialogue;
    
    private List<Dialogue> _npsDialogues;
    private int currentDialogueIndex = 0;

    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        LoadDialoguesFromJson();
        ChangeDialogue();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            ChangeDialogue();
        }
    }

    private void ChangeDialogue()
    {
        DialogText.text = CombineSentences(_npsDialogues[currentDialogueIndex].sentences);
        currentDialogueIndex++;
        if (currentDialogueIndex >= _npsDialogues.Count)
        { 
            EndDialogue?.Invoke();
            Destroy(gameObject);
        }
    }

    private void LoadDialoguesFromJson() => 
        _npsDialogues = JsonUtility.FromJson<DialogueList>(DialogueFile.text).Dialogues;

    private string CombineSentences(string[] sentences)
    {
        StringBuilder combine = new StringBuilder();

        foreach (string sentence in sentences)
        {
            combine.AppendLine(sentence);
        }
        return combine.ToString();
    }
}