using UnityEngine;
using UnityEngine.UI;

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

    public void RoomEntered(Vector2Int position, bool doorUp, bool doorDown, bool doorLeft, bool doorRight)
    {
        float posX = 90.0f * ((float)position.x / RoomManager.DUNGEON_SIZE - 0.5f) + 9.0f;
        float posY = 90.0f * ((float)position.y / RoomManager.DUNGEON_SIZE - 0.5f) + 9.0f;
        if (roomElements[position.x, position.y] == null)
        {
            GameObject element = Instantiate(minimapElementPrefab, transform);
            element.transform.localPosition = new Vector3(posX, posY, element.transform.position.z);
            element.transform.SetSiblingIndex(1);
            if (doorUp)
            {
                element.transform.Find("DoorUp").gameObject.SetActive(true);
            }
            if (doorDown)
            {
                element.transform.Find("DoorDown").gameObject.SetActive(true);
            }
            if (doorLeft)
            {
                element.transform.Find("DoorLeft").gameObject.SetActive(true);
            }
            if (doorRight)
            {
                element.transform.Find("DoorRight").gameObject.SetActive(true);
            }
            roomElements[position.x, position.y] = element;
        }

        youAreHere.transform.localPosition = new Vector3(posX, posY, roomElements[position.x, position.y].transform.position.z);
    }
}
