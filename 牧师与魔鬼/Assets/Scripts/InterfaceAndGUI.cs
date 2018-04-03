using UnityEngine;
using UnityEditor;

public interface ISceneController
{
    void LoadResources();
    void Pause();
    void Resume();
}

public interface IuserAction
{
    void Restart();
    void moveBoat();
    void moveObject(ObjectController objectController);
}

public class Director : System.Object
{
    private static Director _instance;

    public ISceneController CurrentController { get; set; }
    public bool running { get; set; }

    public static Director getInstance()
    {
        if (_instance == null)
        {
            _instance = new Director();
        }
        return _instance;
    }

    public int GetFPS()
    {
        return Application.targetFrameRate;
    }

    public void SetFPS(int val)
    {
        Application.targetFrameRate = val;
    }
}

public class UserGUI : MonoBehaviour
{
    private IuserAction action;
    public int status = 0;

    void Start()
    {
        action = Director.getInstance().CurrentController as IuserAction;
    }
    void OnGUI()
    {
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart"))
            {
                status = 0;
                action.Restart();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!");
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart"))
            {
                status = 0;
                action.Restart();
            }
        }
    }
}

public class ClickGUI : MonoBehaviour
{
    IuserAction action;
    ObjectController characterController;

    public void setController(ObjectController characterCtrl)
    {
        characterController = characterCtrl;
    }

    void Start()
    {
        action = Director.getInstance().CurrentController as IuserAction;
    }

    void OnMouseDown()
    {
        if (gameObject.name == "Boat")
        {
            action.moveBoat();
        }
        else
        {
            action.moveObject(characterController);
        }
    }
}