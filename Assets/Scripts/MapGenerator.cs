using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject[] Obstacle;
    [SerializeField]
    private GameObject RampPrefab;
    [SerializeField]
    private GameObject[] coinPrefab;
    [SerializeField]
    private GameObject[] bonus;
    [SerializeField]
    private GameObject bossStart;
    int itemSpace = 15;
    int itemCountInMap = 5;
    public float laneOffset = 1.2f;
    int coinsCountInItem = 10;
    float coinsHeight = 0.5f;
    int mapSize;

 
    enum TrackPos {Left = -1, Center = 0, Right = 1};
    enum CoinsStyle { Line, Jump, Ramp};
    public GameObject[] skins;

    struct MapItem
    {
        public void SetValues(GameObject obstacle, TrackPos trackPos, CoinsStyle coinsStyle)
        {
            this.obstacle = obstacle;
            this.trackPos = trackPos;
            this.coinsStyle = coinsStyle;
        }
        public GameObject obstacle;
        public TrackPos trackPos;
        public CoinsStyle coinsStyle;
    }
    struct BonusItem
    {
        public void SetValues(GameObject bonus, TrackPos posBonus)
        {
            this.bonus = bonus;
            this.posBonus = posBonus;

        }
        public GameObject bonus;
        public TrackPos posBonus;
    }

    struct BoxBoss
    {
        public void SetValuse(GameObject boxStartBoss, TrackPos posBoxBoss)
        {
            this.boxStartBoss = boxStartBoss;
            this.posBoxBoss = posBoxBoss;
        }
        public GameObject boxStartBoss;
        public TrackPos posBoxBoss;
    }

    static public MapGenerator instance;

    private void Awake()
    {
       
        instance = this;
        mapSize = itemCountInMap * itemSpace;
       
        maps.Add(MakeMap1());
        maps.Add(MakeMap2());
        maps.Add(MakeMap1());
        maps.Add(MakeMap3());
        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }
    }
   
    public List<GameObject> maps = new List<GameObject>(); 
    public List<GameObject> activeMaps = new List<GameObject>(); 

    private void Update()
    {

        if (GeneratorRoad.instance.speed == 0)
        {
            return;
        }
        foreach(GameObject map in activeMaps)
        {
            map.transform.position -= new Vector3(0, 0, GeneratorRoad.instance.speed * Time.deltaTime);
        }
        if (activeMaps[0].transform.position.z < -mapSize)
        {
            RemoveFirstActiveMap();
            AddActiveMap();
        }
    }


    void RemoveFirstActiveMap()
    {
        activeMaps[0].SetActive(false);
        maps.Add(activeMaps[0]);
        activeMaps.RemoveAt(0);
    }

    void AddActiveMap()
    {     
        //int r = Random.Range(0, maps.Count);
        for (int i = 0; i < maps.Count; i++) {
            GameObject go = maps[i];
            go.SetActive(true);
            foreach (Transform child in go.transform)
            {
                child.gameObject.SetActive(true);
            }
            go.transform.position = activeMaps.Count > 0 ? activeMaps[activeMaps.Count - 1].transform.position + Vector3.forward * mapSize :
                                                     new Vector3(0, 0, 10);
            maps.RemoveAt(i);
            activeMaps.Add(go); 
        }
    }

    public void ResetMaps()
    {
        while (activeMaps.Count > 0)
        {
            RemoveFirstActiveMap();
        }
        AddActiveMap();
        AddActiveMap();
    }


    GameObject MakeMap1()
    {
    
        GameObject result = new GameObject("Map1");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        for(int i = 0; i < itemCountInMap; i++)
        {
            item.SetValues(null, TrackPos.Center, CoinsStyle.Line);
            if (i == 2)
            {
                item.SetValues(Obstacle[1], TrackPos.Left, CoinsStyle.Jump);
            }
            if (i == 3)
            {
                item.SetValues(Obstacle[3], TrackPos.Right, CoinsStyle.Jump);
            }
            if (i == 4)
            {
                item.SetValues(Obstacle[0], TrackPos.Center, CoinsStyle.Jump);
            }
            else if (i == 5)
            {
                item.SetValues(Obstacle[0], TrackPos.Right, CoinsStyle.Jump);
            }
            Vector3 obstaclePos = new Vector3((int)item.trackPos * laneOffset, 0, i * itemSpace);
            CreateCoins(item.coinsStyle, obstaclePos, result);
            if(item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                go.transform.SetParent(result.transform);
                
            }
        }
        return result;
    }

    GameObject MakeMap3()
    {

        GameObject result = new GameObject("Map3");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        BoxBoss boss = new BoxBoss();
        for (int i = 0; i < itemCountInMap; i++)
        {
            item.SetValues(null, TrackPos.Center, CoinsStyle.Line);
            if (i == 2)
            {
                item.SetValues(Obstacle[1], TrackPos.Left, CoinsStyle.Jump);
            }
            if (i == 3)
            {
                item.SetValues(Obstacle[3], TrackPos.Right, CoinsStyle.Jump);
            }
            if (i == 4)
            {
                item.SetValues(Obstacle[0], TrackPos.Center, CoinsStyle.Jump);
            }
            else if (i == 5)
            {
                item.SetValues(Obstacle[0], TrackPos.Right, CoinsStyle.Jump);
            }
            Vector3 obstaclePos = new Vector3((int)item.trackPos * laneOffset, 0, i * itemSpace);
            Vector3 boxStartBoss = new Vector3((int)boss.posBoxBoss * laneOffset, 0, 10);
            CreateCoins(item.coinsStyle, obstaclePos, result);
            CreateStartBoss(boxStartBoss, result);
            if (boss.boxStartBoss != null)
            {
                GameObject go = Instantiate(boss.boxStartBoss, boxStartBoss, Quaternion.identity);
                go.transform.SetParent(result.transform);
            }
            if (item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                go.transform.SetParent(result.transform);
            }
        }
        return result;
    }
    GameObject MakeMap2()
    {
     
        GameObject result = new GameObject("Map2");
        result.transform.SetParent(transform);
        MapItem item = new MapItem();
        BonusItem item2 = new BonusItem();   
        for (int i = 0; i < itemCountInMap; i++)
        {        
            item.SetValues(null, TrackPos.Center, CoinsStyle.Line);
            if (i == 2)
            {
                item.SetValues(Obstacle[1], TrackPos.Left, CoinsStyle.Jump);
            }
            if (i == 3)
            {
                item.SetValues(Obstacle[3], TrackPos.Right, CoinsStyle.Jump);
            }
            if (i == 4)
            {
                item.SetValues(Obstacle[0], TrackPos.Center, CoinsStyle.Jump);
            }
            else if (i == 5)
            {
                item.SetValues(Obstacle[0], TrackPos.Right, CoinsStyle.Jump);
            }

            Vector3 obstaclePos = new Vector3((int)item.trackPos * laneOffset, 0, i * itemSpace);
            CreateCoins(item.coinsStyle, obstaclePos, result);
            item2.SetValues(null, TrackPos.Center);
            if (item.obstacle != null)
            {
                GameObject go = Instantiate(item.obstacle, obstaclePos, Quaternion.identity);
                go.transform.SetParent(result.transform);
            }
            Vector3 bonusPos = new Vector3((int)item2.posBonus * laneOffset, 0, 30);
            CreateBonus(bonusPos, result);
            if (item2.bonus != null)
            {
                GameObject go = Instantiate(item2.bonus, bonusPos, Quaternion.identity);
                go.transform.SetParent(result.transform);
            }
        }
        return result;
    }

    void CreateBonus(Vector3 pos, GameObject parent)
    {
      
        Vector3 bonusPos = Vector3.zero;
        bonusPos.y = 0.5f;
        bonusPos.z = 1.5f;
        GameObject go = Instantiate(bonus[0], bonusPos + pos, Quaternion.identity);
        go.transform.SetParent(parent.transform);
    }

    void CreateStartBoss(Vector3 pos, GameObject parent)
    {
        Vector3 pointBoss = Vector3.zero;
        pointBoss.y = 1.11f;
        pointBoss.z = 1.5f;
        GameObject go = Instantiate(bossStart, pointBoss + pos, Quaternion.identity);
        go.transform.SetParent(parent.transform);
    }
    void CreateCoins(CoinsStyle style, Vector3 pos, GameObject parentObject)
    {
        Vector3 coinPos = Vector3.zero;
        if(style == CoinsStyle.Line)
        {
            for(int i = -coinsCountInItem / 2; i < coinsCountInItem / 2; i++)
            {

                coinPos.y = coinsHeight;
                coinPos.z = i * ((float)itemSpace / coinsCountInItem);
                GameObject go = Instantiate(coinPrefab[Random.Range(0,3)], coinPos + pos, Quaternion.identity);
                go.transform.SetParent(parentObject.transform);
            }
        }else if(style == CoinsStyle.Jump)
        {
            for (int i = -coinsCountInItem / 2; i < coinsCountInItem / 2; i++)
            {
                coinPos.y = Mathf.Max(-1 / 2f * Mathf.Pow(i, 2) + 3,coinsHeight);
                coinPos.z = i * ((float)itemSpace / coinsCountInItem);
                GameObject go = Instantiate(coinPrefab[Random.Range(0, 3)], coinPos + pos, Quaternion.identity);
                go.transform.SetParent(parentObject.transform);
            }
        }
    }
}
