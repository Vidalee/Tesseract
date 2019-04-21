using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    private ChestData _chestData;
    private bool _canOpen;
    public GameEvent ItemEvent;

    public void Create(ChestData chestData)
    {
        _chestData = chestData;
        Initialise();
    }

    private void Initialise()
    {
        GetComponent<EdgeCollider2D>().points = _chestData.TriggerCol;
    }

    private void Update()
    {
        if (_canOpen && !_chestData.IsOpen && Input.GetKey("a"))
        {
            ItemEvent.Raise(new EventArgsItem(_chestData.Item));
            _chestData.IsOpen = true;
            transform.parent.GetComponent<Chest>().OpenChest();
        }    
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _canOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {       
        if (other.CompareTag("Player") && (other.transform.position - transform.position).magnitude > 0.8f)
        {
            _canOpen = false;
        }
    }
}
