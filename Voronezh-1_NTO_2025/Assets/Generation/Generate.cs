using UnityEngine;
using System.IO;

public class JsonReader : MonoBehaviour
{
    [SerializeField]
    private GameObject planePrefab;
    [SerializeField]
    private Transform parentBuilds;
    [SerializeField]
    private float planeSize;

    [System.Serializable]
    public class Center
    {
        public float x;
        public float y;
    }

    [System.Serializable]
    public class Crossroad
    {
        public int id;
        public Center center;
        public bool up;
        public bool down;
        public bool left;
        public bool right;
        public int trafficLight1;
        public int trafficLight2;
    }

    [System.Serializable]
    public class Block
    {
        public Center left;
        public Center right;
    }

    [System.Serializable]
    public class MapData
    {
        public Crossroad[] crossroads;
        public Block[] blocks;
    }

    public string jsonFilePath;

    void Start()
    {
        string json = File.ReadAllText(jsonFilePath);
        
        MapData mapData = JsonUtility.FromJson<MapData>(json);
        
        foreach (var crossroad in mapData.crossroads)
            Debug.Log($"Crossroad ID: {crossroad.id}, Center: ({crossroad.center.x}, {crossroad.center.y})");

        foreach (var block in mapData.blocks)
            GenerateBuildingPerimeter(block.right.x, block.right.y, block.left.x, block.left.y);
    }

    void GenerateBuildingPerimeter(float topRightX, float topRightY, float bottomLeftX, float bottomLeftY)
    {
        int widthCount = Mathf.CeilToInt((topRightX - bottomLeftX) / planeSize);
        int heightCount = Mathf.CeilToInt((topRightY - bottomLeftY) / planeSize);

        for (float z = Random.Range(3, 5); z > 0.5f; z--) 
        {
            // Bottom edge
            for (int x = 0; x < widthCount; x++)
            {
                Vector3 position = new Vector3(bottomLeftX + (x * planeSize) + (planeSize / 2), z, bottomLeftY);
                Instantiate(planePrefab, position, Quaternion.Euler(90.0f, 180.0f, 0.0f), parentBuilds).transform.name = "Bottom edge";
            }

            // Top edge
            for (int x = 0; x < widthCount; x++)
            {
                Vector3 position = new Vector3(bottomLeftX + (x * planeSize) + (planeSize / 2), z, topRightY);
                Instantiate(planePrefab, position, Quaternion.Euler(90.0f, 0.0f, 0.0f), parentBuilds).transform.name = "Top edge";
            }

            // Left edge
            for (int y = 0; y < heightCount; z++)
            {
                Vector3 position = new Vector3(bottomLeftX - (planeSize / 2) + (planeSize / 2),  z, bottomLeftY + (z * planeSize) + (planeSize / 2));
                Instantiate(planePrefab, position, Quaternion.Euler(90.0f, -90.0f, 0.0f), parentBuilds).transform.name = "Left edge";
            }

            // Right edge
            for (int y = 0; y < heightCount; z++)
            {
                Vector3 position = new Vector3(topRightX, z, bottomLeftY + (z * planeSize) + (planeSize / 2));
                Instantiate(planePrefab, position, Quaternion.Euler(90.0f, 90.0f, 0.0f), parentBuilds).transform.name = "Right edge";
            }
        }
    }
}

