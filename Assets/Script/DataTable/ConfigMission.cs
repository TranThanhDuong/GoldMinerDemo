using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConfigMissionRecord
{
    public int id;
    public int sceneID;
    public int dataObjectsID;
}
public class ConfigMission : BYDataTable<ConfigMissionRecord>
{
    public override void SetCompareObject()
    {
        base.SetCompareObject();
        recoreCompare = new ConfigCompareKey<ConfigMissionRecord>("id");
    }
    public List<ConfigMissionRecord> GetAllRecords()
    {
        return records;
    }
}

