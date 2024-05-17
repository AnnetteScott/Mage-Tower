using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpIndicator : MonoBehaviour
{
    public Image levelUpVFX; // Assign this in the Inspector
    public Transform character; // Assign your character's transform here
    public float displayDuration = 2.0f; // How long the image should be displayed

    private bool isDisplaying = false;

    void Start()
    {
        // Initially, the image should be hidden
        levelUpVFX.gameObject.SetActive(false);
    }

    public void OnLevelUp()
    {
        // Start the coroutine to show the level up image
        StartCoroutine(DisplayLevelUpImage());
    }

    private IEnumerator DisplayLevelUpImage()
    {
        isDisplaying = true;
        levelUpVFX.gameObject.SetActive(true);

        // Position the image above the character
        Vector3 screenPos = Camera.main.WorldToScreenPoint(character.position + Vector3.up * 2);
        levelUpVFX.transform.position = screenPos;

        yield return new WaitForSeconds(displayDuration);

        levelUpVFX.gameObject.SetActive(false);
        isDisplaying = false;
    }

    void Update()
    {
        if (isDisplaying)
        {
            // Continuously update the position of the image in case the character moves
            Vector3 screenPos = Camera.main.WorldToScreenPoint(character.position + Vector3.up * 2);
            levelUpVFX.transform.position = screenPos;
        }
    }
}
