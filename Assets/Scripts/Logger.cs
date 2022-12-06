using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour
{
    public static Logger Instance { get; private set; }

    public Text logs;
    
    // public Logger(Text text)
    // {
    //     logs = text;
    //     logs.text = "";
    // }
    
    private void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        logs = GetComponent<Text>();
        logs.text = "";
    }

    public void Log(string log)
    {
        return ;

        logs.text += $"\n{log}";
    }
}
