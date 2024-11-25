using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour
{
    public AchievementModule[] achievementModules;
    [SerializeField] Image[] stateSprites;
    [SerializeField] GameObject popUpPrefab;
    [SerializeField] Canvas canvas;
    public GameManager gameManager;

    [SerializeField] private float achievementTimer;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            DoAchievementStuff();
        }
    }

    private void DoAchievementStuff()
    {
        #region Distance Achievements

        // Is there An exit? : Finish all the areas. // reach 750m.

        if (gameManager.GetDistance() >= 750 && gameManager.playerData.achievements[3] == false)
        {
            ApplyAchievements(3);
        }

        // There is no exit : Finish all the areas twice. // reach 1500

        if (gameManager.GetDistance() >= 1500 && gameManager.playerData.achievements[5] == false)
        {
            ApplyAchievements(5);
        }

        // Knight expertise : Reach 3000m as the knight.  // that means traversing all the areas 4 times.

        if (gameManager.GetDistance() >= 3000 && gameManager.playerData.achievements[6] == false)
        {
            if (PlayerPrefs.GetInt("SelectedChar") == 0)
            {
                ApplyAchievements(6);
            }
        }

        // Mage expertise : Reach 3000m as the mage. // that means traversing all the areas 4 times.

        if (gameManager.GetDistance() >= 3000 && gameManager.playerData.achievements[7] == false)
        {
            if (PlayerPrefs.GetInt("SelectedChar") == 1)
            {
                ApplyAchievements(7);
            }
        }

        // Thanks for Playing! : Reach 7500m. // that means traversing all the areas 10 times.

        if (gameManager.GetDistance() >= 7500 && gameManager.playerData.achievements[9] == false)
        {
            ApplyAchievements(9);
        }

        #endregion

        // I will not give up! : Spend 250 or more coins on a revive.

        if (gameManager.reviveCost >= 300 && gameManager.playerData.achievements[8] == false)
        {
            ApplyAchievements(8);
        }

        // THUNDER�spoon? : use the knight�s special power.

        if (PlayerPrefs.GetInt("SelectedChar") == 0 && gameManager.playerData.achievements[0] == false)
        {
            if (gameManager.GetPlayerScript().IsPowerActive())
            {
                ApplyAchievements(0);
            }
        }

        // Side to side : move towards the wall as the mage.

        if (PlayerPrefs.GetInt("SelectedChar") == 1 && gameManager.playerData.achievements[1] == false)
        {
            if (gameManager.GetPlayerScript().dashedThroughWall > 0)
            {
                ApplyAchievements(1);
            }
        }

        // Fast I gotta go : Finish all the areas in less than 3 minutes.

        if (gameManager.playerData.achievements[4] == false)
        {
            achievementTimer = 1 * Time.deltaTime;

            if (achievementTimer <= 180 && gameManager.GetDistance() >= 750)
            {
                ApplyAchievements(4);
            }
        }

        // I thought it was impossible! : Collect an item above a hole and survive.

        if (gameManager.GetPlayerScript().coinAboveHoleAchievement && gameManager.playerData.achievements[2] == false)
        {
            ApplyAchievements(2);
        }
    }

    public void ApplyAchievements(int index)
    {
            gameManager.playerData.achievements[index] = true;
            gameManager.saveSystem.SavePlayerData(gameManager.playerData);
            ShowPopUp(index);
    }
    public void LoadAchievements()
    {
        //Debug.Log(gameManager.playerData.achievements.Length + "sou grandao");
        for (int i = 0; i < achievementModules.Length; i++)
        {
            Debug.Log(gameManager.playerData.achievements[i]);

            if (gameManager.playerData.achievements[i])
            {              
                achievementModules[i].sprite.sprite = stateSprites[1].sprite;
            } else
            {
                achievementModules[i].sprite.sprite = stateSprites[0].sprite;
            }
        }
    }

    public void ShowPopUp(int achievementNumber)
    {
        GameObject popUp = Instantiate(popUpPrefab, canvas.transform);
        AchievPopUp popUpScript = popUp.GetComponent<AchievPopUp>();
        popUpScript.achievementModule = achievementModules[achievementNumber].gameObject;
        popUpScript.checkedSprite = stateSprites[1];
    }

}
