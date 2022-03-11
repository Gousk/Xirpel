using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [Header("General setup settings")]
    [Tooltip("How fast ship moved up and down upon player input")] [SerializeField] float controlSpeed = 35f;
    [Tooltip("How far player moves horizontally")] [SerializeField] float xRange = 12f;
    [Tooltip("How far player moves vertically")] [SerializeField] float yRange = 5f;

    [Header("Laser gun array")]
    [Tooltip("Add player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;
    
    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow;
    float yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation(); 
        ProcessFiring();       
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical"); 

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
          
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;  
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.y * positionYawFactor; 
        float yaw = yawDueToPosition;

        float rollDueToControlThrow = xThrow * controlRollFactor;
        float roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);  
    }
    
    void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
        {
            SetLasers(true);     
        }
        else 
        {
            SetLasers(false);
        }
    }

    void SetLasers(bool isFiring)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isFiring;    
        }
    }
}
