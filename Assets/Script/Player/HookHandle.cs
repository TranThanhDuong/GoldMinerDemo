using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookHandle : MonoBehaviour
{
    [SerializeField]
    Transform hookRotation;
    [SerializeField]
    Transform hookTransform;
    [SerializeField]
    LineRenderer ropeRenderer;
    [SerializeField]
    float maxLeftRotation = -80;
    [SerializeField]
    float maxRightRotation = 80;
    [SerializeField]
    [Range(10, 50)]
    float rotationSpeed = 20f;
    [SerializeField]
    [Range(1, 10)]
    float shootSpeed = 1f;
    [SerializeField]
    [Range(1, 10)]
    float returnSpeed = 1f;

   

    InputHandle inputHandle;

    bool isFire;
    bool isBack;
    bool isBuff;
    public bool IsFire => (isFire || isBack);
    public bool IsBuff => isBuff;
    float defaultHookPosition;
    float rotationDirection;
    float pivotAngle;

    float strengthBuff;

    PlayerDatabinding databinding;
    ClawHandle clawHandle;
    // Start is called before the first frame update
    void Start()
    {
        //Default value
        isFire = false;
        isBuff = false;
        pivotAngle = 0;
        rotationDirection = 1;
        strengthBuff = 1;
        defaultHookPosition = hookTransform.localPosition.y;

        //Get Component
        inputHandle = GetComponentInParent<InputHandle>();
        databinding = GetComponentInParent<PlayerDatabinding>();
        clawHandle = GetComponentInChildren<ClawHandle>();

        //Rope default
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, hookTransform.position);
    }

    private void OnEnable()
    {
        ropeRenderer.SetPosition(0, transform.position);
        ropeRenderer.SetPosition(1, hookTransform.position);
    }

    IEnumerator WaitBuff()
    {
        yield return new WaitForSeconds(1);
        isBuff = false;
    }

    // Update is called once per frame

    #region Movement and Rotation
    public void HookRotation(float delta)
    {
        
        if(IsFire)
        {
            return;
        }
        pivotAngle += rotationDirection * delta * rotationSpeed;

        if(pivotAngle >= maxRightRotation)
        {
            pivotAngle = maxRightRotation;
            rotationDirection = -1;
        }
        else if(pivotAngle <= maxLeftRotation)
        {
            pivotAngle = maxLeftRotation;
            rotationDirection = 1;
        }
        Vector3 rotation = new Vector3(0, 0, pivotAngle);
        hookRotation.rotation = Quaternion.Euler(rotation);
        ropeRenderer.SetPosition(1, hookTransform.position);
    }

    public void HookShooting(float delta)
    {
        if(IsFire  || inputHandle.ShootingInput)
        {
            if(!isFire)
            {
                isFire = true;
            }    

            if(isBack)
            {
                float yPos = hookTransform.localPosition.y + returnSpeed * (1/clawHandle.GetDataHold().weight) * strengthBuff * delta;
                if (yPos >= defaultHookPosition)
                {
                    yPos = defaultHookPosition;
                    isBack = false;
                    isFire = false;
                    ReciveScore(clawHandle.GetDataHold().score);
                    databinding.HardDrag = false;
                    databinding.NormalDrag = false;
                    if(clawHandle.GetDataHold().isBad)
                    {
                        SoundManager.Instance.StopSound();
                        databinding.Sad = true;
                    }
                    else
                    {
                        databinding.Buff = true;
                        SoundManager.Instance.PlaySound(SoundType.GET_POINT);
                    }
                    clawHandle.DestroyHoldObject();
                    isBuff = true;
                    StartCoroutine(WaitBuff());
                }
                SoundManager.Instance.PlaySound(SoundType.DRAG);
                hookTransform.localPosition = new Vector3(0, yPos, 0);
                ropeRenderer.SetPosition(1, hookTransform.position);
            }   
            else
            {
                float yPos = hookTransform.localPosition.y  - shootSpeed * delta;
                Vector3 direction = hookTransform.position - hookRotation.position;

                hookTransform.localPosition = new Vector3(0, yPos, 0);
                ropeRenderer.SetPosition(1, hookTransform.position);
                isBack = clawHandle.DetachObjectTouch(direction);
                SoundManager.Instance.PlaySound(SoundType.SHOOT);

                if (isBack)
                {
                    if (clawHandle.GetDataHold().isBad)
                    {
                        SoundManager.Instance.PlaySound(SoundType.STONE_HIT);
                        databinding.HardDrag = true;
                    }
                    else
                    {
                        SoundManager.Instance.PlaySound(SoundType.OTHER_HIT);
                        databinding.NormalDrag = true;
                    }    
                }
            }    
        }    
    }
    #endregion
    #region Recive Hold Object
    void ReciveScore(int score)
    {
        MissionControl.Instance.AddScore(score);
    }
    #endregion
}
