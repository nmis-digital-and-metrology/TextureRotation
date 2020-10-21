using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatarsystem1 : MonoBehaviour
{

    private GameObject girlSource;
    private Transform girlSourceTrans;

    private GameObject girlTarget;

    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();

    Transform[] girlHips;

    void Start()
    {
        girlSource = Instantiate(Resources.Load("FemaleModel")) as GameObject;
        girlSourceTrans = girlSource.transform; 
        girlSource.SetActive(false);

        girlTarget = Instantiate(Resources.Load("FemaleTarget")) as GameObject;
        
        girlHips = girlTarget.GetComponentsInChildren<Transform>();

    }

   

}
