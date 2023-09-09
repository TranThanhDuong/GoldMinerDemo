using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObjectHoldData
{
    public float weight;
    public int score;
    public bool isBad;
    public void ResetData()
    {
        weight = 1;
        score = 0;
        isBad = false;
    }
}
public class ClawHandle : MonoBehaviour
{
    [SerializeField]
    Sprite normalSprite;
    [SerializeField]
    Sprite holdSprite;
    [SerializeField]
    GameObject objectHold;
    [SerializeField]
    LayerMask ignoreLayer;
    [SerializeField]
    [Range(0f, 1f)]
    float hookRadiusDetach = 1f;

    SpriteRenderer spriteRenderer;
    ObjectHoldData curDataHold;

    public bool CanBoom => canBoom;
    bool canBoom;
    private void Start()
    {
        canBoom = false;
        ignoreLayer = ~(1 << 7);
        curDataHold = new ObjectHoldData();
        curDataHold.ResetData();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public bool DetachObjectTouch(Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, hookRadiusDetach, direction, 0, ignoreLayer);
        if (hit)
        {

            if (hit.collider.gameObject.layer == 8)
            {
                TouchObject obj = hit.collider.GetComponent<TouchObject>();
                obj.TouchBehaviour();

                if (obj.IsExplode)
                {
                    curDataHold.isBad = true;
                    return true;
                }
                spriteRenderer.sprite = holdSprite;

                canBoom = true;

                objectHold = obj.gameObject;
                objectHold.transform.parent = this.transform;
                objectHold.transform.localPosition = new Vector3(0, -0.3f, 0);
                
                curDataHold.score = obj.Score;
                curDataHold.weight = obj.Weight;
                curDataHold.isBad = obj.IsRock;
            }
            else
            {
                curDataHold.isBad = true;
            }
            return true;
        }
        return false;
    }

    public ObjectHoldData GetDataHold()
    {
        return curDataHold;
    }

    public void DestroyHoldObject()
    {
        canBoom = false;
        curDataHold.ResetData();
        spriteRenderer.sprite = normalSprite;
        if (objectHold)
        {
            Object.Destroy(objectHold);
        }
    }
}
