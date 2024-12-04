using System.Collections.Generic;
using UnityEngine;

public enum RoomType { Battle, Shop, Rest, Boss }

public class Room : MonoBehaviour
{
    public RoomType roomType;
    public List<Room> connectedRooms = new List<Room>();
    public GameObject connectionLinePrefab;

    public void SetRoomType(RoomType type)
    {
        roomType = type;
        // Здесь можно задать цвет или иконку комнаты в зависимости от типа
        switch (roomType)
        {
            case RoomType.Battle:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case RoomType.Shop:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case RoomType.Rest:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case RoomType.Boss:
                GetComponent<SpriteRenderer>().color = Color.black;
                break;
        }
    }

    public void ConnectTo(Room otherRoom)
    {
        if (!connectedRooms.Contains(otherRoom))
        {
            connectedRooms.Add(otherRoom);
            otherRoom.connectedRooms.Add(this);
            CreateConnectionLine(otherRoom);
        }
    }

    private void CreateConnectionLine(Room otherRoom)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = otherRoom.transform.position;
        GameObject lineObj = Instantiate(connectionLinePrefab);
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }
}
