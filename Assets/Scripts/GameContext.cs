using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public GameObject DialogPanel;
    public InterlocutorScript Interlocutor;
    public PlayerInputScript PlayerInput;
    public GameObject ExitRoom;

    // ********************* Dialogue Variables *********************
    public bool FirstQuestAccepted = false;
    public bool NotFinishFirstQuest = true;
    public bool NotFinishSecondQuest = true;
    public bool NotFinishThirdQuest = true;
    public int NumberOfCats = 0;
    public bool HaveTalkedToDrunkGuy = false;
    public bool HaveTalkedToHooker = false;
    public bool HaveTalkedToGranny = false;
    public bool HaveTalkedToPriest = false;
    public bool GotBottle = false;
    public bool GotWatch = false;
    public bool GotCollar = false;
    public bool GotRope = false;
    public bool GotPriest = false;
    public bool GotCat0 = false, GotCat1 = false, GotCat2 = false; 
    public bool DrunkGuyFooled = false;
    public bool HookerFooled = false;
    public bool GrannyFooled = false;
    // **************************************************************

    // ********************* Singleton Paradigm *********************
    private static GameContext sInstance = null;
    // Start is called before the first frame update
    void Start()
    {
        sInstance = this;
        DontDestroyOnLoad(this);
    }

    public static GameContext Instance { get { return sInstance; } }
    // **************************************************************

    // ********************** Reflection Stuff **********************
    public void SetField(string fieldname, string value)
    {
        FieldInfo[] fields = this.GetType().GetFields();
        foreach (FieldInfo field in fields)
        {
            if (field.Name == fieldname)
                SetField(field, value);
        }
    }

    private void SetField(FieldInfo field, string value)
    {
        Type type = field.FieldType;
        if (type == typeof(bool) || type == typeof(Boolean))
            field.SetValue(this, bool.Parse(value));
        else if (type == typeof(string))
            field.SetValue(this, value);
        else if (type == typeof(float))
            field.SetValue(this, float.Parse(value));
        else if (type == typeof(int))
            field.SetValue(this, int.Parse(value));
        else
            Debug.Log("Not supported value");
    }
    // **************************************************************


}
