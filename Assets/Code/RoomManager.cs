using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    public const int DUNGEON_SIZE = 5;

    const float ROOM_WIDTH = 14;
    const float ROOM_HEIGHT = 10;

    public GameObject player;
    public GameObject snake;
    public GameObject frogPrefab;
    public GameObject gameCamera;
    public GameObject roomPrefab;
    public Minimap minimap;
    public Tile wallTile;

    
    public class Room
    {
        public bool exists    = false;
        public bool isSnakeRoom = false;
        public DoorState doorLeft  = DoorState.NotPresent;
        public DoorState doorRight = DoorState.NotPresent;
        public DoorState doorUp    = DoorState.NotPresent;
        public DoorState doorDown  = DoorState.NotPresent;
        public GameObject obj = null;
        public List<GameObject> enemies = new List<GameObject>();
    }
    
    public enum DoorState
    {
        NotPresent,
        Locked,
        Open,
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

        // Generate the dungeon layout - decide which rooms actually exist and how they connect
        int snakeLevelEntry = 3;
        rooms[activeRoom.x, activeRoom.y].exists = true;
        Vector2Int randomWalker = activeRoom;
        for (int i = 0; i < DUNGEON_SIZE; i++)
        {
            if (i == DUNGEON_SIZE - 1)
            {
                snakeLevelEntry = randomWalker.x;
            }

            randomWalker.y = i;
            rooms[randomWalker.x, randomWalker.y].exists = true;
            if (randomWalker.y != 0) 
            {
                rooms[randomWalker.x, randomWalker.y - 1].doorUp = DoorState.Locked;
                rooms[randomWalker.x, randomWalker.y].doorDown = DoorState.Locked;
            }
            int numSteps = Random.Range(0, 6);
            for (int j = 0; j < numSteps; j++)
            {

                int step = Random.Range(0.0f, 1.0f) < 0.5 ? -1 : 1;
                if (step == -1 && randomWalker.x - 1 >= 0)
                {
                    rooms[randomWalker.x, randomWalker.y].doorLeft = DoorState.Locked;
                    rooms[randomWalker.x - 1, randomWalker.y].doorRight = DoorState.Locked;
                    randomWalker.x -= 1;
                }
                if (step == 1 && randomWalker.x + 1 < DUNGEON_SIZE)
                {
                    rooms[randomWalker.x, randomWalker.y].doorRight = DoorState.Locked;
                    rooms[randomWalker.x + 1, randomWalker.y].doorLeft = DoorState.Locked;
                    randomWalker.x += 1;
                }
                rooms[randomWalker.x, randomWalker.y].exists = true;
            }
        }

        // Decide the snake room as the furthest room from the entrance of the final y-level
        int maxIndex = snakeLevelEntry;
        int maxDist = 0;
        for (int i = 0; i < DUNGEON_SIZE; ++i) {
            int dist = System.Math.Abs(i - snakeLevelEntry);
            if (rooms[i, DUNGEON_SIZE - 1].exists && dist > maxDist)
            {
                maxDist = dist;
                maxIndex = i;
            }
        }
        rooms[maxIndex, DUNGEON_SIZE - 1].isSnakeRoom = true;
        snake.transform.position = new Vector3(maxIndex * ROOM_WIDTH, (DUNGEON_SIZE - 1) * ROOM_HEIGHT, 0.0f);
        activeRoom = new Vector2Int(maxIndex, (DUNGEON_SIZE - 1));

        // Instantiate the prefabs for each existent room
        for (int i = 0; i < DUNGEON_SIZE; i++) {
            for (int j = 0; j < DUNGEON_SIZE; j++) {
                Room room = rooms[i, j];
                if (room.exists)
                {
                    GameObject roomObject = Instantiate(roomPrefab, transform);
                    roomObject.SetActive(false);
                    roomObject.transform.position = new Vector3(ROOM_WIDTH * i, ROOM_HEIGHT * j, 0.0f);
                    room.obj = roomObject;

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

        OpenDoors();
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

        if (rooms[activeRoom.x, activeRoom.y].enemies.Count == 0)
        {
            OpenDoors();
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

            // Open the door you are entering through
            switch (activeRoom.x - oldActiveRoom.x, activeRoom.y - oldActiveRoom.y)
            {
                case (1, 0):
                    Debug.Log("Entering from the left!");
                    newRoom.doorLeft = DoorState.Open;
                    break;
                case (-1, 0):
                    newRoom.doorRight = DoorState.Open;
                    break;
                case (0, 1):
                    newRoom.doorDown = DoorState.Open;
                    break;
                case (0, -1):
                    newRoom.doorUp = DoorState.Open;
                    break;
            };
            RefreshDoors();


            newRoom.obj.SetActive(true);
            minimap.RoomEntered(activeRoom, newRoom.doorUp, newRoom.doorDown, newRoom.doorLeft, newRoom.doorRight);
            foreach (GameObject enemy in newRoom.enemies) {
                enemy.SetActive(true);
            }
        }
        Vector3 cameraTarget = new Vector3(activeRoom.x * ROOM_WIDTH, activeRoom.y * ROOM_HEIGHT, gameCamera.transform.position.z);
        gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, cameraTarget, 0.05f);
    }
    void printTheWholeMap()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < DUNGEON_SIZE; i++)
        {
            for (int j = 0; j < DUNGEON_SIZE; j++)
            {
                sb.Append(rooms[j, i].exists);
                sb.Append(", ");
            }
            sb.AppendLine();
        }
        Debug.Log(sb.ToString());
    }

    void OpenDoors()
    {
        Room room = rooms[activeRoom.x, activeRoom.y];
        if (room.doorRight == DoorState.Locked)
        {
            room.doorRight = DoorState.Open;
        }
        if (room.doorLeft == DoorState.Locked)
        {
            room.doorLeft = DoorState.Open;
        }
        if (room.doorUp == DoorState.Locked)
        {
            room.doorUp = DoorState.Open;
        }
        if (room.doorDown == DoorState.Locked)
        {
            room.doorDown = DoorState.Open;
        }
        RefreshDoors();
    }

    void RefreshDoors()
    {
        Room room = rooms[activeRoom.x, activeRoom.y];
        Tilemap tilemap = room.obj.GetComponentInChildren<Tilemap>();

        Tile upTile = room.doorUp == DoorState.Open ? null : wallTile;
        Tile downTile = room.doorDown == DoorState.Open ? null : wallTile;
        Tile leftTile = room.doorLeft == DoorState.Open ? null : wallTile;
        Tile rightTile = room.doorRight == DoorState.Open ? null : wallTile;

        tilemap.SetTile(new Vector3Int(6, 0, 0), rightTile);
        tilemap.SetTile(new Vector3Int(6, -1, 0), rightTile);

        tilemap.SetTile(new Vector3Int(-7, 0, 0), leftTile);
        tilemap.SetTile(new Vector3Int(-7, -1, 0), leftTile);

        tilemap.SetTile(new Vector3Int(0, 4, 0), upTile);
        tilemap.SetTile(new Vector3Int(-1, 4, 0), upTile);

        tilemap.SetTile(new Vector3Int(0, -5, 0), downTile);
        tilemap.SetTile(new Vector3Int(-1, -5, 0), downTile);
    }
}
