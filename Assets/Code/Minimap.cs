using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject minimapElementPrefab;
    GameObject[,] roomSprites = null;

    void Start()
    {
        roomSprites = new GameObject[RoomManager.DUNGEON_SIZE, RoomManager.DUNGEON_SIZE];
    }

    void Update()
    {
        
    }

    public void RevealRoom(Vector2Int position, bool doorUp, bool doorDown, bool doorLeft, bool doorRight)
    {
        GameObject element = Instantiate(minimapElementPrefab, transform);
        float posX = 90.0f * ((float)position.x / RoomManager.DUNGEON_SIZE - 0.5f) + 9.0f;
        float posY = 90.0f * ((float)position.y / RoomManager.DUNGEON_SIZE - 0.5f) + 9.0f;
        element.transform.localPosition = new Vector3(posX, posY, element.transform.position.z);

        if (doorUp) {
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
    }
}
