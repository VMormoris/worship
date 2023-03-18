using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterlocutorScript : MonoBehaviour
{
    public string[] Conversations;
    
    public void StartConversation()
    {
        DialogueSystem dialogueSystem = GameContext.Instance.DialogPanel.GetComponent<DialogueSystem>();
        int index = FindNextIndex();
        if(index != -1)
            dialogueSystem.StartConversation(Conversations[index]);
    }

    protected abstract int FindNextIndex();

}
