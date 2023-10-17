using UnityEngine;

[CreateAssetMenu(menuName = "Team Configuration")]
public class TeamConfiguration : ScriptableObject
{
    public enum Team
    {
        Team6,
        Team7
    }

    public Team team;
    public int layer;  // Set this in the inspector according to the layers you set up.
}
