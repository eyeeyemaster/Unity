using UnityEngine;
using System.Collections.Generic;

public class TechTreeManager : MonoBehaviour
{
    public GameObject techNodePrefab; // Your UI prefab for tech nodes
    public Transform techTreePanel;   // Your UI panel to parent the tech nodes
    public List<TechNode> techTree;
    public float horizontalSpacing = 150f; // Adjust as needed
    public float verticalSpacing = 100f;   // Adjust as needed
    public Material lineMaterial;
    private Dictionary<string, RectTransform> techNodeRects = new Dictionary<string, RectTransform>();

    private void Start()
    {
        TechNode masonry = new TechNode { techName = "Masonry", isResearched = false, prerequisites = new List<TechNode>() };
        TechNode archery = new TechNode { techName = "Archery", isResearched = false, prerequisites = new List<TechNode>() };
        TechNode advancedConstruction = new TechNode
        {
            techName = "Advanced Construction",
            isResearched = false,
            prerequisites = new List<TechNode> { masonry }
        };
        techTree = new List<TechNode> { masonry, archery, advancedConstruction };
        for (int i = 0; i < techTree.Count; i++)
        {
            // Instantiate UI node
            GameObject nodeObj = Instantiate(techNodePrefab, techTreePanel);
            // Position node in a grid layout for simplicity
            float xPosition = (i % 5) * horizontalSpacing; // For example, creates a new row every 5 nodes
            float yPosition = -(i / 5) * verticalSpacing;  // Negative because UI coordinates go down as they increase
            nodeObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);
            TechNodeUI nodeUI = nodeObj.GetComponent<TechNodeUI>();
            nodeUI.techNode = techTree[i];
            RectTransform rect = nodeObj.GetComponent<RectTransform>();
            techNodeRects[techTree[i].techName] = rect;
            foreach (TechNode prereq in techTree[i].prerequisites)
            {
                if (techNodeRects.ContainsKey(prereq.techName))
                {
                    DrawLineBetween(techNodeRects[prereq.techName], rect);
                }
            }
        }
    }
    private void DrawLineBetween(RectTransform start, RectTransform end)
    {
        //print("Drawing Lines");
        GameObject lineObj = new GameObject("Line");
        lineObj.transform.SetParent(techTreePanel, false);
        lineObj.transform.localPosition = new Vector3(0, 0, -5); // Adjust the Z value as needed.
        LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();

        lineRenderer.material = lineMaterial;
        
        lineRenderer.widthMultiplier = 4.0f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = false; // Important for UI
        lineRenderer.startColor = Color.black; // Or any color you prefer
        lineRenderer.endColor = Color.black;


        lineRenderer.SetPosition(0, start.localPosition);
        lineRenderer.SetPosition(1, end.localPosition);
    }

    public bool ResearchTech(string techName)
    {
        // Find the technology node.
        TechNode node = techTree.Find(tech => tech.techName == techName);

        if (node == null)
        {
            Debug.LogError("Tech not found: " + techName);
            return false;
        }

        // Check prerequisites are researched.
        foreach (TechNode prereq in node.prerequisites)
        {
            if (!prereq.isResearched)
            {
                Debug.LogError("Prerequisite not met: " + prereq.techName);
                return false;
            }
        }

        // Research the tech.
        node.isResearched = true;
        Debug.Log("Technology Researched: " + techName);
        return true;
    }
}
