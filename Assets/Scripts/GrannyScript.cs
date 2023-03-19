using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrannyScript : InterlocutorScript
{

    private static int SearchingCats = 0;
    private static int Blabering = 1;
    private static int FollowMe = 2;

    void Start()
    {
        if (GameContext.Instance.GrannyFooled) gameObject.SetActive(false);
    }

    protected override int FindNextIndex()
    {
        GameContext instance = GameContext.Instance;
        if (GameContext.Instance.GotCollar) return FollowMe;
        else if (GameContext.Instance.HaveTalkedToGranny) return Blabering;
        else if (!GameContext.Instance.NotFinishFirstQuest) return SearchingCats;
        else return -1;
    }

    public override void ConversationEnd()
    {
        if (GameContext.Instance.GrannyFooled) gameObject.SetActive(false);
    }
}
