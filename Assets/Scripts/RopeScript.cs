using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeScript : InterlocutorScript
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameContext.Instance.GotRope) gameObject.SetActive(false);
    }

    protected override int FindNextIndex()
    {
        if (GameContext.Instance.NotFinishSecondQuest) return -1;
        else return 0;
    }

    public override void ConversationEnd() { GameContext.Instance.GotRope = true; gameObject.SetActive(false); }
}
