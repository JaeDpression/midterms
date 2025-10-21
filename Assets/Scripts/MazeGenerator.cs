using UnityEngine;

public class MazeGenerator3D : MonoBehaviour
{

    public GameObject largeWallPrefab;
    public GameObject mediumWallPrefab;
    public GameObject smallWallPrefab;
    public GameObject startLinePrefab;
    public GameObject finishLinePrefab;
    public GameObject playerPrefab;


    public float spawnRange = 50f;
    public int largeWallCount = 3;
    public int mediumWallCount = 6;
    public int smallWallCount = 10;

    private Vector3 startPosition;
    private Vector3 finishPosition;

    void Start()
    {
        GeneratePlane();
        GenerateMaze();
        GenerateStartAndFinish();
        SpawnPlayerAtStart();
    }

    void GeneratePlane()
    {
        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(10f, 1f, 10f);
        plane.transform.position = Vector3.zero;
    }

    void GenerateMaze()
    {
        for (int i = 0; i < largeWallCount; i++)
            SpawnWall(largeWallPrefab, new Vector3(50, 3, 1));
        for (int i = 0; i < mediumWallCount; i++)
            SpawnWall(mediumWallPrefab, new Vector3(30, 3, 1));
        for (int i = 0; i < smallWallCount; i++)
            SpawnWall(smallWallPrefab, new Vector3(10, 3, 1));
    }

    void SpawnWall(GameObject prefab, Vector3 size)
    {
        Vector3 pos = new Vector3(Random.Range(-spawnRange, spawnRange), 1.5f, Random.Range(-spawnRange, spawnRange));
        GameObject wall = Instantiate(prefab, pos, Quaternion.Euler(0, Random.Range(0, 360), 0));
        wall.transform.localScale = size;
    }

    void GenerateStartAndFinish()
    {
        startPosition = new Vector3(-spawnRange + 5, 0.1f, -spawnRange + 5);
        finishPosition = new Vector3(spawnRange - 5, 0.1f, spawnRange - 5);

        GameObject start = Instantiate(startLinePrefab, startPosition, Quaternion.identity);
        start.name = "StartLine";
        start.tag = "Start";

        GameObject finish = Instantiate(finishLinePrefab, finishPosition, Quaternion.identity);
        finish.name = "FinishLine";
        finish.tag = "Finish";

        Debug.Log("Start Line created at: " + startPosition);
        Debug.Log("Finish Line created at: " + finishPosition);
    }

    void SpawnPlayerAtStart()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab is NOT assigned in the inspector!");
            return;
        }

        GameObject startObj = GameObject.FindGameObjectWithTag("Start");
        if (startObj == null)
        {
            Debug.LogError("Start Line not found! Make sure it's tagged as 'Start'.");
            return;
        }

        Vector3 playerSpawn = startObj.transform.position + new Vector3(0, 2f, 0);
        GameObject player = Instantiate(playerPrefab, playerSpawn, Quaternion.identity);
        Debug.Log("Spawned Player at: " + playerSpawn);

        GameObject finishObj = GameObject.FindGameObjectWithTag("Finish");
        if (finishObj != null)
        {
            Vector3 lookDir = (finishObj.transform.position - startObj.transform.position).normalized;
            lookDir.y = 0;
            player.transform.forward = lookDir;
        }

        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        if (cam != null)
        {
            cam.target = player.transform;
        }
    }
}
