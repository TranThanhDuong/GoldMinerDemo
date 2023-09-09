using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataObjectScene
{
    public int id;
    public Vector2 pos;
}

[Serializable]
public class ConfigDataObjectsRecord
{
    public int id;
    public int totalScore;
    [SerializeField]
    private string objectsID;
    [SerializeField]
    private string posX;
    [SerializeField]
    private string posY;

    public List<DataObjectScene> lsObjects
    {
        get
        {
            List<DataObjectScene> ls = new List<DataObjectScene>();

            string[] sArrayID = objectsID.Split(';');
            string[] sArrayPosX = posX.Split(';');
            string[] sArrayPosY = posY.Split(';');
            int i = 0;
            foreach (string s in sArrayID)
            {
                DataObjectScene dataNew = new DataObjectScene();
                dataNew.id = int.Parse(s);

                float pX = 0f;
                float pY = 0f;
                if (i < sArrayPosX.Length)
                    pX = float.Parse(sArrayPosX[i]);
                if (i < sArrayPosY.Length)
                    pY = float.Parse(sArrayPosY[i]);

                dataNew.pos = new Vector2(pX,pY);
                ls.Add(dataNew);
                i++;
            }

            return ls;
        }
    }
}
public class ConfigDataObjects : BYDataTable<ConfigDataObjectsRecord>
{
    public override void SetCompareObject()
    {
        base.SetCompareObject();
        recoreCompare = new ConfigCompareKey<ConfigDataObjectsRecord>("id");
    }
    public List<ConfigDataObjectsRecord> GetAllRecords()
    {
        return records;
    }
}
