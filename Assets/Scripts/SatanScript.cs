using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanScript : InterlocutorScript
{
    private static int GiveFirstQuest = 0;
    private static int WaitingFirstQuest = 1;

    // Start is called before the first frame update
    protected override int FindNextIndex()
    {
        if (!GameContext.Instance.FirstQuestAccepted) return GiveFirstQuest;
        else if (!GameContext.Instance.NotFinishFirsQuest) return WaitingFirstQuest;
        else return -1;
    }
}
