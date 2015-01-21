using UnityEngine;
using System.Collections;
using Script.Config;
using Script.Config.ConfigObj;
using System.IO;

public class TestConfig : MonoBehaviour {
    //配置
    public ConfigManager config;
	// Use this for initialization
	void Start () {
        config = new ConfigManager();
        //加载配置
        if (Application.isEditor)
        {
            config.Parse(File.ReadAllBytes(localEditor + "/config.W"));
        }
        else
        {
            config.Parse(File.ReadAllBytes(localConfig + "/config.W"));
        }
        ItemConfig item = config.GetItem(100201);

        Debug.Log(item.Name + ":" + item.Descript);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static string localEditor = Application.dataPath + "/StreamingAssets";
    public static string localConfig =
#if UNITY_ANDROID  
    Application.dataPath + "!/assets";
#elif UNITY_IPHONE 
    Application.dataPath + "/Raw";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
    Application.dataPath + "/StreamingAssets";
#else
    string.Empty;
#endif
}
