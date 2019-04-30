using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour
{
    //CONSTANTS
    const string SAVES_ROUTE = "/Saves";
    const string PLAYER_DATA_ROUTE = "/Saves/playerdata.dat";
    const int PLAYER_DATA = 0;


    public void LoadPlayerData()
    {
        string file = Application.persistentDataPath + PLAYER_DATA_ROUTE;

        bool fileExist = CheckIfFileExist(file);

        if (fileExist == true)
        {
            GameManager.GameDebugger.DebugLogWithMessage("[DataManager] LOADING PLayerData");
            LoadFile(file, PLAYER_DATA);
        }
        else
        {

            GameManager.GameDebugger.DebugLogWithMessage("[DataManager] no PLayerData existing");
            string saveRoute = Application.persistentDataPath + SAVES_ROUTE;
            Player newPlayer = new Player();
            newPlayer.Reset();
            SetPlayersSavedData(newPlayer);

            DirectoryInfo savesDir = new DirectoryInfo(saveRoute);

            if (savesDir.Exists == false)
            {
               
                savesDir.Create();

            }
        }

    }

    public void SavePlayerData()
    {
        GameManager.GameDebugger.DebugLogWithMessage("[DataManager] Saving PLayerData");
        string file = Application.persistentDataPath + PLAYER_DATA_ROUTE;
        SaveFile(file, PLAYER_DATA);

    }

    public void DeletePlayerData()
    {
        GameManager.GameDebugger.DebugLogWithMessage("[DataManager] Delete PLayerData");
        string file = Application.persistentDataPath + PLAYER_DATA_ROUTE;
        DeleteFile(file);
    }

    bool CheckIfFileExist(string fileToCheck)
    {

        bool fileExist = File.Exists(fileToCheck);
        return fileExist;
    }

    #region file handling
    void SaveFile(string fileToSave, int fileType)
    {
        GameManager.GameDebugger.DebugLogWithMessage("[DataManager] creating PLayerData file");
        FileStream file = File.Open(fileToSave, FileMode.Create);
        SerializeFile(file, fileType);
        file.Close();
        GameManager.GameDebugger.DebugLogWithMessage("[DataManager] saved PLayerData");


    }

    void LoadFile(string fileToOpen, int fileType)
    {

        GameManager.GameDebugger.DebugLogWithMessage("[DataManager] Open PLayerData file");
        FileStream savedFile = File.Open(fileToOpen, FileMode.Open);
        DeserializeFile(savedFile, fileType);
        savedFile.Close();

        GameManager.GameDebugger.DebugLogWithMessage("[DataManager] finish loading PLayerData");
    }

    void DeleteFile(string fileToDelete)
    {
        File.Delete(fileToDelete);
    }

    #endregion

    #region serialize and deserialize handling

    void SerializeFile(FileStream fileToSave, int fileType)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        switch (fileType)
        {

            case PLAYER_DATA:
                GameManager.GameDebugger.DebugLogWithMessage("[DataManager] serializing PLayerData");
                formatter.Serialize(fileToSave, GameManager.GamePlayer.GetStats());
                break;

            default:

                break;


        }
    }

    void DeserializeFile(FileStream savedFile, int fileType)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        switch (fileType)
        {

            case PLAYER_DATA:
                GameManager.GameDebugger.DebugLogWithMessage("[DataManager] Deserializing PLayerData");
                Player newPlayerData = (Player)formatter.Deserialize(savedFile);
                SetPlayersSavedData(newPlayerData);

                break;

            default:

                break;


        }

    }
    #endregion

    void SetPlayersSavedData(Player playerData)
    {
        GameManager.GamePlayer.SetStats(playerData);
    }
}
