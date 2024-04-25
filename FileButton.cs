/*using CGP;
using UnityEngine;
using UnityEngine.UI;

public class FileButton : MonoBehaviour
{
    public string fileName;
    public SaveLoadManager saveLoadManager;
    public int spawnNum;
    public StartGame startGame;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        // Load game if save file exists, otherwise start a new game
        GameState loadedGameState = saveLoadManager.LoadGame(fileName);

        // THIS NEEDS TO CHANGE BASED ON WHICH CHECKPOINT THE FILE IS AT
        StateManager.spawn = spawnNum;

        if (loadedGameState != null)
        {
            GameStateManager.Instance.SetGameState(loadedGameState);
        }
        else
        {
            startGame.playGame();
        }
    }

    public void DeleteFile()
    {
        saveLoadManager.DeleteSaveFile(fileName);
    }
}
*/