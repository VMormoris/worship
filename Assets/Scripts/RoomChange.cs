using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomChange : MonoBehaviour
{
    private void Start()
    {
        GameContext.Instance.ExitRoom = gameObject;
        if (!GameContext.Instance.FirstQuestAccepted)
            gameObject.SetActive(false);
    }

    public void ExitRoom()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (!GameContext.Instance.NotFinishSecondQuest) SceneManager.LoadScene("PriestHouse");
        else if (GameContext.Instance.NotFinishFirstQuest) SceneManager.LoadScene("CatsSquare");
        else SceneManager.LoadScene("TownSquare");
    }

    public void GoToSatan() { SceneManager.LoadScene("SatanRoom"); }
}
