using UnityEngine;
using UnityEngine.AI;

public static class NavMeshHelper
{
    public static bool Validate(NavMeshAgent agent)
    {
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent reference is null. Make sure it's properly initialized.");
            return false;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on a NavMesh surface.");
            return false;
        }

        return true;
    }
}