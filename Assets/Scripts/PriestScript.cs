using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PriestScript : InterlocutorScript
{
    private static int FirstTime = 0;
    private static int Again = 1;
    private static int LastTime = 2;

    private void Start()
    {
        if (GameContext.Instance.GotPriest) gameObject.SetActive(false);
    }

    protected override int FindNextIndex()
    {
        if (GameContext.Instance.GotRope) return LastTime;
        else if (!GameContext.Instance.HaveTalkedToPriest) return FirstTime;
        else return Again;
    }

    public override void ConversationEnd()
    {
        if(GameContext.Instance.GotRope)
        {
            gameObject.SetActive(false);
            GameContext.Instance.GotPriest = true;
            SceneManager.LoadScene("FinalScene");
        }
    }

}
