using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchScript : InterlocutorScript
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameContext.Instance.GotWatch) gameObject.SetActive(false);
    }

    protected override int FindNextIndex()
    {
        if (GameContext.Instance.NotFinishFirstQuest) return -1;
        else return 0;
    }

    public override void ConversationEnd() { GameContext.Instance.GotWatch = true; gameObject.SetActive(false); }
}
