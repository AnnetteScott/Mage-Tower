using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public void setCameraZoom(float zoom)
    {
        GlobalData.cameraZoom = zoom;
    }
}
