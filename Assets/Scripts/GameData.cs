using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//게임정보에 대한 접근이 쉽도록 GameData에 List나 Dictionary를 선언해 줍니다.

[SerializeField] 
public enum Dungeons
{
    Wood,
    Abyss,
    Cellar
}

[SerializeField]
public enum BuildingAllow
{
    BuildingAllowded,
    BuildingNotAllowded
}

[SerializeField]
public enum GameLayer
{
    Ground = 6,//땅
    Building = 7,//건물
    Road = 8,//길
    Dungeon = 9//던전
}

[SerializeField]
public enum BuildingType
{
    Shop,
    Environment,
    Dungeon
}

[SerializeField]
public enum BuildingName
{
    ArmorShop,
    WeaponShop,
    Hotel,
    Platform,
    Tree,
    Fence,
    FenceCurve
}


public class GameData : GenericSingleton<GameData>
{
    //public Dictionary<string, int> buildingDictionary; // X_Y로 몇번째 켜져있는지 받아옴

    public List<string> woodDungeonEnemy;
    public List<string> abyssDungeonEnemy;
    public List<string> cellarDungeonEnemy;

    public Dictionary<string, EnemyData> enemyDictionary;

    public Dictionary<string, Transform> npcTransformDictionary;
    public List<string> npcNameList; // 이름으로 데이터 받아옴
    public Dictionary<string, NpcData> npcDataDictionary;

    public Dictionary<int, WeaponData> weaponDataDictionary;
    public Dictionary<int, ArmorData> armorDataDictionary;

    //                 pos      name(weaponShop, armorShop, hotel, dungeon0, dungeon1, dungeon2, platform, tree)
    public Dictionary<string, string> buildingDataList;

    public int gameSpeed;
    public int money;
    public int frameRate;
    public float time;

    protected override void Awake()
    {
        base.Awake();

        woodDungeonEnemy = new List<string>();
        abyssDungeonEnemy = new List<string>();
        cellarDungeonEnemy = new List<string>();

        enemyDictionary = new Dictionary<string, EnemyData>();
        weaponDataDictionary = new Dictionary<int, WeaponData>();
        armorDataDictionary = new Dictionary<int, ArmorData>();

        npcTransformDictionary = new Dictionary<string, Transform>();
        npcNameList = new List<string>();
        npcDataDictionary = new Dictionary<string, NpcData>();

        buildingDataList = new Dictionary<string, string>();
    }
}
