using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    private Image content;

    [SerializeField]
    private float lerpSpeed = 0.5f;

    private float currentFill;

    public float MaxValue { get; set; }

    private float currentValue;

    private float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MaxValue)
            {
                currentValue = MaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;
            }
            else
            {
                currentValue = value;
            }

            currentFill = currentFill / MaxValue;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        content = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFill != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }
    }

    public void Initialized(float currentValue, float maxValue)
    {
        MaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
