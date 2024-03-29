﻿using UnityEngine;
using Random = UnityEngine.Random;

public class RoomInstance : MonoBehaviour
{
    public Transform Wall;
    public Transform SimpleDeco;
    public Transform Chest;
    public Transform Portal;
    public Transform Pikes;
    
    public ChestData[] ChestDatas;
    public GamesItem[] GamesItems;
    public PikesData[] PikesDatas;
    public PortalData[] PortalDatas;
    public SimpleDecoration[] SimpleDecoration;
    
    private MapGridCreation script;

    private RoomData _roomData;
    private int _prob;

    public void Create(RoomData roomData, int prob)
    {
        _roomData = roomData;
        _prob = prob;
        script = transform.parent.GetComponent<MapGridCreation>();
    }

    public void BigWall()
    {
        int x = Random.Range(_roomData.X1 + 1, _roomData.X1 + _roomData.Width/2);
        int y = Random.Range(_roomData.Y1 + 1, _roomData.Y1 + _roomData.Height/2);

        int width = Random.Range(x + 2, _roomData.X2) - x;
        int height = Random.Range(y + 2, _roomData.Y2) - y;
        
        for (int i = x; i < x + width; i++)
        {
            for (int j = y; j < y + height; j++)
            {
                if (Random.Range(0, _prob + 1) == 0) continue;
                
                script.AddToGrid(j, i, false);
                _roomData.ModifyGrid(j - y, i - x, Wall);
            }
        }
    }

    public void AddSimpleDecoration(int number)
    {
        int len = SimpleDecoration.Length;
        for (int i = 0; i < number; i++)
        {
            int x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
            int y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);

            if (!script.Instances[y, x])
            {
                if (!script.Instances[y + 1, x] ||
                    !script.Instances[y + 1, x] ||
                    !script.Instances[y, x + 1] ||
                    !script.Instances[y, x + 1])
                {
                    _roomData.ModifyGrid(y - _roomData.Y1, x - _roomData.X1 , InstantiateDeco(x, y, SimpleDecoration[Random.Range(0, len)]));
                }
            }
            else
            {
                i--;
            }
        }
    }
    
    private Transform InstantiateDeco(int x, int y, SimpleDecoration deco)
    {
        MapGridCreation script = transform.parent.GetComponent<MapGridCreation>();
        Transform o = Instantiate(SimpleDeco, new Vector3(x, y, 0), Quaternion.identity, transform);
        o.GetComponent<SimpleDeco>().Create(deco);

        script.AddToInstance(y, x, true, !deco.AsCol);

        return o;
    }

    public void AddChest()
    {

        int x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
        int y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);

        if (!script.Instances[y, x] && !script.Instances[y + 1, x] && !script.Instances[y - 1, x])
        {
            Transform o = Instantiate(Chest, new Vector3(x, y, 0), Quaternion.identity, transform);
            ChestData chest = ScriptableObject.CreateInstance<ChestData>();
            ChestData chestref = ChestDatas[Random.Range(0, ChestDatas.Length)];
            
            chest.Create(chestref);
            chest.Item = GamesItems[Random.Range(0, GamesItems.Length)];
            o.GetComponent<Chest>().Create(chest);
            
            script.AddToInstance(y, x, true, false);
            _roomData.ModifyGrid(y - _roomData.Y1, x - _roomData.X1 , o);
        }
    }

    public void AddPortal(Vector3 pos)
    {
        int x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
        int y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);
        
        if (!script.Instances[y, x])
        {
            Transform o = Instantiate(Portal, new Vector3(x, y, 0), Quaternion.identity, transform);
            o.GetComponent<Portal>().Create(PortalDatas[0], pos);

            script.AddToInstance(y, x, true, true);
            _roomData.ModifyGrid(y - _roomData.Y1, x - _roomData.X1 , o);
        }
    }

    public bool AddBossPortal()
    {
        bool canSpawn = true;
        
        int x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
        int y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);

        for (int k = 0; k < 3; k++)
        {
            if (y - k < 3 || y + k > script.MapHeight - 3 || 
                script.Instances[y + k, x] || !script._grid[y + k, x] ||
                script.Instances[y - k, x] || !script._grid[y - k, x])
            {
                canSpawn = false;
                break;
            }
        }

        if (!canSpawn) return false;
        
        Transform o = Instantiate(Portal, new Vector3(x, y, 0), Quaternion.identity, transform);
        o.GetComponent<Portal>().Create(PortalDatas[1], new Vector3());
        
        script.AddToInstance(y, x, true, true);
        _roomData.ModifyGrid(y - _roomData.Y1, x - _roomData.X1 , o);

        return true;
    }

    public void AddPikes()
    {
        for (int i = 0; i < 6; i++)
        {
            int x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
            int y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);
        
            if (!script.Instances[y, x])
            {
                Transform o = Instantiate(Pikes, new Vector3(x, y, 0), Quaternion.identity, transform);
                o.GetComponent<Pikes>().Create(PikesDatas[Random.Range(0, PikesDatas.Length)]);
                
                script.AddToInstance(y, x, true, false);
                _roomData.ModifyGrid(y - _roomData.Y1, x - _roomData.X1 , o);
            } 
        }
    }
    public Vector3 GetFreePos()
    {
        int maxTry = 0;
        int x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
        int y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);
        
        while (maxTry < 10)
        {
            bool canSpawn = true;
            
            for (int k = 0; k < 3; k++)
            {
                if (y - k < 3 || y + k > script.MapHeight || 
                    script.Instances[y + k, x] || !script._grid[y + k, x] ||
                    script.Instances[y - k, x] || !script._grid[y - k, x])
                {
                    canSpawn = false;
                    break;
                }
            }
            
            if (canSpawn)
            {
                return new Vector3(x, y, 0);
            }
            
            x = _roomData.X1 + Random.Range(1, _roomData.Width - 2);
            y = _roomData.Y1 + Random.Range(1, _roomData.Height - 2);
            
            maxTry++;
        }

        return Vector3.zero;
    }
}
