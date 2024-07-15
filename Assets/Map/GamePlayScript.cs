using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class GamePlayScript : MonoBehaviour
    {
        public int TotalRooms { get; set; }
        public HashSet<RoomController> RoomsEntered { get; set; } = new();

        private void Update()
        {
            // if (RoomsEntered.Count > 2 && RoomsEntered.Count == TotalRooms) {

            // }
        }
    }
}