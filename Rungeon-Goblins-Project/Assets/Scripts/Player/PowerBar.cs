using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] int maximum;
    [SerializeField] int current;
    [SerializeField] Image mask;
    [SerializeField] GameObject outline;
    [SerializeField] GameObject[] outlines;
    [SerializeField] Image[] masks;

    void Start()
    {
        int charIndex = PlayerPrefs.GetInt("SelectedChar", 0);
        outline = outlines[charIndex];
        mask = masks[charIndex];

    }
    private void Update()
    {
        GetCurrentFill();

        if (current >= maximum) {
            outline.SetActive(true);
        }
        else {
            outline.SetActive(false);
        }
    }

    private void GetCurrentFill()
    {
        float fill = (float)current / (float)maximum;
        mask.fillAmount = fill;
    }

    public void SetCurrentFill(int value)
    {
        current = value;
    }

    public void SetMaximumFill(int value)
    {
        maximum = value;
    }
}
