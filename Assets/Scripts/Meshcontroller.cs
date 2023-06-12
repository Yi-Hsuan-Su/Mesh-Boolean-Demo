using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meshcontroller : MonoBehaviour
{
    [SerializeField]
    public bool isselected;
    public bool isenter;
    public bool isfirstentry;
    public Meshprocess m_meshproccess;
    public GameObject resultobject;
    public Collider m_other;
    public Mesh origmesh;
    public Material origmat;
    public Transform origtrans;
    public Vector3 origpos;

    public List<GameObject> objlist;
    public bool isexecute;
    
    private void Start()
    {
        isselected = false;
        isenter = false;
        isfirstentry = true;
        m_meshproccess = this.GetComponent<Meshprocess>();
        origmesh =this.GetComponent<MeshFilter>().sharedMesh;
        origmat = this.GetComponent<MeshRenderer>().sharedMaterial;
        origtrans = this.GetComponent<Transform>();
        origpos = this.transform.position;

        objlist = new List<GameObject>();
        isexecute = true;

    }

    private void Update()
    {


        if ( ( isexecute || this.GetComponent<Axiscontroller>().ismoved) && isenter && objlist.Count != 0   )
            {
                ComputeSubstract(m_other, objlist);
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
             }

        for (int i = 0; i < objlist.Count; i++)
        {
            if (objlist[i].GetComponent<Axiscontroller>().ismoved)
            {
                isexecute = true;
                break;
            }
            else
            {
                isexecute = false;
            }
        }

        if (this.gameObject.GetComponent<Highlight>().ishighlight)
        {
            if(GameObject.Find(this.name+"new"))
                GameObject.Find(this.name+"new").GetComponent<Outline>().enabled = true;
        }
        else
        {
            if(GameObject.Find(this.name+"new"))
                GameObject.Find(this.name + "new").GetComponent<Outline>().enabled = false;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        isenter = true;
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Object")) 
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        if (!objlist.Exists(t => t == other.gameObject) && other.gameObject.CompareTag("Object")) 
        {
            objlist.Add(other.gameObject);
        }

        m_other = other;
    }

    private void OnTriggerStay(Collider other)
    {
        if(objlist.Count >0)
             isenter = true;

   
    }


    private void OnTriggerExit(Collider other)
    {
        isenter = false;
        m_other = null;
        if (other.gameObject.CompareTag("Object"))
        {
            Debug.Log(other.gameObject.name + "Leave");
            objlist.Remove(other.gameObject);
        }
        
        if (objlist.Count ==0  )
        {
            /*
            if (!other.gameObject.CompareTag("Object")) 
            {    
                this.gameObject.GetComponent<Outline>().enabled = true;
            }*/
            if (GameObject.Find(this.name + "new"))
            {
                Object.Destroy(GameObject.Find(this.name + "new"));
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        
        }
    }

    void ComputeSubstract(Collider other, List<GameObject> objlist)
    {
       
        m_meshproccess.substract(this.gameObject, objlist);
       


        
       

        
        /*
        GameObject utmp = new GameObject();
        utmp.transform.name = this.name + "utmp";
        //newobj.transform.position = new Vector3(-6, 0, 0);
        utmp.AddComponent<MeshFilter>().sharedMesh = m_meshproccess.r_mesh;
        utmp.AddComponent<MeshRenderer>().sharedMaterials = m_meshproccess.r_material;
       
        for (int i = 2; i < tmplist.Count-1; i++) 
        {
            m_meshproccess.Union(utmp, tmplist[i + 1]);
        }*/



        


        if (GameObject.Find(this.name + "new"))
         {
            Object.Destroy(GameObject.Find(this.name + "new").GetComponent<MeshFilter>().sharedMesh);
             Object.Destroy(GameObject.Find(this.name + "new"));
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
        
        GameObject newobj = new GameObject();
        newobj.transform.name = this.name+"new";
       
        newobj.AddComponent<MeshFilter>().sharedMesh = m_meshproccess.r_mesh; //---
       newobj.AddComponent<MeshRenderer>().sharedMaterials = m_meshproccess.r_material;
  
        
        List<Material> newmat = new List<Material>();

        for (int i = 0; i < newobj.GetComponent<MeshRenderer>().materials.Length; i++) 
        {
            newmat.Add(Resources.Load<Material>("Material/Object"));
        }
     

        newobj.GetComponent<MeshRenderer>().materials = newmat.ToArray();


        if (this.gameObject.GetComponent<Highlight>().ishighlight)
        {
            newobj.AddComponent<Outline>().enabled = true;
        }
        else
        {
            newobj.AddComponent<Outline>().enabled = false;
        }

        newobj.transform.SetParent(this.transform);

    }
    
}
