using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            playerTransform = players[0].GetComponent<Player>().transform; ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.transform.position + new Vector3(0, 1, -10);
        Camera.main.orthographicSize = GlobalData.cameraZoom;
    }
}
