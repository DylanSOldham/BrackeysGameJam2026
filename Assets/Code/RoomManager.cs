using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    const int DUNGEON_SIZE = 5;

    const float ROOM_WIDTH = 14;
    const float ROOM_HEIGHT = 10;

    public GameObject roomPrefab;

    private class Room
    {
        public bool exists    = false;
        public bool doorLeft  = false;
        public bool doorRight = false;
        public bool doorUp    = false;
        public bool doorDown  = false;
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
        rooms[activeRoom.x + 1, activeRoom.y].exists = true;
        rooms[activeRoom.x + 1, activeRoom.y + 1].exists = true;

        for (int i = 0; i < DUNGEON_SIZE; i++) {
            for (int j = 0; j < DUNGEON_SIZE; j++) {
                Room room = rooms[i, j];
                if (room.exists)
                {
                    GameObject roomObject = Instantiate(roomPrefab);
                    roomObject.transform.position = new Vector3(ROOM_WIDTH * (i - DUNGEON_SIZE / 2.0f + 0.5f), ROOM_HEIGHT * j, 0.0f);
                }
            }
        }
    }

    void Update()
    {
        
    }
}
