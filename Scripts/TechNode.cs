using System.Collections.Generic;

[System.Serializable]
public class TechNode
{
    public string techName;
    public bool isResearched = false;
    public List<TechNode> prerequisites;
}
