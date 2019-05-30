using UnityEngine;

namespace Script.GlobalsScript
{
    public class StaticData
    {
        public static string PlayerChoice;

        public static PlayerData actualData;

        public static int NumberFloor = 2;
        public static int ActualFloor;

        public static int[] LevelMap;

        public static int RandomLevel()
        {
            return Random.Range(LevelMap[0], LevelMap[1]);
        }
        public static int Seed;
    }
}
