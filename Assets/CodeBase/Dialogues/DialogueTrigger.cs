using System;
using CodeBase.StaticData.Level;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject DialogueUI;
    public Collider2D Colliders;
    public event Action OpenDoor;
    private string FinishScene = LevelId.Finish.ToString();
    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(DialogueUI);
        if (SceneManager.GetActiveScene().name == FinishScene)
        {
            OpenDoor?.Invoke();
        }
        Colliders.enabled = false;
    }
}
