using UnityEngine;

namespace Script.GlobalsScript
{
    public class StaticData
    {
        public static string PlayerChoice;

        public static PlayerData actualData;

        public static int NumberFloor;
        public static int ActualFloor = 0;

        public static int[] LevelMap = new int[2];

        public static int RandomLevel()
        {
            return Random.Range(LevelMap[0], LevelMap[1]);
        }
        
        public static int Seed;
    }
}
