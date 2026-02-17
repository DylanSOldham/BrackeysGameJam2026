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

    public void RevealRoom(Vector2Int position)
    {
        GameObject element = Instantiate(minimapElementPrefab, transform);
        element.transform.localPosition = new Vector3(11.0f * position.x, 11.0f * position.y, element.transform.position.z);
    }
}
