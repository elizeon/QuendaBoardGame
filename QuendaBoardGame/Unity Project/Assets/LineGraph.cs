using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Line Graph Unity class
 * Written by Elizabeth Haynes (32670805)
 * Values are expected to be within 0 and 1 to display correctly.
 * */
public class LineGraph : MonoBehaviour

{
    public List<float> values { get { return m_values; } set { m_values = value; } }
    private List<float> m_values = new List<float>();
    private float m_graphWidth;
    private float m_graphHeight;
    [SerializeField]
    private float m_zPos;
    [SerializeField]
    private GameObject m_graphNode;
    [SerializeField]
    private Material m_lineMaterial;

    GameObject m_lineRenderer;
    List<GameObject> nodes = new List<GameObject>();
    GameObject m_graphParent;
    // Use this for initialization
    void Start ()
    {
        m_graphParent = new GameObject();
        m_graphParent.AddComponent<Canvas>();
        m_graphParent.transform.SetParent(this.transform);
    }

    public void Draw(Camera cam)
    {
        Start();
        m_graphWidth = GetComponent<RectTransform>().rect.width / 2f;
        m_graphHeight = GetComponent<RectTransform>().rect.height / 2f;

        for (int i=0;i<nodes.Count;i++)
        {
            Destroy(nodes[i]);
        }

        Destroy(m_lineRenderer);
        m_lineRenderer = new GameObject();
        m_lineRenderer.transform.SetParent(m_graphParent.transform, false);
        LineRenderer line = m_lineRenderer.AddComponent<LineRenderer>();


        nodes = new List<GameObject>();

        line.numPositions = values.Count;

        for (int i = 0; i < values.Count; i++)
        {

            Vector3 nodePosAtCentredAnchor = new Vector3((m_graphWidth / values.Count) * i * 2, (m_graphHeight / (1 / values[i])),m_zPos);
            Vector3 nodePosFinal = new Vector3(nodePosAtCentredAnchor.x * 2 - m_graphWidth, nodePosAtCentredAnchor.y * 2 - m_graphHeight, nodePosAtCentredAnchor.z);
            //nodes.Add(Instantiate(m_graphNode));

            //nodes[i].GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            //nodes[i].GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);

            //nodes[i].GetComponent<RectTransform>().anchoredPosition = nodePosFinal;
            //nodes[i].transform.SetParent(m_graphParent.transform,true);

            //nodes[i].transform.localScale = new Vector3(1, 1, 1);
            line.startWidth = 0.02f;
            line.endWidth = 0.02f;
            line.material = m_lineMaterial;

            Vector3 linePos = new Vector3(nodePosAtCentredAnchor.x / m_graphWidth, nodePosAtCentredAnchor.y / m_graphHeight, 1);

            linePos = new Vector3(linePos.x / 3 +0.3f, linePos.y / 3+0.6f, linePos.z);
            line.SetPosition(i, cam.ViewportToWorldPoint(linePos));


        }

        
        //m_graphParent.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        //LineRenderer;

    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
