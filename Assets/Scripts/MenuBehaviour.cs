using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public void StartGame() { SceneManager.LoadScene("SatanRoom"); }

    public void ExitGame() { Application.Quit(0); }
}
