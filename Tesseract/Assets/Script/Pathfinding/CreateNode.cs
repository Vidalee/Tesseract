using UnityEngine;

namespace Script.Pathfinding
{
    public class CreateNode : MonoBehaviour
    {    
        // Start is called before the first frame update
        void Awake()
        {
            AllNodes.AddNode(transform.position);
        }
    }
}
