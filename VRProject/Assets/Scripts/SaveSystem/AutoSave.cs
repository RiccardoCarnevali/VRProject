using System.Collections;
using UnityEngine;

public class AutoSave : MonoBehaviour
{

    private float autoSaveDelaySeconds = 5f;
    private float autoSaveProgress = 0;
    
    private void Update() {
        if (Settings.paused)
            return;

        autoSaveProgress += Time.deltaTime * Time.timeScale;

        if (autoSaveProgress >= autoSaveDelaySeconds)  {
            SaveSystem.Save();
            autoSaveProgress = 0;
        }
    }
}
