using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axiscontroller : MonoBehaviour
{



    public GameObject arrow_x;
    public GameObject arrow_y;
    public GameObject arrow_z;
    public GameObject center;

    private GameObject m_arrowx;
    private GameObject m_arrowz;
    private GameObject m_arrowy;
    private GameObject m_center;

    public Vector3 xorigscale;
    public Vector3 yorigscale;
    public Vector3 zorigscale;
    public Vector3 centerorigscale;

    public bool ismoved;
    public Vector3 lastpos;
    // Start is called before the first frame update
    void Start()
    {

        arrow_x = Resources.Load<GameObject>("arrow_x");
        arrow_y = Resources.Load<GameObject>("arrow_y");
         arrow_z = Resources.Load<GameObject>("arrow_z");
         center = Resources.Load<GameObject>("center");
        
        Axis_initialize();
       Axis_disable();
        ismoved = false;
        lastpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastpos != transform.position) 
        {
            ismoved = true;
            lastpos = transform.position;
        }
        else { ismoved = false; }
        /*
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
            Ray ray = m_cam.ScreenPointToRay(pos);
            RaycastHit hit;
            Physics.Raycast(m_cam.transform.position, ray.direction, out hit, 10000.0f);
            Debug.DrawLine(m_cam.transform.position, ray.direction);
            if (hit.collider.gameObject.name == "arrowx")
            {
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            }
        }*/
    }





    public void Axis_initialize()
    {
        
        m_arrowx = GameObject.Instantiate(arrow_x, Vector3.zero, arrow_x.transform.rotation);

      

       m_arrowy = GameObject.Instantiate(arrow_y, Vector3.zero, arrow_y.transform.rotation); 
      m_arrowz = GameObject.Instantiate(arrow_z, Vector3.zero ,arrow_z.transform.rotation);
      m_center = GameObject.Instantiate(center, Vector3.zero, center.transform.rotation);

      m_arrowx.transform.name = "arrowx";
      m_arrowy.transform.name = "arrowy";
      m_arrowz.transform.name = "arrowz";
     m_center.transform.name = "center";

        m_arrowx.transform.SetParent(this.transform);
        m_arrowy.transform.SetParent(this.transform);
        m_arrowz.transform.SetParent(this.transform);
        m_center.transform.SetParent(this.transform);

     

        Vector3 obj_center = Compute_center() +  arrow_x.transform.position;
        m_arrowx.transform.position = obj_center;

        obj_center = Compute_center() + arrow_y.transform.position;
        m_arrowy.transform.position = obj_center;

        obj_center = Compute_center() + arrow_z.transform.position;
        m_arrowz.transform.position = obj_center;

        obj_center = Compute_center() + center.transform.position;
        m_center.transform.position = obj_center;


        xorigscale = m_arrowx.transform.localScale;
        yorigscale = m_arrowy.transform.localScale;
        zorigscale = m_arrowz.transform.localScale;
        centerorigscale = m_center.transform.localScale;

    }

public void axis_update()
    {
        Vector3 obj_center = Compute_center() + this.transform.Find("arrowx").position;
        m_arrowx.transform.position = obj_center;

        obj_center = Compute_center() + this.transform.Find("arrowy").position;
        m_arrowy.transform.position = obj_center;

        obj_center = Compute_center() + this.transform.Find("arrowz").position;
        m_arrowz.transform.position = obj_center;

        obj_center = Compute_center() + this.transform.Find("center").position;
        m_center.transform.position = obj_center;
    }

    public void Axis_enable()
    {
        m_arrowx.SetActive(true);
        m_arrowy.SetActive(true);
        m_arrowz.SetActive(true);
        m_center.SetActive(true);
    }


    public void Axis_disable()
    {
        m_arrowx.SetActive(false);
        m_arrowy.SetActive(false);
        m_arrowz.SetActive(false);
        m_center.SetActive(false);
    }


    Vector3 Compute_center()
    {
        Mesh objmesh = this.gameObject.GetComponent<MeshFilter>().mesh;

        Matrix4x4 localToWorld = transform.localToWorldMatrix;

        float max_x = -1000, max_y = -1000, max_z = -1000, min_x = 1000, min_y = 1000, min_z = 1000;

        for (int i = 0; i < objmesh.vertices.Length; i++)
        {
            Vector3 tmp_vertex = objmesh.vertices[i];

            if (tmp_vertex.x > max_x)
            {
                max_x = tmp_vertex.x;
            }

            if  (tmp_vertex.y > max_y)
            {
                max_y = tmp_vertex.y;
            }

            if (max_z > tmp_vertex.z)
            {
                max_z = tmp_vertex.z;
            }

            if (tmp_vertex.x < min_x)
            {
                min_x = tmp_vertex.x;
            }

            if (tmp_vertex.y < min_y)
            {
                min_y = tmp_vertex.y;
            }

            if (min_z < tmp_vertex.z)
            {
                min_z = tmp_vertex.z;
            }
        }

        float c_x = (min_x + max_x) / 2.0f;
        float c_y = max_y + (max_y * 0.3f);
        float c_z = (min_z + max_z) /2.0f ;
        return  localToWorld.MultiplyPoint3x4(new Vector3(c_x, c_y , c_z));

    }

    public void scale_xarrow(float s)
    {
        m_arrowx.transform.localScale *= s;
    }

    public void scale_yarrow(float s)
    {
        m_arrowy.transform.localScale *= s;
    }

    public void scale_zarrow(float s)
    {
        m_arrowz.transform.localScale *= s;
    }

    public void scale_center(float s)
    {
        m_center.transform.localScale *= s;
    }


    public void reset_arrowx_scale() 
    {
        m_arrowx.transform.localScale = xorigscale;
    }

    public void reset_arrowy_scale()
    {
        m_arrowy.transform.localScale = yorigscale;
    }

    public void reset_arrowz_scale()
    {
        m_arrowz.transform.localScale = zorigscale;
    }

    public void reset_center_scale()
    {
        m_center.transform.localScale = centerorigscale;
    }

}
