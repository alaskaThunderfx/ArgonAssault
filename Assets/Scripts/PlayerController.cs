using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls playerControls;

    [Header("Control Settings")]
    [SerializeField]
    [Tooltip("Speed that fairy moves")]
    float controlSpeed = 20f;

    [SerializeField]
    [Tooltip("Minimum horizontal distance fairy can move.")]
    float xMin = -13.5f;

    [SerializeField]
    [Tooltip("Maximum horizontal distance fairy can travel.")]
    float xMax = 13.5f;

    [SerializeField]
    [Tooltip("Minimum vertical distance fairy can travel.")]
    float yMin = -2f;

    [SerializeField]
    [Tooltip("Maximum veritcal distance fairy can travel.")]
    float yMax = 14f;

    [Header("Screen Position Based Tuning")]
    [SerializeField]
    float pitchFactor = -2f;

    [SerializeField]
    float yawFactor = 4f;

    [Header("Player Input Based Tuning")]
    [SerializeField]
    float controlPitchFactor = -20f;

    [SerializeField]
    float controlRollFactor = -20f;

    [Header("Magic Missile")]
    [SerializeField]
    GameObject[] magicMissile;

    float xThrow;
    float yThrow;

    Animator thisAnimator;
    AnimationClip idle;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void Start() { }

    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = playerControls.Fairy.Move.ReadValue<Vector2>().x;
        yThrow = playerControls.Fairy.Move.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, xMin, xMax);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchFactor;
        float pitchDueToControl = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (playerControls.Fairy.Fire.ReadValue<float>() == 1)
        {
            SetMagicMissileActive(true);
        }
        else
        {
            SetMagicMissileActive(false);
        }
    }

    void SetMagicMissileActive(bool isActive)
    {
        var emissionModule = magicMissile[1].GetComponent<ParticleSystem>().emission;
        emissionModule.enabled = isActive;
        magicMissile[0].SetActive(isActive);
    }
}
