using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerMovement : MonoBehaviour
{
    protected float posX, posY, posZ;
    protected string charName;
    [SerializeField] protected float moveDistance;
    [SerializeField] public GameManager gameManager;
    [SerializeField] protected bool canTakeDamge = true;
    [SerializeField] protected bool isFliyng = false;
    [SerializeField] protected float speed;
    [SerializeField] protected int hitSound;
    protected IEnumerator dash;
    protected bool canMove = true;
    protected Animator animator;

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
    //{
    //    //if (Time.timeScale > 0)
    //    //{
    //    //    if (direction.y >= 1)
    //    //    {
    //    //        //Debug.Log("UP");
    //    //        transform.position = new Vector3(posX, posY, posZ + moveDistance);
    //    //        canMoveBackwards = true; // PH
    //    //        gameManager.AddDistance(1);
    //    //    }
    //    //    else if (direction.y <= -1)
    //    //    {
    //    //        if (gameManager.GetDistance() != 0 && canMoveBackwards)
    //    //        {
    //    //            transform.position = new Vector3(posX, posY, posZ - moveDistance);
    //    //            canMoveBackwards = false;
    //    //            gameManager.AddDistance(-1);
    //    //        }
    //    //        //Debug.Log("DOWN");
    //    //    }
    //    //    else if (direction.x >= 1)
    //    //    {
    //    //        //Debug.Log("RIGHT");
    //    //        if (transform.position.x >= moveDistance)
    //    //        {
    //    //            transform.position = new Vector3(posX - moveDistance * 2, posY, posZ + moveDistance);
    //    //        }
    //    //        else
    //    //        {
    //    //            transform.position = new Vector3(posX + moveDistance, posY, posZ + moveDistance);
    //    //        }
    //    //        canMoveBackwards = true; // PH
    //    //        gameManager.AddDistance(1);

    //    //    }
    //    //    else if (direction.x <= -1)
    //    //    {
    //    //        //Debug.Log("LEFT");
    //    //        if (transform.position.x <= -moveDistance)
    //    //        {
    //    //            transform.position = new Vector3(posX + moveDistance * 2, posY, posZ + moveDistance);
    //    //        }
    //    //        else
    //    //        {
    //    //            transform.position = new Vector3(posX - moveDistance, posY, posZ + moveDistance);
    //    //        }
    //    //        canMoveBackwards = true; // PH
    //    //        gameManager.AddDistance(1);
    //    //    }
    //    //    UpdatePosition();
    //    }
    //}

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
    public bool CanTakeDamage()
    {
        return canTakeDamge;
    }

    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }

    public void SwitchCanMove(bool condition)
    {
        canMove = condition;
    }

    public bool IsFliyng()
    {
        return isFliyng;
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
            if (actualCharges < maxCharges)
            {
                actualCharges += amount;
            }
        }

        AudioManager.Instance.PlaySFX(12);
    }

    protected void UpdateUIInfo()
    {
        uiActualCharges.text = actualCharges.ToString();
        uiMaxCharges.text = maxCharges.ToString();
    }

    #endregion
}
