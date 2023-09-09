using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    [SerializeField]
    int score;
    [SerializeField]
    float weight;
    [SerializeField]
    bool isRock;
    [SerializeField]
    bool isExplode;
    [SerializeField]
    protected GameObject holeObject;
    [SerializeField]
    protected string effectPath;
    public int Score => score;
    public float Weight => weight;
    public bool IsExplode => isExplode;
    public bool IsRock => isRock;
    public virtual void TouchBehaviour()
    {

    }    
}
