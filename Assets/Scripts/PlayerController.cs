using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InputAction movement;

    [SerializeField]
    float controlSpeed = 10f;

    [SerializeField]
    float xMin = -5f;

    [SerializeField]
    float xMax = 5f;

    [SerializeField]
    float yMin = -5f;

    [SerializeField]
    float yMax = 5f;

    [SerializeField]
    GameObject leftUpperWing;

    [SerializeField]
    GameObject leftLowerWing;

    [SerializeField]
    GameObject rightUpperWing;

    [SerializeField]
    GameObject rightLowerWing;

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

    // Update is called once per frame
    void Update()
    {
        float xThrow = movement.ReadValue<Vector2>().x;
        float yThrow = movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, xMin, xMax);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, yMin, yMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void WingFlap() { }
}
