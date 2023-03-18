using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterlocutorScript : MonoBehaviour
{
    public string[] Conversations;
    
    private int mIndex = -1;

    virtual public void StartConversation()
    {
        DialogueSystem dialogueSystem = GameContext.Instance.DialogPanel.GetComponent<DialogueSystem>();
        dialogueSystem.StartConversation(Conversations[++mIndex]);
    }

}
