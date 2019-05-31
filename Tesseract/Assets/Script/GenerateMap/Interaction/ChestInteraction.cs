using System.Collections;
using System.Collections.Generic;
using Script.GlobalsScript.Struct;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    private ChestData _chestData;
    private bool _canOpen;
    public Transform Potion;
    
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
            Transform o = Instantiate(Potion, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
            o.GetComponent<PotionManager>().Create(_chestData.Item as Potions);
            
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
