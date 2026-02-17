using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomManager : MonoBehaviour
{
    public const int DUNGEON_SIZE = 5;

    const float ROOM_WIDTH = 14;
    const float ROOM_HEIGHT = 10;

    public GameObject player;
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
                if (randomWalker.x + step >= 0 && randomWalker.x + step < DUNGEON_SIZE)
                {
                    randomWalker.x += step;
                    rooms[randomWalker.x, randomWalker.y].exists = true;
                    if (step == -1)
                    {
                        rooms[randomWalker.x, randomWalker.y].doorRight = true;
                        rooms[randomWalker.x + 1, randomWalker.y].doorLeft = true;
                    }
                    if (step == 1)
                    {
                        rooms[randomWalker.x, randomWalker.y].doorLeft = true;
                        rooms[randomWalker.x - 1, randomWalker.y].doorRight = true;
                    }
                }
            }
            randomWalker.y += 1;
        }

        for (int i = 0; i < DUNGEON_SIZE; i++) {
            for (int j = 0; j < DUNGEON_SIZE; j++) {
                Room room = rooms[i, j];
                if (room.exists)
                {
                    GameObject roomObject = Instantiate(roomPrefab);
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
                }
            }
        }

        Debug.Log(activeRoom);
        rooms[activeRoom.x, activeRoom.y].obj.SetActive(true);

        player.transform.position = new Vector3(activeRoom.x * ROOM_WIDTH, activeRoom.y * ROOM_HEIGHT, player.transform.position.z);
        gameCamera.transform.position = new Vector3(activeRoom.x * ROOM_WIDTH, activeRoom.y * ROOM_HEIGHT, gameCamera.transform.position.z);
        minimap.RevealRoom(activeRoom);
    }

    void Update()
    {
        Vector2Int oldActiveRoom = activeRoom;
        activeRoom.x = Mathf.FloorToInt(player.transform.position.x / ROOM_WIDTH + 0.5f);
        activeRoom.y = (int)(player.transform.position.y / ROOM_HEIGHT + 0.5);
        if (oldActiveRoom != activeRoom)
        {
            GameObject oldRoom = rooms[oldActiveRoom.x, oldActiveRoom.y].obj;
            if (oldRoom != null) oldRoom.SetActive(false);
            GameObject newRoom = rooms[activeRoom.x, activeRoom.y].obj;
            if (newRoom != null) newRoom.SetActive(true);

            minimap.RevealRoom(activeRoom);
        }
        Vector3 cameraTarget = new Vector3(activeRoom.x * ROOM_WIDTH, activeRoom.y * ROOM_HEIGHT, gameCamera.transform.position.z);
        gameCamera.transform.position = Vector3.Lerp(gameCamera.transform.position, cameraTarget, 0.05f);
    }
}
