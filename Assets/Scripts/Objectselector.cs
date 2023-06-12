using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectselector : MonoBehaviour
{

    public Vector2 pos;
    public Camera m_cam;
    public Highlight m_highlight;
    public GameObject selectedGameobject;
    public bool isclicking;
    public bool obj_lock;
    public GameObject cur_obj;
    // Start is called before the first frame update

    public float xSpeed = 20.0f;
    public float ySpeed = 120.0f;
    void Start()
    {
        m_cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        isclicking = false;
        obj_lock = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !isclicking)
        {

            pos = Input.mousePosition;
            Ray ray = m_cam.ScreenPointToRay(pos);
            RaycastHit hit;
            Physics.Raycast(m_cam.transform.position, ray.direction, out hit, 10000.0f);
            ///Debug.DrawLine(m_cam.transform.position, ray.direction);
            if (hit.collider)
            {
                Debug.Log(hit.collider.transform.name);
                if (!obj_lock || hit.collider.gameObject.CompareTag("Arrowx") || hit.collider.gameObject.CompareTag("Arrowy") || hit.collider.gameObject.CompareTag("Arrowz") || hit.collider.gameObject.CompareTag("center"))
                {
                    selectedGameobject = hit.collider.gameObject;
                    obj_lock = true;
                }
                else if ( (hit.collider.gameObject.CompareTag("Object") || hit.collider.gameObject.CompareTag("solid") ) && obj_lock)
                {
                    initialize();
                    selectedGameobject = hit.collider.gameObject;
                   obj_lock = false;
                }

                if (selectedGameobject.CompareTag("Object") || selectedGameobject.CompareTag("solid"))
                {

                    cur_obj = selectedGameobject;
                    // selectedGameobject.GetComponent<Meshcontroller>().isselected = true;


                    selectedGameobject.GetComponent<Highlight>().ishighlight = true;
                    m_highlight = selectedGameobject.GetComponent<Highlight>();
                    m_highlight.m_outline.enabled = true;
                    /*
                    if (selectedGameobject.GetComponent<MeshRenderer>().enabled == true)
                    {
                        m_highlight = selectedGameobject.GetComponent<Highlight>();
                        m_highlight.m_outline.enabled = true;
                    }
                    else 
                    {
                        selectedGameobject.GetComponent<Highlight>().ishighlight = true;
                        m_highlight = selectedGameobject.GetComponent<Highlight>();
                        m_highlight.m_outline.enabled = true;
                    }*/

                    selectedGameobject.GetComponent<Axiscontroller>().Axis_enable();
                  obj_lock = true;
                }

                if (selectedGameobject.CompareTag("Arrowx"))
                {
                    isclicking = true;
                    cur_obj.GetComponent<Axiscontroller>().scale_xarrow(1.1f);
                }
                else if (selectedGameobject.CompareTag("Arrowz"))
                {
                    isclicking = true;
                    cur_obj.GetComponent<Axiscontroller>().scale_zarrow(1.1f);
                }
                else if (selectedGameobject.CompareTag("Arrowy"))
                {
                    isclicking = true;
                    cur_obj.GetComponent<Axiscontroller>().scale_yarrow(1.1f);
                }
                else if (selectedGameobject.CompareTag("center"))
                {
                    isclicking = true;
                    cur_obj.GetComponent<Axiscontroller>().scale_center(1.1f);
                }
            
            }
            else
            {
             

                initialize();
            }
        }

        else if (Input.GetMouseButtonUp(0) && isclicking)
        {
            isclicking = false;
            if (selectedGameobject.CompareTag("Arrowx"))
            {
                cur_obj.GetComponent<Axiscontroller>().reset_arrowx_scale();
            }
            else if (selectedGameobject.CompareTag("Arrowy"))
            {
                cur_obj.GetComponent<Axiscontroller>().reset_arrowy_scale();
            }
            else if (selectedGameobject.CompareTag("Arrowz")) 
            {
                cur_obj.GetComponent<Axiscontroller>().reset_arrowz_scale();
            }
            else
            {
                cur_obj.GetComponent<Axiscontroller>().reset_center_scale();
            }
        }

        if (isclicking)
        {
            if (selectedGameobject.CompareTag("Arrowx"))
            {
               
                Vector3 temp = new Vector3(cur_obj.transform.position.x - Input.GetAxis("Mouse X") * xSpeed * 0.01f, cur_obj.transform.position.y, cur_obj.transform.position.z);
                cur_obj.transform.position = temp;

                selectedGameobject.GetComponent<Outline>().enabled = true;
                selectedGameobject.GetComponent<Arrowglow>().ToggleHighlight(true);


            }
            else if (selectedGameobject.CompareTag("Arrowy"))
            {
                Vector3 temp = new Vector3(cur_obj.transform.position.x, cur_obj.transform.position.y + Input.GetAxis("Mouse Y") * xSpeed * 0.01f, cur_obj.transform.position.z);
                cur_obj.transform.position = temp;

                // selectedGameobject.GetComponent<Outline>().enabled = true;
                //selectedGameobject.GetComponent<Arrowglow>().ToggleHighlight(true);

            }
            else if (selectedGameobject.CompareTag("Arrowz"))
            {

                Vector3 temp = new Vector3(cur_obj.transform.position.x, cur_obj.transform.position.y, cur_obj.transform.position.z - Input.GetAxis("Mouse Y") * xSpeed * 0.01f);
                cur_obj.transform.position = temp;

                selectedGameobject.GetComponent<Outline>().enabled = true;
                selectedGameobject.GetComponent<Arrowglow>().ToggleHighlight(true);

            }
            else if (selectedGameobject.CompareTag("center"))
            {
                Vector3 temp = new Vector3(cur_obj.transform.position.x - Input.GetAxis("Mouse X") * xSpeed * 0.01f, cur_obj.transform.position.y, cur_obj.transform.position.z - Input.GetAxis("Mouse Y") * xSpeed * 0.01f);
                cur_obj.transform.position = temp;

                // selectedGameobject.GetComponent<Outline>().enabled = true;
                //selectedGameobject.GetComponent<Arrowglow>().ToggleHighlight(true);

            }
        }

  
    }


    void initialize() 
    {

        if (selectedGameobject)
        {

        }
        if (cur_obj)
        {
            cur_obj.GetComponent<Outline>().enabled = false;
          //  cur_obj.GetComponent<Meshcontroller>().isselected = false;
            cur_obj.GetComponent<Axiscontroller>().Axis_disable();
            cur_obj.GetComponent<Highlight>().ishighlight = false;
        }
        obj_lock = false;
        cur_obj = null;
        selectedGameobject = null;

    }



}
