using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LandMinesHandle : MonoBehaviour
{
    [SerializeField]
    float flySpeed = 5;
    [SerializeField]
    float lifeTime = 3;
    bool startFly;
    Transform targetTransform;
    Animator animator;
    Action OnExplode;
    // Start is called before the first frame update
    public void Setup(Transform target, Action callback)
    {
        targetTransform = target;
        OnExplode = callback;
    }    

    private void Awake()
    {
        startFly = false;
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        startFly = true;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;
        FlyHandle(delta);
    }
    void FlyHandle(float delta)
    {
        Debug.LogError(startFly);
        if (startFly)
        {
            if (Vector2.Distance(targetTransform.position, transform.position) > 0.5f)
            {
                Vector3 direction = targetTransform.position - transform.position;
                direction.Normalize();
                transform.position += direction * flySpeed * delta;
            }
            else
            {
                animator.SetTrigger("Boom");
                OnExplode?.Invoke();
                StartCoroutine(Explode());
            }
        }
    }
    IEnumerator Explode()
    {
        startFly = false;
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }
}
