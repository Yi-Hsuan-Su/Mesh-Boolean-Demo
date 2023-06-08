using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    public  Outline m_outline ;
    public bool ishighlight;
    public void Start()
    {
        m_outline = this.GetComponent<Outline>();
        m_outline.OutlineMode = Outline.Mode.OutlineAll;
        m_outline.OutlineColor = new Color(255, 98, 0);
        m_outline.OutlineWidth = 10f;
        m_outline.enabled = false;

        ishighlight = false;
    }

  
}