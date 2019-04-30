using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(DataManager))]
[RequireComponent(typeof(SoundManager))]
[RequireComponent(typeof(SceneLoaderManager))]
[RequireComponent(typeof(DebugLoggerUtil))]

public class GameManager : MonoBehaviour
{
    [Header("For Testing and QA")]
    public bool DebuggingOn = false;
    public bool qaOn = false;



    private static GameManager _instance;
    public static GameManager GameMaster
    {
        get { return _instance; }
    }

    private static PlayerManager _playerManager;
    public static PlayerManager GamePlayer
    {
        get { return _playerManager; }
    }

    private static DataManager _dataManager;
    public static DataManager GameData
    {
        get { return _dataManager; }
    }

    private static SoundManager _soundManager;
    public static SoundManager GameSound
    {
        get { return _soundManager; }
    }

    private static SceneLoaderManager _sceneLoaderManager;
    public static SceneLoaderManager GameSceneLoader
    {
        get { return _sceneLoaderManager; }
    }

    private static DebugLoggerUtil _debuggerUtility;
    public static DebugLoggerUtil GameDebugger
    {
        get { return _debuggerUtility; }
    }







    void Awake()
    {

        _debuggerUtility = GetComponent<DebugLoggerUtil>();
        _playerManager = GetComponent<PlayerManager>();
        _dataManager = GetComponent<DataManager>();
        _soundManager = GetComponent<SoundManager>();
        _sceneLoaderManager = GetComponent<SceneLoaderManager>();




        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

}
