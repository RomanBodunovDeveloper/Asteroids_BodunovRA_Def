using System.Collections.Generic;
using UnityEngine;

public class ObjectCreator : MonoBehaviour
{
    private GameObject inputReaderPrefab;
    private GameObject starshipPrefab;
    private GameObject enemySpawnerPrefab;
 
    private List<GameObject> asteroidPrefab;
    private List<GameObject> ufoPrefab;

    private GameObject inputReader;
    private GameObject starship;
    private GameObject asteroidSpawner;
    private GameObject ufoSpawner;

    [SerializeField] private bool noSpawnStarsipDbg;
    [SerializeField] private bool noSpawnAsteroidDbg;
    [SerializeField] private bool noSpawnUfoDbg;

    private void Awake()
    {
        PrepareResourses();
        inputReader = Object.Instantiate(inputReaderPrefab, Vector3.zero, Quaternion.identity);
        inputReader.name = "inputReader";
    }

    private void OnEnable()
    {
        EventBus.StartGame += CreateObjects;
    }

    private void OnDisable()
    {
        EventBus.StartGame -= CreateObjects;
    }

    private void CreateObjects()
    {
        if (!noSpawnStarsipDbg)
        {
            starship = Object.Instantiate(starshipPrefab, Vector3.zero, Quaternion.identity);
            starship.name = "PlayerStarship";
        }
        
        if (!noSpawnAsteroidDbg)
        {
            asteroidSpawner = Object.Instantiate(enemySpawnerPrefab, Vector3.zero, Quaternion.identity);
            asteroidSpawner.name = "AsteroidSpawner";
            asteroidSpawner.GetComponent<EnemySpawner>().Init(asteroidPrefab, 3, 10, 3, 10);
        }
        
        if (!noSpawnUfoDbg)
        {
            ufoSpawner = Object.Instantiate(enemySpawnerPrefab, Vector3.zero, Quaternion.identity);
            ufoSpawner.name = "UfoSpawner";
            ufoSpawner.GetComponent<EnemySpawner>().Init(ufoPrefab, 0, 1, 10, 30);
        }
    }
    private  void PrepareResourses()
    {
        starshipPrefab     = Resources.Load<GameObject>("Prefab/Starship/SimpleStarship");
        inputReaderPrefab  = Resources.Load<GameObject>("Prefab/Common/InputReader");
        enemySpawnerPrefab = Resources.Load<GameObject>("Prefab/Enemy/enemySpawner");

        asteroidPrefab = LoadPrefabList("Assets/Resources/Prefab/Enemy/Asteroid");
        ufoPrefab      = LoadPrefabList("Assets/Resources/Prefab/Enemy/UFO");  
    }

    private  List<GameObject> LoadPrefabList(string path)
    {
        List<GameObject> prefabList = new List<GameObject>();
        string[] prefabFiles = System.IO.Directory.GetFiles(path);

        for (int i = 0; i < prefabFiles.Length; i++)
        {
            if (prefabFiles[i].EndsWith(".prefab"))
            {
                string tempString = prefabFiles[i].Substring(17); //cutting "Assets/Resources/"
                tempString = tempString.Substring(0, tempString.Length - 7); //cutting ".prefab"
                tempString = tempString.Replace('\\', '/');
                prefabList.Add(Resources.Load<GameObject>(tempString));
            }
        }
        return prefabList;
    }
}
