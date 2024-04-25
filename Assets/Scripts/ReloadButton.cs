using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButton : MonoBehaviour
{
    bool isReloading;

    public void Reload()
    {
        if (isReloading)
            return;

        isReloading = true;
        SceneManager.LoadScene(0);
    }
}
