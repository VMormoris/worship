using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkDudeScript : InterlocutorScript
{
    private static int Sleeping = 0;
    private static int InnerThought = 1;
    private static int FollowMe = 2;

    void Start()
    {
        if (GameContext.Instance.DrunkGuyFooled) gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    protected override int FindNextIndex()
    {
        if (GameContext.Instance.GotBottle) return FollowMe;
        else if (GameContext.Instance.HaveTalkedToDrunkGuy) return InnerThought;
        else if (!GameContext.Instance.NotFinishFirstQuest) return Sleeping;
        else return -1;
    }

    public override void ConversationEnd()
    {
        if(GameContext.Instance.DrunkGuyFooled) gameObject.SetActive(false);
    }
}
