using UnityEngine;

namespace Map
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private GameObject[] doors;

        private GamePlayScript _gamePlay;
        public Vector2Int RoomPosition { get; set; }

        private void Start()
        {
            _gamePlay = FindObjectOfType<GamePlayScript>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                MoveCameraToRoom();
                if (!_gamePlay.RoomsEntered.Contains(this))
                {
                    _gamePlay.RoomsEntered.Add(this);
                }
            }
        }

        public void OpenDoors(Vector2Int direction)
        {
            if (direction == Vector2Int.up)
                doors[0].SetActive(false);
            else if (direction == Vector2Int.down)
                doors[1].SetActive(false);
            else if (direction == Vector2Int.left)
                doors[2].SetActive(false);
            else if (direction == Vector2Int.right) doors[3].SetActive(false);
        }

        private void MoveCameraToRoom()
        {
            if (Camera.main != null)
                Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y,
                    Camera.main.transform.position.z);
        }
    }
}