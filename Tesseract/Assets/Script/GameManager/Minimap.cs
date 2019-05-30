using UnityEngine;
using UnityEngine.Tilemaps;

public class Minimap : MonoBehaviour
{

    public Tilemap Texture;
    public Tilemap Collider;
    public Tilemap Background;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Tile t = ScriptableObject.CreateInstance<Tile>();
    }
}
