using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerMovement : MonoBehaviour
{
    protected float originalSpeed;
    protected float posX, posY, posZ;
    protected string charName;
    [SerializeField] protected float moveDistance;
    [SerializeField] public GameManager gameManager;
    [SerializeField] protected float speed;
    [SerializeField] protected int hitSound;
    protected IEnumerator dash;
    protected bool canMove = true;
    protected Animator animator;
    protected Timer timer;

    [Header("Power")]
    //[SerializeField] protected float powerCooldown;
    [SerializeField] protected bool isPowerActive = false;
    [SerializeField] protected int killMeta;
    protected int originalKillMeta;
    protected int actualKills;
    [SerializeField] protected int maxKills;
    protected int kills;
    [SerializeField] public bool killed;
    [SerializeField] protected PowerBar powerBar;

    [Header("FrontDash")]
    [SerializeField] protected int maxCharges;
    [SerializeField] protected int actualCharges;
    [SerializeField] protected float dashCoolDown;
    [SerializeField] protected bool isDashCoolingDown;
    [SerializeField] protected TMP_Text uiMaxCharges;
    [SerializeField] protected TMP_Text uiActualCharges;

    [Header("Dash")]
    protected Vector3 lastPosition;
    protected Vector3 nextposition;
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

    protected void MoveToDestiny()
    {
        if (Vector3.Distance(transform.position, nextposition) < 0.001f)
        {
            hasReached = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextposition, speed * Time.deltaTime);
            hasReached = false;
        }
    }

    public void PlayHitSFX()
    {
        AudioManager.Instance.PlaySFX(hitSound);
    }

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
