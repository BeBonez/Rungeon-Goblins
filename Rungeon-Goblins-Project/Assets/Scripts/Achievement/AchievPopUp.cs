using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AchievPopUp : MonoBehaviour
{
    [NonSerialized] public GameObject achievementModule;
    [NonSerialized] public Image checkedSprite;
    public Transform spawnPosition;
    public RectTransform moduleSpawnPosition;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;

        GameObject module = Instantiate(achievementModule, this.transform);

        module.GetComponent<RectTransform>().pivot = moduleSpawnPosition.pivot;
        module.GetComponent<RectTransform>().anchoredPosition = moduleSpawnPosition.anchoredPosition;
        module.GetComponent<RectTransform>().position = moduleSpawnPosition.position;

        module.GetComponent<AchievementModule>().sprite.sprite = checkedSprite.sprite;

        animator.enabled = true;

        AudioManager.Instance.PlaySFX(15);

        Destroy(gameObject, 4);
    }

}
