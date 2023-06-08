using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;
public class test : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    // Start is called before the first frame update
    void Start()
    {
        Model result = CSG.Subtract(a, b);

        b.GetComponent<MeshFilter>().sharedMesh = result.mesh;
        b.GetComponent<MeshRenderer>().sharedMaterials = result.materials.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
