using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
	const string FILE_DIR = "/SAVEDATA/";
	string FILE_NAME = "<name>.json";

	string FILE_PATH;

    // Start is called before the first frame update
    void Start()
    {
	    FILE_NAME = FILE_NAME.Replace("<name>", name);
	    FILE_PATH = Application.dataPath + FILE_DIR + FILE_NAME;
	    
	    // if the file exists convert from JSON to a position vector
	    if (File.Exists(FILE_PATH))
	    {
		    string jsonStr = File.ReadAllText(FILE_PATH);
		    transform.position = JsonUtility.FromJson<Vector3>(jsonStr);
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition(); 
    }
    
    Vector3 GetMouseWorldPosition()
    {
        Vector3 result = Input.mousePosition;
        // accounting for depth from visual ray trace in x and y from z
        result.z = Camera.main.WorldToScreenPoint(transform.position).z;
        result = Camera.main.ScreenToWorldPoint(result);
        return result;
     }
    
	void OnApplicationQuit()
	{
		// convert position vector into JSON text 
		string fileContent = JsonUtility.ToJson(transform.position, true);
		
		Debug.Log(fileContent);
		File.WriteAllText(FILE_PATH,fileContent);
	}

}  
