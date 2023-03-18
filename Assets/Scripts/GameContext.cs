using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public GameObject DialogPanel;
    public PlayerInputScript PlayerInput;


    // ********************* Dialogue Variables *********************
    public bool FirstQuestAccepted = false;
    public bool NotFinishFirsQuest = true;
    // **************************************************************

    // ********************* Singleton Paradigm *********************
    private static GameContext sInstance = null;
    // Start is called before the first frame update
    void Start() { sInstance = this; }
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
