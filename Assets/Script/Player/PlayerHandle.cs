using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandle : MonoBehaviour
{
    [SerializeField]
    Transform projecties;
    [SerializeField]
    int clipSize = 1;
    [SerializeField]
    float maxMoveLeftRight = 5;
    [SerializeField]
    float moveSpeed = 5;
    [SerializeField]
    Transform boomSpawnPosition;

    Transform trans;
    InputHandle inputHandle;
    HookHandle hookHandle;
    ClawHandle clawHandle;
    GameObject boomObj;
    public PlayerDatabinding databinding;

    bool isMove = false;
    bool isThrow = false;
    float checkStand = 0;
    // Start is called before the first frame update
    void Start()
    {
        trans = transform;
        inputHandle = GetComponent<InputHandle>();
        databinding = GetComponent<PlayerDatabinding>();
        hookHandle = GetComponentInChildren<HookHandle>();
        clawHandle = GetComponentInChildren<ClawHandle>();
        boomObj = Instantiate(projecties).gameObject;

        boomObj.GetComponent<LandMinesHandle>().Setup(clawHandle.transform, () => { isThrow = false; clawHandle.DestroyHoldObject(); });
        boomObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (MissionControl.Instance.IsPause)
            return;

        float delta = Time.deltaTime;
        HandleMovement(delta);
        HandleThrow(delta);

        hookHandle.HookRotation(delta);
        hookHandle.HookShooting(delta);
    }

    #region Handle
    void HandleMovement(float delta)
    {
        if (hookHandle.IsFire || hookHandle.IsBuff)
            return;

        float posX = trans.position.x;
        float velocity = inputHandle.MovementInput.x * moveSpeed * delta;

        if(velocity != 0)
        {
            hookHandle.gameObject.SetActive(false);
            if(!isMove)
            {
                isMove = true;
                databinding.Walk = true;
                checkStand = 0.2f;
            }

            if (velocity < 0)
            {
                trans.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else
            {
                trans.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            posX += velocity;
            if (posX >= maxMoveLeftRight)
            {
                posX = maxMoveLeftRight;
            }
            else if (posX <= -maxMoveLeftRight)
            {
                posX = -maxMoveLeftRight;
            }
            SoundManager.Instance.PlaySound(SoundType.MOVE_1);
            trans.position = new Vector3(posX, trans.position.y, 0);
        }
        else if(isMove)
        {
            if(checkStand > 0)
            {
                checkStand -= Time.deltaTime;
                return;
            }
            checkStand = 0;
            isMove = false;
            hookHandle.gameObject.SetActive(true);
            trans.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            databinding.Walk = false;
        }    
    }

    void HandleThrow(float delta)
    {
        if (clawHandle.CanBoom && !isThrow && inputHandle.BoomInput)
        {
            databinding.Throw = true;
            StartCoroutine("ThrowBoom");
        }
    }

    IEnumerator ThrowBoom()
    {
        yield return new WaitForSeconds(0.7f);
        isThrow = true;
        boomObj.transform.position = boomSpawnPosition.position;
        boomObj.SetActive(true);
    }    
    #endregion Handle
}
