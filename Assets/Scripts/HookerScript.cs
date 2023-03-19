using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookerScript : InterlocutorScript
{
    private static int IgnoringYou = 0;
    private static int Angry = 1;
    private static int FollowMe = 2;

    void Start()
    {
        if (GameContext.Instance.HookerFooled) gameObject.SetActive(false);    
    }

    protected override int FindNextIndex()
    {
        if (GameContext.Instance.GotWatch) return FollowMe;
        else if (GameContext.Instance.HaveTalkedToHooker) return Angry;
        else if (!GameContext.Instance.NotFinishFirstQuest) return IgnoringYou;
        else return -1;
    }

    public override void ConversationEnd()
    {
        if (GameContext.Instance.HookerFooled) gameObject.SetActive(false);
    }
}
