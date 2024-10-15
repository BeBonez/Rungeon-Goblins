using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaulScript : PlayerMovement
{
    [Header("Dash")]
    [SerializeField] int maxCharges;
    [SerializeField] int actualCharges;
    [SerializeField] float dashCoolDown;
    [SerializeField] bool isDashCoolingDown;

    [Header("Shield")]
    [SerializeField] float timeActive;
    [SerializeField] float shieldCoolDown;
    float actualShieldTimeCooldown;
    bool isShieldCoolingDown;
    bool isActive;

    [Header("UI Components")]
    [SerializeField] TMP_Text uiMaxCharges;
    [SerializeField] TMP_Text uiActualCharges;
    [SerializeField] Image uiShield;

    private void Start()
    {
        uiMaxCharges = GameObject.Find("MaxCharges").GetComponent<TMP_Text>();
        uiActualCharges = GameObject.Find("ActualCharges").GetComponent<TMP_Text>();
        uiShield = GameObject.Find("ShieldImage").GetComponent<Image>();
        UpdatePosition();
    }

    public override void Move(Vector2 direction)
    {
        
        if (direction.y >= 1)
        {
            
            if (actualCharges > 0)
            {
                Debug.Log("Cima");
                transform.position = new Vector3(posX, posY, posZ + moveDistance);
                actualCharges--;
                gameManager.AddDistance(1);
            }

        }
        else if (direction.y <= -1)
        {
            Debug.Log("baixo");
            if (isShieldCoolingDown == false && isActive == false)
            {
                StartCoroutine(RaiseShield());
            }
        }

        else if (direction.x >= 1)
        {
            Debug.Log("direita");
            if (!(transform.position.x == 10)) // Se não for ultrapassar o limite da direita, ele pode mover
            {
                transform.position = new Vector3(posX + moveDistance, posY, posZ + moveDistance);
                gameManager.AddDistance(1);
            }
            

        }

        else if (direction.x <= -1)
        {
            Debug.Log("esquerad");
            if (!(transform.position.x == -10)) // Se não for ultrapassar o limite da esquerda, ele pode mover
            {
                transform.position = new Vector3(posX - moveDistance, posY, posZ + moveDistance);
                gameManager.AddDistance(1);
            }
            
        }
        UpdatePosition();
    }

    private void FixedUpdate()
    {
        if (actualCharges < maxCharges && isDashCoolingDown == false)
        {
            StartCoroutine(Recharge());
        }
        UpdateUIInfo();
    }

    private IEnumerator Recharge()
    {
        isDashCoolingDown = true;
        yield return new WaitForSeconds(dashCoolDown);
        actualCharges++;
        isDashCoolingDown = false;
        StopCoroutine("Recharge");
    }

    private IEnumerator RaiseShield()
    {
        isActive = true;
        uiShield.color = Color.blue;
        canTakeDamge = false;
        yield return new WaitForSeconds(timeActive);
        isActive = false;
        canTakeDamge = true;
        isShieldCoolingDown = true;
        uiShield.color = Color.red;
        yield return new WaitForSeconds(shieldCoolDown);
        isShieldCoolingDown = false;
        uiShield.color = Color.white;
        StopCoroutine("RaiseShield");
    }

    private void UpdateUIInfo()
    {
        uiActualCharges.text = actualCharges.ToString();
        uiMaxCharges.text = maxCharges.ToString();
    }
}
