using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TechNodeUI : MonoBehaviour
{
    public Button button;
    public TMP_Text text;
    public TechNode techNode;
    private SimpleDraggableUI draggableUI;
    private void Awake()
    {
        draggableUI = GetComponent<SimpleDraggableUI>();
    }
    private void Start()
    {
        text.text = techNode.techName;
        button.interactable = ArePrerequisitesMet();
        button.onClick.AddListener(ResearchTech);
    }

    private bool ArePrerequisitesMet()
    {
        foreach (var prereq in techNode.prerequisites)
        {
            if (!prereq.isResearched)
            {
                return false;
            }
        }
        return true;
    }

    private void ResearchTech()
    {
        //print(draggableUI.IsDragging);
        if (draggableUI.IsDragging) // Do not proceed if currently dragging
            return;
        if (techNode.isResearched)
        {
            Debug.LogWarning(techNode.techName + " already researched!");
            return;
        }

        if (ArePrerequisitesMet())
        {
            techNode.isResearched = true;
            text.color = Color.green; // Change color to indicate researched status
            Debug.Log(techNode.techName + " researched!");
        }
        else
        {
            Debug.LogWarning("Prerequisites not met for: " + techNode.techName);
        }
    }
}
