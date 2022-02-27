using UnityEngine;
using System.Net;
using System;
using System.IO;
using SimpleJSON;

public class MakeCubes : MonoBehaviour
{
    // Create a plane, sphere and cube in the Scene.

    void Start()
    {
        //GameObject plane  = GameObject.CreatePrimitive(PrimitiveType.Plane);

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(0, 0.5f, 0);

        //GameObject cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.position = new Vector3(1, 0.5f, 0);

        //GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere.transform.position = new Vector3(0, 1.5f, 0);

        //GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        //capsule.transform.position = new Vector3(2, 1, 0);

        //GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        //cylinder.transform.position = new Vector3(-2, 1, 0);

        Debug.Log(GetAPI());

        var json = JSON.Parse(GetAPI());
        foreach(JSONNode o in json.Children)
        {



            //Debug.Log(string.Format("JSON request, info header: {0}", o["Sector_ID"]));
            //Debug.Log(string.Format("tileid: {0} , type: {1}, X_POS: {2}, Y_POS: {3}", o["tile_id"], o["type"], o["x"], o["y"]));
            var xintval = (byte)o["x"].AsInt;  // needed to get INT val from json output
            var yintval = (byte)o["y"].AsInt;
            var zintval = (byte)o["z"].AsInt;
            //Debug.Log(string.Format("xintval {0}, yintval {1}, zintval {2}", xintval, yintval, zintval)); 
            Instantiate(cube, new Vector3(xintval, zintval, yintval), Quaternion.identity);  

            string blocktype = o["type"].ToString();
            if (blocktype.Contains("dirt") == true)
            {
                Color brown = new Color(139f/255f, 69f/255f, 19f/255f, 1f);
                cube.GetComponent<Renderer>().material.color = brown;
                Debug.Log("Blocktype dirt");
            }

            //if (blocktype.Contains("grass") == true)
            //{
            //    Color green = new Color(0, 1, 0, 1);
            //    cube.GetComponent<Renderer>().material.color = green;
            //    Debug.Log("Blocktype grass");
            //}
            //Debug.Log(blocktype);
            
        }
    }



public string GetAPI()
{
    var request = (HttpWebRequest)WebRequest.Create("https://tilegridmainframe.qcm.repl.co/get");
    using (var response = (HttpWebResponse)request.GetResponse())
    using (var stream = response.GetResponseStream())
    using (var reader = new StreamReader(stream))
    {
        return reader.ReadToEnd();
    }
}

}