using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class PowerBar : MonoBehaviour
{
    [SerializeField] int maximum;
    [SerializeField] int current;
    [SerializeField] Image mask;
    [SerializeField] Image[] masks;


    void Start()
    {
        int charIndex = PlayerPrefs.GetInt("SelectedChar", 0);
        mask = masks[charIndex];

    }
    private void Update()
    {
        GetCurrentFill();
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
