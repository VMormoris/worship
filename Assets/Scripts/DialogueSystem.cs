using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    //public float Duration = 2.0f;
    public GameObject Character;
    public GameObject MainBubble;
    public GameObject InnerBubble;
    public GameObject OtherBubble;
    public GameObject Choices;

    public AudioClip[] VoiceLines;

    private Line[] mCurrentDialogue;
    private int mIndex = 0;
    private int mChoice = -1;
    private List<string> mCommands = new List<string>();
    private bool mArrowState;

    void Start()
    {
        GameContext.Instance.DialogPanel = gameObject;
        gameObject.SetActive(false);
    }

    public void StartConversation(string filename)
    {
        gameObject.SetActive(true);
        GameContext.Instance.PlayerInput.enabled = false;

        mArrowState = GameContext.Instance.ExitRoom.activeSelf;
        GameContext.Instance.ExitRoom.SetActive(false);

        mCurrentDialogue = Utils.ReadDialogue(filename);
        mIndex = 0 ;
        mChoice = -1;
        Show(mCurrentDialogue[0]);
    }
    
    private void Show(Line line)
    {
        switch (line.Type)
        {
            case LineType.Main:
            case LineType.Inner:
                OtherBubble.SetActive(false);
                Choices.SetActive(false);
                GameObject bubble;
                GameObject obubble;
                if (line.Type == LineType.Main)
                {
                    bubble = MainBubble;
                    obubble = InnerBubble;
                }
                else
                {
                    bubble = InnerBubble;
                    obubble = MainBubble;
                }
                bubble.SetActive(true);
                obubble.SetActive(false);
                string speech = line.Speech;
                if (speech.Contains("[["))
                {
                    int start = speech.IndexOf('[');
                    string command = speech.Substring(start + 2, speech.IndexOf(']') - start - 2);
                    speech = speech.Substring(0, start);
                    command = Utils.Trim(command);
                    string[] parsed = command.Split('=');
                    GameContext.Instance.SetField(parsed[0], parsed[1]);
                }
                if(speech.Contains('\\'))
                {
                    int index = speech.IndexOf('\\');
                    int voiceline = int.Parse(speech.Substring(index + 1));
                    speech = speech.Substring(0, index);
                    AudioSource source = GetComponent<AudioSource>();
                    source.clip = VoiceLines[voiceline];
                    source.Play();
                }
                bubble.GetComponentInChildren<TextMeshProUGUI>().text = speech;
                break;
            case LineType.Answer:
                {
                    OtherBubble.SetActive(false);
                    Choices.SetActive(false);
                    InnerBubble.SetActive(false);
                    MainBubble.SetActive(true);
                    string[] answers = line.Speech.Split(';');
                    string choice = answers[mChoice];
                    bool end = false;
                    if (choice.ToLower().Contains("[end]"))
                    {
                        choice = choice.Substring(0, choice.IndexOf('['));
                        end = true;
                    }
                    if (choice.Contains('\\'))
                    {
                        int index = choice.IndexOf('\\');
                        int voiceline = int.Parse(choice.Substring(index + 1));
                        choice = choice.Substring(0, index);
                        AudioSource source = GetComponent<AudioSource>();
                        source.clip = VoiceLines[voiceline];
                        source.Play();
                    }
                    MainBubble.GetComponentInChildren<TextMeshProUGUI>().text = choice;
                    mChoice = -1;
                    if (end)
                        mIndex = mCurrentDialogue.Length;
                    break;
                }
            case LineType.Other:
                {
                    MainBubble.SetActive(false);
                    Choices.SetActive(false);
                    InnerBubble.SetActive(false);
                    OtherBubble.SetActive(true);
                    string t = line.Speech;
                    if (t.Contains('\\'))
                    {
                        int index = t.IndexOf('\\');
                        int voiceline = int.Parse(t.Substring(index + 1));
                        t = t.Substring(0, index);
                        AudioSource source = GetComponent<AudioSource>();
                        source.clip = VoiceLines[voiceline];
                        source.Play();
                    }
                    OtherBubble.GetComponentInChildren<TextMeshProUGUI>().text = t;
                    break;
                }
            case LineType.Choice:
                {
                    MainBubble.SetActive(false);
                    InnerBubble.SetActive(false);
                    OtherBubble.SetActive(false);
                    Choices.SetActive(true);
                    string[] choices = line.Speech.Split(';');

                    foreach (Transform child in Choices.transform)//Deactivate all choices
                        child.gameObject.SetActive(false);

                    mCommands.Clear();
                    for (int i = 0; i < choices.Length; i++)//Activate only available choices
                    {
                        string choice = choices[i];
                        if (choice.Contains("[["))
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
                    break;
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
            Character.SetActive(false);
            GameContext.Instance.ExitRoom.SetActive(mArrowState);
            GameContext.Instance.Interlocutor.ConversationEnd();
            GameContext.Instance.PlayerInput.enabled = true;
        }
    }

    public void SelectChoice(int choice)
    {
        mChoice = choice;
        
        if (choice < mCommands.Count)
        {
            string cmd = mCommands[choice];
            string[] parsed = cmd.Split('=');
            GameContext.Instance.SetField(parsed[0], parsed[1]);
        }

        if (++mIndex < mCurrentDialogue.Length)
            Show(mCurrentDialogue[mIndex]);
        else
        {
            gameObject.SetActive(false);
            Character.SetActive(false);
            GameContext.Instance.ExitRoom.SetActive(mArrowState);
            GameContext.Instance.Interlocutor.ConversationEnd();
            GameContext.Instance.PlayerInput.enabled = true;
        }
    }

}
