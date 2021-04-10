using UnityEngine;

public class FPS : MonoBehaviour
{
    public float updateInterval = 1f;

    private float lastInterval;
    private float frames = 0;
    private float fps;

    private Color color;

    private static GUIStyle normalLableStyle = null;
    public static GUIStyle NormalLableStyle
    {
        get
        {
            if (normalLableStyle == null)
            {
                normalLableStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 32,
                    fontStyle = FontStyle.Bold
                };
            }
            return normalLableStyle;
        }
    }

    private void OnGUI()
    {
        GUI.color = color;
        GUI.Label(new Rect(5, 5, 300, 40), string.Format("FPS: {0:0}", fps), NormalLableStyle);


    }


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

    }

    private void Update()
    {
        ++frames;
        var timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval - updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }
        if (fps < 25)
        {
            color = Color.yellow;
        }
        else if (fps < 15)
        {
            color = Color.red;
        }
        else
        {
            color = Color.green;
        }
    }
}
