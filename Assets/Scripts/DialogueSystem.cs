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

    // Start is called before the first frame update
    void Start()
    {
        StartConversation("Test");
    }

    public void StartConversation(string filename)
    {
        mCurrentDialogue = Parser.ReadDialogue(filename);
        mIndex = 0 ;
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
            int i = 0;
            foreach (Transform child in Choices.transform)
                child.GetComponentInChildren<TextMeshProUGUI>().text = choices[i++];
        }
    }

    public void NextLine()
    {
        if(++mIndex < mCurrentDialogue.Length)
            Show(mCurrentDialogue[mIndex]);
    }

}
