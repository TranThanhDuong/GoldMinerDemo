using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BYDataTableMaker : MonoBehaviour
{
    [MenuItem("Assets/BY/CreateBinaryDataFromCSV",false,1)]
    public static void CreateBinaryDataFromCSV()
    {
        foreach(UnityEngine.Object e in Selection.objects)
        {
            TextAsset csvFile = (TextAsset)e;
            string nameTable = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csvFile));
          
            ScriptableObject scriptableObject = ScriptableObject.CreateInstance(nameTable);
             if(scriptableObject!=null)
            {
                AssetDatabase.CreateAsset(scriptableObject, "Assets/Resources/DataTable/" + nameTable + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                BYDataTableCreate bycreate = (BYDataTableCreate)scriptableObject;
                bycreate.ImportData(csvFile);
                EditorUtility.SetDirty(bycreate);
                    

            }
        }
    
    }
    [MenuItem("Assets/BY/Create CSV File form ScriptableTable (Binary file)", false, 1)]
    private static void CreateCSVFile()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            BYDataTableCreate dataFile = (BYDataTableCreate)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(dataFile));
            string data = dataFile.GetCSVData();
            string filePath = "Assets/Data/DataTable/" + tableName + ".csv";
            FileUtil.DeleteFileOrDirectory(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Create);//Tạo file mới tên là test.txt            
            // request.RawData = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(dataSend));
            StreamWriter sWriter = new StreamWriter(fs);//fs là 1 FileStream 
            sWriter.Write(data);
            // Ghi và đóng file
            sWriter.Flush();
            fs.Close();
            AssetDatabase.Refresh();
        }
    }
    [MenuItem("Assets/BY/Make Data by CSV to local")]
    public static void CreateAsset2()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            TextAsset csvFile = (TextAsset)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(csvFile));
            ScriptableObject scriptableTable = ScriptableObject.CreateInstance(tableName);


            if (scriptableTable == null)
                return;
            Debug.Log(Path.GetDirectoryName(AssetDatabase.GetAssetPath(csvFile)));
            AssetDatabase.CreateAsset(scriptableTable, Path.GetDirectoryName(AssetDatabase.GetAssetPath(csvFile)) + "/" + tableName + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            BYDataTableCreate by = (BYDataTableCreate)scriptableTable;
            by.ImportData(csvFile);

            EditorUtility.SetDirty(by);
        }
    }

    [MenuItem("Assets/BY/Create JSon File form ScriptableTable (Binary file)", false, 1)]
    private static void CreateJsonFile()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            BYDataTableCreate dataFile = (BYDataTableCreate)obj;
            string tableName = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(dataFile));
            string data = dataFile.GetJsonData();
            string filePath = "Assets/Data/DataTable/Json/" + tableName + ".json";
            FileUtil.DeleteFileOrDirectory(filePath);

            FileStream fs = new FileStream(filePath, FileMode.Create);//Tạo file mới tên là test.txt            
            // request.RawData = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(dataSend));
            StreamWriter sWriter = new StreamWriter(fs);//fs là 1 FileStream 
            sWriter.Write(data);
            // Ghi và đóng file
            sWriter.Flush();
            fs.Close();
            AssetDatabase.Refresh();
        }
    }
}
