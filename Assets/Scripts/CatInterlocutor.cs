using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatInterlocutor : InterlocutorScript
{
    private static int sNextIndex = 0;
    private int mIndex;
    
    private void Start()
    {
        mIndex = sNextIndex++;
        if (sNextIndex == 3)
            sNextIndex = 0;

        if (mIndex == 0 && GameContext.Instance.GotCat1) gameObject.SetActive(false);
        else if (mIndex == 1 && GameContext.Instance.GotCat2) gameObject.SetActive(false);
        else if (mIndex == 2 && GameContext.Instance.GotCat0) gameObject.SetActive(false);
    }

    protected override int FindNextIndex() { return 0; }

    public override void ConversationEnd()
    {
        GameContext.Instance.NumberOfCats++;
        Debug.Log(mIndex);
        if (mIndex == 0) GameContext.Instance.GotCat1 = true;
        else if (mIndex == 1) GameContext.Instance.GotCat2 = true;
        else if (mIndex == 2) GameContext.Instance.GotCat0 = true;
        gameObject.SetActive(false);
    }

}
