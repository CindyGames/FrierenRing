using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject roomPrefab;
    public Transform mapParent;
    public int rows = 7;  // количество уровней по вертикали
    public int cols = 5;  // количество узлов на уровне
    public float xSpacing = 2.0f;
    public float ySpacing = 1.5f;

    private List<List<Room>> mapGrid = new List<List<Room>>();

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int row = 0; row < rows; row++)
        {
            List<Room> rowRooms = new List<Room>();

            for (int col = 0; col < cols; col++)
            {
                Vector2 position = new Vector2(col * xSpacing, row * -ySpacing);
                GameObject roomObj = Instantiate(roomPrefab, position, Quaternion.identity, mapParent);
                Room room = roomObj.GetComponent<Room>();

                if (room != null)
                {
                    rowRooms.Add(room);
                    AssignRoomType(room, row);  // Тип комнаты
                }
            }
            mapGrid.Add(rowRooms);
        }
        ConnectRooms();
    }

    void AssignRoomType(Room room, int row)
    {
        if (row == rows - 1)
            room.SetRoomType(RoomType.Boss);
        else if (Random.value < 0.2f)
            room.SetRoomType(RoomType.Shop);
        else if (Random.value < 0.3f)
            room.SetRoomType(RoomType.Rest);
        else
            room.SetRoomType(RoomType.Battle);
    }

    void ConnectRooms()
    {
        for (int row = 0; row < rows - 1; row++)
        {
            for (int i = 0; i < mapGrid[row].Count; i++)
            {
                Room room = mapGrid[row][i];
                int connections = Random.Range(1, 3);

                for (int j = 0; j < connections; j++)
                {
                    int nextRow = row + 1;
                    int nextCol = Mathf.Clamp(i + Random.Range(-1, 2), 0, cols - 1);
                    Room nextRoom = mapGrid[nextRow][nextCol];
                    room.ConnectTo(nextRoom);
                }
            }
        }
    }
}
