using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigObjectRecord
{
    public int id;
    public string prefab;
}
public class ConfigObject : BYDataTable<ConfigObjectRecord>
{
    public override void SetCompareObject()
    {
        base.SetCompareObject();
        recoreCompare = new ConfigCompareKey<ConfigObjectRecord>("id");
    }
    public List<ConfigObjectRecord> GetAllRecords()
    {
        return records;
    }
}

