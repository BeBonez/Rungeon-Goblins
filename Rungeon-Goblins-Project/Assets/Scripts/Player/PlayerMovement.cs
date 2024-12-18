using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerMovement : MonoBehaviour
{
    public bool coinAboveHoleAchievement;
    protected float originalSpeed;
    protected float posX, posY, posZ;
    protected string charName;
    [SerializeField] protected float moveDistance;
    [NonSerialized] public GameManager gameManager;
    [SerializeField] protected float speed;
    [SerializeField] protected int hitSound;
    protected IEnumerator dash;
    protected bool canMove = true;
    [SerializeField] protected Animator animator;
    protected Timer timer;

    [Header("GeneralPower")]
    [SerializeField] protected int killMeta;
    [SerializeField] protected int maxKills;
    [SerializeField] protected float timeActive, timeBetweenDashes;
    protected float actualTime;
    protected bool isPowerActive = false;
    protected int originalKillMeta, actualKills;
    [SerializeField] protected GameObject trail;
    [NonSerialized] public int kills;
    [NonSerialized] public bool killed;
    protected PowerBar powerBar;

    [Header("Cheats")]
    protected bool imortal;
    protected bool infinityTime;
    protected bool infinityPower;

    [Header("FrontDash")]
    [SerializeField] protected int maxCharges;
    [SerializeField] protected int actualCharges;
    protected TMP_Text uiMaxCharges;
    protected TMP_Text uiActualCharges;

    [Header("Dash")]
    public int dashedThroughWall = 0;
    protected Vector3 lastPosition;
    [SerializeField] protected Vector3 nextposition;
    [SerializeField] protected GameObject dashFx;
    protected bool hasReached;
    protected bool canceled = false;
    public abstract void Move(Vector2 direction);
    public void UpdatePosition()
    {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
    }

    protected abstract void Dash(Vector3 direction);

    protected Vector3 FixPosition(Vector3 pos)
    {
        Vector3 newPos = new Vector3((float)Math.Floor(pos.x), 0, (float)Math.Floor(pos.z - 20));

        float xValue = newPos.x;
        if (Math.Abs(xValue % 10) != 0)
        {
            xValue = (float)Math.Floor(xValue / 10) * 10;
        }

        float zValue = newPos.z;
        if (Math.Abs(zValue % 10) != 5)
        {
            zValue = (float)Math.Floor(zValue / 10) * 10 + (zValue >= 0 ? 5 : -5);
        }

        newPos = new Vector3(xValue, newPos.y, zValue);

        return newPos;
    }
    protected void MoveToDestiny()
    {
        if (Vector3.Distance(transform.position, nextposition) < 0.001f)
        {
            hasReached = true;
        }
        else
        {
            nextposition = new Vector3((float)Math.Floor(nextposition.x), (float)Math.Floor(nextposition.y), (float)Math.Floor(nextposition.z));
            transform.position = Vector3.MoveTowards(transform.position, nextposition, speed * Time.deltaTime);
            hasReached = false;
        }
    }

    public void PlayHitSFX()
    {
        AudioManager.Instance.PlaySFX(hitSound);
    }

    #region cheats
    public void ToggleImortality()
    {
        imortal = !imortal;
    }

    public void ToggleInfPower()
    {
        infinityPower = !infinityPower;

        if (infinityPower)
        {
            killMeta = 0;
            powerBar.maximum = killMeta;
        }
        else
        {
            killMeta = originalKillMeta;
            powerBar.maximum = killMeta;
        }
    }

    public void ToggleInfTime()
    {
        infinityTime = !infinityTime;
        timer.SetInfTime(infinityTime);
    }

    public bool GetImortallity()
    {
        return imortal;
    }

    public bool GetInfTime()
    {
        return infinityTime;
    }
    #endregion
    #region GetSets
    public bool IsPowerActive()
    {
        return isPowerActive;
    }

    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }

    public void SwitchCanMove(bool condition)
    {
        canMove = condition;
    }

    public Animator GetAnimator()
    {
        return animator;
    }

    public string GetName()
    {
        return charName;
    }

    public void SetNextPosition(Vector3 position)
    {
        nextposition = position;
    }

    public Vector3 GetNextPosition()
    {
        return nextposition;
    }

    public void DashOFF(bool condition)
    {
        canceled = condition;
    }

    public float GetMoveDistance()
    {
        return moveDistance;
    }

    public abstract void DeactivatePower();

    public virtual void AddCharge(int amount, string type)
    {
        if (type == "Cheat")
        {
            actualCharges = amount;
        }
        if (type == "Kill")
        {
            if (actualCharges + amount > maxCharges)
            {
                actualCharges = maxCharges;
            }
            else
            {
                actualCharges += amount;
            }
        }

        AudioManager.Instance.PlaySFX(12);
    }

    public int GetMaxCharges()
    {
        return maxCharges;
    }

    protected void UpdateUIInfo()
    {
        uiActualCharges.text = actualCharges.ToString();
        uiMaxCharges.text = maxCharges.ToString();
    }

    public void AddKill(int value)
    {
        actualKills += value;
        powerBar.SetCurrentFill(actualKills);
    }
    protected bool CanUsePower()
    {
        return actualKills >= killMeta;
    }
    protected void ResetPowerFill()
    {
        actualKills = 0;
        powerBar.SetCurrentFill(actualKills);
    }
    public int GetKillLimit()
    {
        return originalKillMeta;
    }
    public int GetActualKills()
    {
        return actualKills;
    }

    public void SetSpeed(float amount)
    {
        speed = amount;
    }

    public float GetOriginalSpeed()
    {
        return originalSpeed;
    }

    public void AddPowerKill(int amount)
    {
        kills += amount;
    }

    #endregion


}
