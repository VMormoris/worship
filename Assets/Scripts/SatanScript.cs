using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanScript : InterlocutorScript
{
    private static int GiveFirstQuest = 0;
    private static int WaitingFirstQuest = 1;
    private static int GotCats = 2;

    // Start is called before the first frame update
    protected override int FindNextIndex()
    {
        if (!GameContext.Instance.FirstQuestAccepted) return GiveFirstQuest;
        else if (GameContext.Instance.NumberOfCats == 3) return GotCats;
        else if (GameContext.Instance.NotFinishFirstQuest) return WaitingFirstQuest;
        else return -1;
    }

    public override void ConversationEnd() { if (GameContext.Instance.FirstQuestAccepted) GameContext.Instance.ExitRoom.SetActive(true); }
}
