using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parabox.CSG;


public class Meshprocess : MonoBehaviour
{
    [SerializeField]
    public Mesh r_mesh;
    public Material[] r_material;
    public Model result;

    private void Start()
    {

    }

    public void substract(GameObject orig, List<GameObject> objlist)
    {


        if (objlist.Count == 1)
        {         
            result = CSG.Subtract(orig, objlist[0]);

    
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
            r_mesh = result.mesh;
            r_material = result.materials.ToArray();
        }
        else if (objlist.Count > 1)
        {
 

        
 
                result = CSG.Subtract(orig, objlist[0]);



            for (int i = 1; i < objlist.Count; i++)
            {

                        GameObject tmp = new GameObject();
                        tmp.transform.name = orig.name + "tmp";
                        tmp.AddComponent<MeshFilter>().mesh = result.mesh;
                        tmp.AddComponent<MeshRenderer>().materials = result.materials.ToArray();
                        Destroy(result.mesh);

                        result = CSG.Subtract(tmp, objlist[i]);
                        Destroy(tmp.GetComponent<MeshFilter>());
                        Destroy(tmp.GetComponent<MeshRenderer>());
                        Object.Destroy(tmp);
                        System.GC.Collect();
                    
            }


            r_mesh = result.mesh;
            r_material = result.materials.ToArray();

        }

    }


    public void Union(GameObject a, GameObject b)
    {
        Model result = CSG.Union(a, b);

        r_mesh = result.mesh;
        r_material = result.materials.ToArray();
    }
}
