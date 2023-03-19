using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterlocutorScript : MonoBehaviour
{
    public string[] Conversations;
    public GameObject Character;

    public void StartConversation()
    {
        Character.SetActive(true);
        GameContext.Instance.Interlocutor = this;
        DialogueSystem dialogueSystem = GameContext.Instance.DialogPanel.GetComponent<DialogueSystem>();
        dialogueSystem.OtherBubble = Character.transform.GetChild(0).gameObject;
        dialogueSystem.Character = Character;
        
        int index = FindNextIndex();
        if(index != -1)
            dialogueSystem.StartConversation(Conversations[index]);
    }

    public virtual void ConversationEnd() { }

    protected abstract int FindNextIndex();

}
