using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeObject : TouchObject
{
    [SerializeField]
    float radiusEffect = 1.5f;
    [SerializeField]
    bool isDestroyOther;
    [SerializeField]
    LayerMask masksDestroy;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        masksDestroy = (1 << 8);
    }
    public override void TouchBehaviour()
    {
        base.TouchBehaviour();
        animator.SetTrigger("Boom");
        StartCoroutine(Explore());
    }

    IEnumerator Explore()
    {
        yield return new WaitForSeconds(0.5f);
        if(isDestroyOther)
        {
            SoundManager.Instance.PlaySound(SoundType.BOOM);
            DestroyObjectAround();
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    void DestroyObjectAround()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(this.transform.position, radiusEffect, Vector3.zero, 1, masksDestroy);
        foreach(RaycastHit2D hit in hits)
        {
            TouchObject obj = hit.collider.gameObject.GetComponent<TouchObject>();
            if(obj.IsExplode)
            {
                obj.TouchBehaviour();
            }
            else
            {
                Destroy(obj.gameObject);
            }    
            
        }    
    }    
}
