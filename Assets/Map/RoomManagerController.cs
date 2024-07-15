using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class NewBehaviourScript : MonoBehaviour
    {
        private const int RoomWidth = 3;
        private const int RoomHeight = 3;
        [SerializeField] private int gridSizeX = 10;
        [SerializeField] private int gridSizeY = 10;
        [SerializeField] private GameObject[] roomPrefabs;
        [SerializeField] private int maxRooms = 15;
        [SerializeField] private int minRooms = 10;

        private readonly List<GameObject> _rooms = new();

        private readonly List<GameObject> CreateRooms = new();
        // [SerializeField] private readonly GameObject endRoomPrefab = null!;
        private int _count;

        private GamePlayScript _gamePlay;
        private bool _generateComplete;
        private int[,] _grid;
        private Queue<Vector2Int> _roomsToGenerate = null!;

        private void Start()
        {
            _grid = new int[gridSizeX, gridSizeY];
            _roomsToGenerate = new Queue<Vector2Int>();
            var initialRoomIndex = new Vector2Int(gridSizeX / 2, gridSizeY / 2);
            _gamePlay = FindObjectOfType<GamePlayScript>();
            StartRoomGeneration(initialRoomIndex);
        }

        private void Update()
        {
            if (!_generateComplete)
            {
                if (_roomsToGenerate.Count > 0 || _count < minRooms)
                {
                    if (_roomsToGenerate.Count == 0) return;
                    var index = _roomsToGenerate.Dequeue();
                    var x = index.x;
                    var y = index.y;
                    TryGenerateRoom(new Vector2Int(x - 1, y));
                    TryGenerateRoom(new Vector2Int(x + 1, y));
                    TryGenerateRoom(new Vector2Int(x, y - 1));
                    TryGenerateRoom(new Vector2Int(x, y + 1));
                }
                else
                {
                    _generateComplete = true;
                    _gamePlay.TotalRooms = _count;
                }
            }
        }

        private void OnDrawGizmos()
        {
            var color = new Color(0, 1, 1, 0.04f);
            Gizmos.color = color;

            for (var x = 0; x < gridSizeX; x++) 
            {
                for (var y = 0; y < gridSizeY; y++)
                {
                    var position = GetPositionFromIndex(new Vector2Int(x, y));
                    Gizmos.DrawWireCube(position, new Vector3(RoomWidth, RoomHeight, 1));
                }
            }
            
        }

        private GameObject GetRandomRoom()
        {
            return roomPrefabs[Random.Range(0, roomPrefabs.Length)];
        }

        private void StartRoomGeneration(Vector2Int index)
        {
            _roomsToGenerate.Enqueue(index);
            _grid[index.x, index.y] = 1;
            _count++;
            var initialRoom = Instantiate(GetRandomRoom(), GetPositionFromIndex(index), Quaternion.identity);
            initialRoom.name = $"Room_{_count}";
            initialRoom.GetComponent<RoomController>().RoomPosition = index;
            _gamePlay.RoomsEntered.Add(initialRoom.GetComponent<RoomController>());
            _rooms.Add(initialRoom);
        }

        private int CountNeighbours(Vector2Int index)
        {
            var count = 0;
            var x = index.x;
            var y = index.y;
            if (x > 0 && _grid[x - 1, y] == 1) count++; // Left
            if (x < gridSizeX - 1 && _grid[x + 1, y] == 1) count++; // Right
            if (y > 0 && _grid[x, y - 1] == 1) count++; // Down
            if (y < gridSizeY - 1 && _grid[x, y + 1] == 1) count++; // Up
            return count;
        }

        private bool TryGenerateRoom(Vector2Int index)
        {
            if (_count >= maxRooms) return false;

            // Check if index is within the valid range of the _grid array
            if (index.x < 0 || index.x >= gridSizeX || index.y < 0 || index.y >= gridSizeY) return false;

            // Check if a room already exists at this position
            if (_grid[index.x, index.y] == 1) return false;

            if (CountNeighbours(index) > 2) return false;

            if (Random.value < 0.5f && index != Vector2Int.zero) return false;
            _roomsToGenerate.Enqueue(index);
            _grid[index.x, index.y] = 1;
            _count++;
            var room = Instantiate(GetRandomRoom(), GetPositionFromIndex(index), Quaternion.identity);
            room.name = $"Room_{_count}";
            room.GetComponent<RoomController>().RoomPosition = index;
            _rooms.Add(room);
            OpenDoors(room, index);
            return true;
        }

        private void OpenDoors(GameObject room, Vector2Int index)
        {
            var x = index.x;
            var y = index.y;
            var roomControl = room.GetComponent<RoomController>();
            var left = GetRoomAt(new Vector2Int(x - 1, y));
            var right = GetRoomAt(new Vector2Int(x + 1, y));
            var up = GetRoomAt(new Vector2Int(x, y + 1));
            var down = GetRoomAt(new Vector2Int(x, y - 1));
            if (x > 0 && _grid[x - 1, y] == 1)
            {
                roomControl.OpenDoors(Vector2Int.left);
                left.OpenDoors(Vector2Int.right);
            }

            if (x < gridSizeX - 1 && _grid[x + 1, y] == 1)
            {
                roomControl.OpenDoors(Vector2Int.right);
                right.OpenDoors(Vector2Int.left);
            }

            if (y > 0 && _grid[x, y - 1] == 1)
            {
                roomControl.OpenDoors(Vector2Int.down);
                down.OpenDoors(Vector2Int.up);
            }

            if (y < gridSizeY - 1 && _grid[x, y + 1] == 1)
            {
                roomControl.OpenDoors(Vector2Int.up);
                up.OpenDoors(Vector2Int.down);
            }
        }

        private RoomController GetRoomAt(Vector2Int index)
        {
            var room = _rooms.Find(r => r.GetComponent<RoomController>().RoomPosition == index);
            return room?.GetComponent<RoomController>();
        }

        private Vector3 GetPositionFromIndex(Vector2Int index)
        {
            var gridX = index.x;
            var gridY = index.y;
            return new Vector3(RoomWidth * (gridX - gridSizeX / 2), RoomHeight * (gridY - gridSizeY / 2));
        }
    }
}