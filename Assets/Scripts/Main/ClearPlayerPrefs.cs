
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour
{
    void Start()
    {
        ClearCache();
    }

    private void ClearCache()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
