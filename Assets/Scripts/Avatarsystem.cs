using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatarsystem : MonoBehaviour
{
    //Initialise the data
    private Transform girlSourceTrans;
    private GameObject girlTarget;
     
    //save the data: part name; part ID; skin mesh render
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
    
    //skelton information
    Transform[] girlHips;

    //skm information after changing the clothes: part name; smr
    private Dictionary<string, SkinnedMeshRenderer> girlData2 = new Dictionary<string, SkinnedMeshRenderer>();

    //initialise which part refers to which number
    private string[,] girlStr = new string[,] { { "eyes", "1" }, { "hair", "1" }, { "top", "1" }, { "pants", "1" }, { "shoes", "1" }, { "face", "1" } };
     

    void Start()
    {
        InitialseSource();
        InitialseTarget();
        SaveData();
        InitAvatar();

    }
      
    void InitialseSource()
    {
        GameObject go = Instantiate(Resources.Load ("FemaleModel")) as GameObject;
        girlSourceTrans = go.transform;
        go.SetActive(false);
    }

    void InitialseTarget()
    {
        girlTarget = Instantiate(Resources.Load("FemaleTarget")) as GameObject;
        girlHips = girlTarget.GetComponentsInChildren<Transform>();
    }
      
    void SaveData()
    {
        //safe check
        if (girlSourceTrans == null)
        return;

        //go over all parts where consist skin mesh render
        SkinnedMeshRenderer[] parts = girlSourceTrans.GetComponentsInChildren<SkinnedMeshRenderer>();

        //find the names of these parts and split these names, save of the smr into girlData
        foreach (var part in parts)
        {
            string[] names = part.name.Split('-');
            if (!girlData.ContainsKey(names[0]))  //if the generated part is gone over first time, not only add skm information to girlData and also add to girlData2(girlSmr), which
                //excludes the number ID
            {
                GameObject partGo = new GameObject(); 
                partGo.name = names[0];
                partGo.transform.parent = girlTarget.transform;
                girlData2.Add(names[0], partGo.AddComponent<SkinnedMeshRenderer>()); //record the skm inf on the target's skelton and only record one time 
                girlData.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>()); //
                
            } 

           girlData[names[0]].Add(names[1], part);

        }
        
    }

    void ChangeMesh (string part, string num) 
    {
        SkinnedMeshRenderer skm = girlData[part][num]; //find the position to change the part
        List<Transform> bones = new List<Transform>();

        foreach(var trans in skm.bones) // go over all the bones under mesh
        {
            foreach (var bone in girlHips)
            {

                if (bone.name == trans.name)
                {
                    bones.Add(bone);
                    break;
                }
            }
        }

        //change the clothes: change the bones - change the materials - change the mesh
        girlData2[part].bones = bones.ToArray();
        girlData2[part].materials = skm.materials;
        girlData2[part].sharedMesh = skm.sharedMesh;

    }

    void InitAvatar () //initialise the skeleton with mesh, materials and bones
    {
        int length = girlStr.GetLength(0); //get the number of the row
        for (int i = 0; i < length; i++)
        {
            ChangeMesh(girlStr[i, 0], girlStr[i, 1]);
        }
       
    }

}
