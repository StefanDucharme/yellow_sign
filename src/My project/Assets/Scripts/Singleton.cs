// Singleton
// Survives across scenes
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static volatile T _instance = null;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        GameObject go = new GameObject();
                        _instance = go.AddComponent<T>();
                        go.name = typeof(T).ToString() + " singleton";
                        DontDestroyOnLoad(go);
                    }
                }
            }
            return _instance;
        }
    }
}
