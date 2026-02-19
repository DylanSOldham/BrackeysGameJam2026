using UnityEngine;
using UnityEngine.UI;
using static RoomManager;

public class Minimap : MonoBehaviour
{
    public GameObject minimapElementPrefab;
    public GameObject youAreHere;
    GameObject[,] roomElements = new GameObject[RoomManager.DUNGEON_SIZE, RoomManager.DUNGEON_SIZE];

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void RoomEntered(Vector2Int position, DoorState doorUp, DoorState doorDown, DoorState doorLeft, DoorState doorRight)
    {
        float posX = 90.0f * ((float)position.x / DUNGEON_SIZE - 0.5f) + 9.0f;
        float posY = 90.0f * ((float)position.y / DUNGEON_SIZE - 0.5f) + 9.0f;
        if (roomElements[position.x, position.y] == null)
        {
            GameObject element = Instantiate(minimapElementPrefab, transform);
            element.transform.localPosition = new Vector3(posX, posY, element.transform.position.z);
            element.transform.SetSiblingIndex(1);
            if (doorUp != DoorState.NotPresent)
            {
                element.transform.Find("DoorUp").gameObject.SetActive(true);
            }
            if (doorDown != DoorState.NotPresent)
            {
                element.transform.Find("DoorDown").gameObject.SetActive(true);
            }
            if (doorLeft != DoorState.NotPresent)
            {
                element.transform.Find("DoorLeft").gameObject.SetActive(true);
            }
            if (doorRight != DoorState.NotPresent)
            {
                element.transform.Find("DoorRight").gameObject.SetActive(true);
            }
            roomElements[position.x, position.y] = element;
        }

        youAreHere.transform.localPosition = new Vector3(posX, posY, roomElements[position.x, position.y].transform.position.z);
    }
}
