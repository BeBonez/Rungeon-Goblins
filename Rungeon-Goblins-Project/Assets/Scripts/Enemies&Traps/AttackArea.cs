using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    [SerializeField] float maxScale, attackCooldown, attackActiveTime;
    [SerializeField] GameObject progressCube, exclamation;
    [SerializeField] Color greenColor, redColor;
    private bool isCoolingDown = false;
    private bool isAoERunning = false;
    private float cooldownEndTime;
    private BoxCollider boxCollider;
    private Material material, cubeMaterial;

    void Start()
    {
        if (transform.position.x >= 11 || transform.position.x <= -11)
        {
            Destroy(gameObject);
            return;
        }

        material = GetComponent<Renderer>().material;

        cubeMaterial = progressCube.GetComponent<Renderer>().material;

        boxCollider = GetComponent<BoxCollider>();
        StartCoroutine(AoESize(attackCooldown));
    }

    void Update()
    {
        if (progressCube.transform.localScale.x >= maxScale && !isCoolingDown)
        {
            StartCooldown();
        }

        if (isCoolingDown && Time.time >= cooldownEndTime && !isAoERunning)
        {
            isCoolingDown = false;
            StartCoroutine(AoESize(attackCooldown));
        }
    }

    private void StartCooldown()
    {
        isCoolingDown = true;
        cooldownEndTime = Time.time + attackActiveTime;
    }

    IEnumerator AoESize(float duration)
    {
        material.color = greenColor;
        
        cubeMaterial.color = greenColor;

        isAoERunning = true;

        float time = 0;

        exclamation.SetActive(false);
        boxCollider.enabled = false;

        while (time < duration)
        {
            time += Time.deltaTime;

            progressCube.transform.localScale = new Vector3(Mathf.Lerp(0, maxScale, time / duration), progressCube.transform.localScale.y,
                Mathf.Lerp(0, maxScale, time / duration)
            );

            material.color = Color.LerpUnclamped(greenColor, redColor, time / duration);
            cubeMaterial.color = Color.LerpUnclamped(greenColor, redColor, time / duration);

            yield return null;
        }

        exclamation.SetActive(true);
        boxCollider.enabled = true;
        isAoERunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hole"))
        {
            Destroy(gameObject);
        }
    }
}