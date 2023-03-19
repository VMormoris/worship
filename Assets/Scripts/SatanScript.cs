using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanScript : InterlocutorScript
{
    private static int GiveFirstQuest = 0;
    private static int WaitingFirstQuest = 1;
    private static int GotCats = 2;
    private static int WaitingSecondQuest = 3;
    private static int WaitingThirdQuest = 4;
    private static int FooledGranny = 5;
    private static int FooledHooker = 6;
    private static int FooledDrunkDude = 7;
    private static int FinishDialogue = 8;

    // Start is called before the first frame update
    protected override int FindNextIndex()
    {
        if (GameContext.Instance.GotPriest) return FinishDialogue;
        else if (!GameContext.Instance.NotFinishSecondQuest) return WaitingThirdQuest;
        else if (GameContext.Instance.HookerFooled) return FooledHooker;
        else if (GameContext.Instance.DrunkGuyFooled) return FooledDrunkDude;
        else if (GameContext.Instance.GrannyFooled) return FooledGranny;
        else if (!GameContext.Instance.NotFinishFirstQuest) return WaitingSecondQuest;
        else if (GameContext.Instance.NumberOfCats == 3) return GotCats;
        else if (!GameContext.Instance.FirstQuestAccepted) return GiveFirstQuest;
        else return WaitingFirstQuest;
        //if (!GameContext.Instance.FirstQuestAccepted) return GiveFirstQuest;
        //else if (GameContext.Instance.NumberOfCats == 3) return GotCats;
        //else if (GameContext.Instance.NotFinishFirstQuest) return WaitingFirstQuest;
        //else if (GameContext.Instance.GrannyFooled || GameContext.Instance.DrunkGuyFooled || GameContext.Instance.HookerFooled) return GotCats;
        //else if (GameContext.Instance.NotFinishSecondQuest) return WaitingSecondQuest;
        //else if (GameContext.Instance.NotFinishThirdQuest) return WaitingThirdQuest;
        //else return -1;
    }

    public override void ConversationEnd() { if (GameContext.Instance.FirstQuestAccepted) GameContext.Instance.ExitRoom.SetActive(true); }
}
