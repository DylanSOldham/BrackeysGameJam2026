using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    public const int DUNGEON_SIZE = 5;

    const float ROOM_WIDTH = 14;
    const float ROOM_HEIGHT = 10;

    public GameObject player;
    public GameObject frogPrefab;
    public GameObject gameCamera;
    public GameObject roomPrefab;
    public Minimap minimap;
    public Tile doorTile;

    
    public class Room
    {
        public bool exists    = false;
        public bool doorLeft  = false;
        public bool doorRight = false;
        public bool doorUp    = false;
        public bool doorDown  = false;
        public GameObject obj = null;
        public List<GameObject> enemies = new List<GameObject>();
    }
    
    Room[,] rooms = new Room[DUNGEON_SIZE, DUNGEON_SIZE];
    Vector2Int activeRoom = new Vector2Int(DUNGEON_SIZE / 2, 0);

    void Start()
    {
        for (int i = 0; i < DUNGEON_SIZE; i++) {
            for (int j = 0; j < DUNGEON_SIZE; j++) {
                Room room = new Room();
                rooms[i, j] = room;
            }
        }

        rooms[activeRoom.x, activeRoom.y].exists = true;
        Vector2Int randomWalker = activeRoom;
        for (int i = 0; i < DUNGEON_SIZE; i++) {
            rooms[randomWalker.x, randomWalker.y].exists = true;
            if (randomWalker.y != 0) 
            {
                rooms[randomWalker.x, randomWalker.y - 1].doorUp = true;
                rooms[randomWalker.x, randomWalker.y].doorDown = true;
            }
            int numSteps = Random.Range(0, 6);
            for (int j = 0; j < numSteps; j++)
            {
                int step = Random.Range(0.0f, 1.0f) < 0.5 ? -1 : 1;
                rooms[randomWalker.x, randomWalker.y].exists = true;
                if (step == -1 && randomWalker.x - 1 >= 0)
                {
                    rooms[randomWalker.x, randomWalker.y].doorLeft = true;
                    rooms[randomWalker.x - 1, randomWalker.y].doorRight = true;
                    randomWalker.x -= 1;
                }
                if (step == 1 && randomWalker.x + 1 < DUNGEON_SIZE)
                {
                    rooms[randomWalker.x, randomWalker.y].doorRight = true;
                    rooms[randomWalker.x + 1, randomWalker.y].doorLeft = true;
                    randomWalker.x += 1;
                }
            }
            randomWalker.y += 1;
        }

        for (int i = 0; i < DUNGEON_SIZE; i++) {
            for (int j = 0; j < DUNGEON_SIZE; j++) {
                Room room = rooms[i, j];
                if (i == activeRoom.x && j == activeRoom.y)
                {
                    Debug.Log(room.exists);
                }
                if (room.exists)
                {
                    GameObject roomObject = Instantiate(roomPrefab, transform);
                    roomObject.SetActive(false);
                    roomObject.transform.position = new Vector3(ROOM_WIDTH * i, ROOM_HEIGHT * j, 0.0f);
                    room.obj = roomObject;
                    Tilemap tilemap = roomObject.GetComponentInChildren<Tilemap>();
                    if (room.doorRight)
                    {
                        tilemap.SetTile(new Vector3Int(6, 0, 0), null);
                        tilemap.SetTile(new Vector3Int(6, -1, 0), null);
                    }
                    if (room.doorLeft)
                    {
                        tilemap.SetTile(new Vector3Int(-7, 0, 0), null);
                        tilemap.SetTile(new Vector3Int(-7, -1, 0), null);
                    }
                    if (room.doorUp)
                    {
                        tilemap.SetTile(new Vector3Int(0, 4, 0), null);
                        tilemap.SetTile(new Vector3Int(-1, 4, 0), null);
                    }
                    if (room.doorDown)
                    {
                        tilemap.SetTile(new Vector3Int(0, -5, 0), null);
                        tilemap.SetTile(new Vector3Int(-1, -5, 0), null);
                    }

                    int numEnemies = Mathf.CeilToInt(Random.Range(0, 4));
                    for (int k = 0; k < numEnemies; k++)
                    {
                        GameObject enemy = Instantiate(frogPrefab, transform);
                        enemy.SetActive(false);
                        enemy.transform.position = new Vector3(
                            i * ROOM_WIDTH + Random.Range(-1.0f, 1.0f), 
                            j * ROOM_HEIGHT + Random.Range(-1.0f, 1.0f), 
                            0.0f
                            );
                        room.enemies.Add(enemy);
                    }
                }
            }
        }

        Room active = rooms[activeRoom.x, activeRoom.y];
        active.obj.SetActive(true);

        minimap.RoomEntered(activeRoom, active.doorUp, active.doorDown, active.doorLeft, active.doorRight);

        player.transform.position = new Vector3(activeRoom.x * ROOM_WIDTH, activeRoom.y * ROOM_HEIGHT, 0.0f);
        gameCamera.transform.position = new Vector3(activeRoom.x * ROOM_WIDTH, activeRoom.y * ROOM_HEIGHT, gameCamera.transform.position.z);
    }

    void Update()
    {
        for (int i = 0; i < rooms[activeRoom.x, activeRoom.y].enemies.Count; ++i)
        {
            if (rooms[activeRoom.x, activeRoom.y].enemies[i] == null)
            {
                rooms[activeRoom.x, activeRoom.y].enemies.Remove(rooms[activeRoom.x, activeRoom.y].enemies[i]);
            }
        }

        Vector2Int oldActiveRoom = activeRoom;
        activeRoom.x = Mathf.FloorToInt(player.transform.position.x / ROOM_WIDTH + 0.5f);
        activeRoom.y = (int)(player.transform.position.y / ROOM_HEIGHT + 0.5);
        if (oldActiveRoom != activeRoom)
        {
            Room oldRoom = rooms[oldActiveRoom.x, oldActiveRoom.y];
            oldRoom.obj.SetActive(false);
            foreach (GameObject enemy in oldRoom.enemies) { 
                enemy.SetActive(false);
            }
            Room newRoom = rooms[activeRoom.x, activeRoom.y];

            if (!newRoom.exists)
            {
                Debug.LogError("Entered Invalid Room");
            }

            newRoom.obj.SetActive(true);
            minimap.RoomEntered(activeRoom, newRoom.doorUp, newRoom.doorDown, newRoom.doorLeft, newRoom.doorRight);
            foreach (GameObject enemy in newRoom.enemies) {
                enemy.SetActive(true);
            }
        }
        Vector3 cameraTarget = new Vector3(activeRoom.x * ROOM_WIDTH, activeRoom.y * ROOM_HEIGHT, gameCamera.transform.position.z);
        gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, cameraTarget, 0.05f);
    }
}
