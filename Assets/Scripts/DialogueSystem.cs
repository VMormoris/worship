using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    //public float Duration = 2.0f;
    public GameObject MainBubble;
    public GameObject OtherBubble;
    public GameObject Choices;

    private Line[] mCurrentDialogue;
    private int mIndex = 0;
    private int mChoice = -1;
    private List<string> mCommands = new List<string>();

    //private void Start()
    //{
    //    StartConversation("Test");
    //}

    public void StartConversation(string filename)
    {
        gameObject.SetActive(true);
        GameContext.Instance.PlayerInput.enabled = false;

        mCurrentDialogue = Utils.ReadDialogue(filename);
        mIndex = 0 ;
        mChoice = -1;
        Show(mCurrentDialogue[0]);
    }
    
    private void Show(Line line)
    {
        if (line.Type == LineType.Main)
        {
            OtherBubble.SetActive(false);
            Choices.SetActive(false);
            MainBubble.SetActive(true);
            MainBubble.GetComponentInChildren<TextMeshProUGUI>().text = line.Speech;
        }
        else if(line.Type == LineType.Answer)
        {
            OtherBubble.SetActive(false);
            Choices.SetActive(false);
            MainBubble.SetActive(true);
            string[] answers = line.Speech.Split(';');
            string choice = answers[mChoice];
            bool end = false;
            if(choice.ToLower().Contains("[end]"))
            {
                choice = choice.Substring(0, choice.IndexOf('['));
                end = true;
            }
            MainBubble.GetComponentInChildren<TextMeshProUGUI>().text = choice;
            mChoice = -1;
            if (end)
                mIndex = mCurrentDialogue.Length;
        }
        else if (line.Type == LineType.Other)
        {
            MainBubble.SetActive(false);
            MainBubble.SetActive(false);
            OtherBubble.SetActive(true);
            OtherBubble.GetComponentInChildren<TextMeshProUGUI>().text = line.Speech;
        }
        else if (line.Type == LineType.Choice)
        {
            MainBubble.SetActive(false);
            OtherBubble.SetActive(false);
            Choices.SetActive(true);
            string[] choices = line.Speech.Split(';');
            
            foreach (Transform child in Choices.transform)//Deactivate all choices
                child.gameObject.SetActive(false);

            mCommands.Clear();
            for(int i = 0; i < choices.Length; i++)//Activate only available choices
            {
                string choice = choices[i];
                if(choice.Contains("[["))
                {
                    int start = choice.IndexOf('[');
                    string command = choice.Substring(start + 2, choice.IndexOf(']') - start - 2);
                    choice = choice.Substring(0, start);
                    mCommands.Add(Utils.Trim(command));
                }
                Transform child = Choices.transform.GetChild(i);
                child.gameObject.SetActive(true);
                child.GetComponentInChildren<TextMeshProUGUI>().text = choice;
            }
        }
    }

    public void NextLine()
    {
        if (++mIndex < mCurrentDialogue.Length)
            Show(mCurrentDialogue[mIndex]);
        else
        {
            gameObject.SetActive(false);
            GameContext.Instance.PlayerInput.enabled = true;
        }
    }

    public void SelectChoice(int choice)
    {
        mChoice = choice;
        if (++mIndex < mCurrentDialogue.Length)
            Show(mCurrentDialogue[mIndex]);
        else
        {
            gameObject.SetActive(false);
            GameContext.Instance.PlayerInput.enabled = true;
        }

        foreach(string cmd in mCommands)
            Debug.Log(cmd);

        if(choice < mCommands.Count)
        {
            string cmd = mCommands[choice];
            GameContext.Instance.SetField("FirstQuestAccepted", "true");
        }
    }

}
