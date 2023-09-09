using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldObject : TouchObject
{
    public override void TouchBehaviour()
    {
        holeObject.transform.parent = null;
        if(effectPath.Length > 0)
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load(effectPath), this.transform.position, Quaternion.identity, this.transform);
        }
    }
}
