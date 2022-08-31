using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    public float ShutterSpeed = 50f;
    public float Aperture = 5.6f;
    public int ISOIndex = 0;

    public InputActionReference isoAdjust = null;

    private bool isAdjustingIso;

    public GameObject RightController;
    public GameObject LeftController;

    public int[] isoValues;

    private Vector3 adjustmentOriginPosition;
    private Quaternion adjustmentOriginRotation;

    public float pitchMax = 45;
    public int adjustmentSteps = 7;
    public float liftMax = 0.04f;

    private float nextAdjustmentTime;
    private float adjustmentTimer;
    private float adjustmentRate = 0.7f;

    private void Awake()
    {
        isoAdjust.action.started += AdjustingIso_started;
        isoAdjust.action.canceled += AdjustingIso_ended;
    }

    private void AdjustingIso_ended(InputAction.CallbackContext obj)
    {
        isAdjustingIso = false;


        //adjustingIsoOrigin = LeftController.transform;

        //adjustingIsoOrigin.position = LeftController.transform.position;
        //adjustingIsoOrigin.rotation = LeftController.transform.rotation;

        Debug.Log($"Stopped adjusting ISO");

        Debug.Log($"ISO value is {lookupValueFromRelativeIndex<int>(ISOIndex, determineMagnitude(LeftController.transform), isoValues)}");

        adjustmentTimer = 0f;
    }


    /// <summary>
    /// Determines the offset value based on position and rotation from the offset.
    /// 
    /// Assuming lift of +- 0.04, and pitch of +- 45 degrees
    /// Pitching up and lifting up should both yield a positive offset. 
    /// Pitching down and pushing down should both yield a negative offset 
    /// </summary>
    /// <param name="before"></param>
    /// <param name="after"></param>
    /// <returns></returns>
    private int determineMagnitude(Transform after)
    {
        int magnitude = 0;
        var liftDelta = after.transform.position.y - adjustmentOriginPosition.y;
        var liftScore = Mathf.CeilToInt((liftDelta) / (liftMax / adjustmentSteps / 2));
        var pitchDelta = (after.transform.rotation.eulerAngles.x - adjustmentOriginRotation.eulerAngles.x);

        var normalizedPitch = pitchDelta < -270f ? pitchDelta + 360f : pitchDelta;
        normalizedPitch = Mathf.CeilToInt(normalizedPitch / (pitchMax / adjustmentSteps / 2));

        //var result = liftScore > 0 ? liftScore : normalizedPitch;

        var result = liftScore;

        Debug.Log($"Your lift {Mathf.CeilToInt(liftDelta)} and pitch {Mathf.CeilToInt(pitchDelta)} yield {result}");

        return (int)result;
    }

    private T lookupValueFromRelativeIndex<T>(int currentIndex, int relativeIndex, T[] values)
    {
        int indexToSearch = -1;
        if (currentIndex - relativeIndex < 0)
        {
            indexToSearch = 0;
        }
        else if (currentIndex + relativeIndex >= values.Length)
        {
            indexToSearch = values.Length - 1;
        }

        return values[indexToSearch];
    }
    

    private void AdjustingIso_started(InputAction.CallbackContext context)
    {

            adjustmentOriginPosition = LeftController.transform.position;
        adjustmentOriginRotation = LeftController.transform.rotation;

            //adjustingIsoOrigin.position = LeftController.transform.position;
            //adjustingIsoOrigin.rotation = LeftController.transform.rotation;

            Debug.Log($"Started adjusting ISO: {adjustmentOriginPosition.y} {adjustmentOriginRotation.eulerAngles.x}");

            isAdjustingIso = true;
        adjustmentTimer = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {

        adjustmentTimer += adjustmentRate;
        if (adjustmentTimer > nextAdjustmentTime)
        {
            if(isAdjustingIso)
            {
              
                Debug.Log($"ISO value is {lookupValueFromRelativeIndex<int>(ISOIndex, determineMagnitude(LeftController.transform), isoValues)}");

                adjustmentTimer = 0f;
            }

        } else
        {

        }

        if (isAdjustingIso)
        {
        }
        else
        {

        }
    }

}
