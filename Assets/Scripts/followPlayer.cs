using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.position = player.GetComponent<Player>().transform.transform.position + new Vector3(0, 1, -10);
            Camera.main.orthographicSize = GlobalData.cameraZoom;
        }
    }
}
