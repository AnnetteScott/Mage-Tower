using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerStatus : MonoBehaviour
{
    private Entity entity;

    void Start()
    {      
        entity = GetComponent<Entity>();
    }

    void Update()
    {
        entity.addExperience(10);

        if (entity.getLevel() > 1)
        {
            entity.maxHealth += entity.getLevel() / 2;
        }
    }
}
