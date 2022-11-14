using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;

    [SerializeField]  float xMin = -13.5f;
    [SerializeField] float xMax = 13.5f;
    [SerializeField] float yMin = -2f;
    [SerializeField] float yMax = 12f;

    [SerializeField]
    float pitchFactor = -2f;

    [SerializeField]
    float controlPitchFactor = -20f;

    [SerializeField]
    float yawFactor = 4f;

    [SerializeField]
    float controlRollFactor = -20f;

    float xThrow;
    float yThrow;

    Animator thisAnimator;
    AnimationClip idle;

    void Start()
    {
        thisAnimator = GetComponent<Animator>();
        // idle = thisAnimator.GetBehaviour
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        yThrow = movement.ReadValue<Vector2>().y;

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

    void WingFlap() { }
}
