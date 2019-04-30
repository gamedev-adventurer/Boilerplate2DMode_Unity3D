using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionTypes
{
    Move,
    Jump
}

public enum Directions
{
    Up,
    Down,
    Left,
    Right,
    None
}

public class PlayerManager : MonoBehaviour
{
    Player _stats = new Player();


    public void SetStats(Player newStats)
    {
        _stats = newStats;
    }

    public Player GetStats()
    {
        return _stats;
    }

    public void DoAction(ActionTypes action, Directions direction)
    {
        GameManager.GameDebugger.DebugLogWithMessage("[PlayerManager] Do: " + action.ToString() + " Diretcion: " + direction.ToString());
    }




    #region DEBUG OnGUI
    void OnGUI()
    {
        if (GameManager.GameMaster.qaOn)
        {
            if (GUI.Button(new Rect(10, 10, 150, 40), "Load Data"))
            {
                //print("Load Data");
                GameManager.GameData.LoadPlayerData();

            }
            if (GUI.Button(new Rect(10, 60, 250, 40), "Add and Save"))
            {
                //print("Update Data");
                _stats.HighScore += 20;
                _stats.Health += 10;
                _stats.Score += 50;

                GameManager.GameData.SavePlayerData();

            }

            if (GUI.Button(new Rect(300, 60, 250, 40), "Substract high and Save"))
            {
                //print("Update Data");
                _stats.HighScore -= 20;
                _stats.Health -= 10;
                _stats.Score -= 50;
                GameManager.GameData.SavePlayerData();

            }

            if (GUI.Button(new Rect(10, 120, 150, 40), "Delete File"))
            {
                //print("Delete Data");
                GameManager.GameData.DeletePlayerData();

            }        



            if (_stats != null)
            {
                GUI.Label(new Rect(10, 220, 100, 20), "Health: " + _stats.Health);
                GUI.Label(new Rect(10, 240, 100, 20), "Score: " + _stats.Score);
                GUI.Label(new Rect(10, 260, 100, 20), "Hight Score: " + _stats.HighScore);

            }

        }

    }
    #endregion
}
