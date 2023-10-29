namespace RayRoom.Structures
{
    public class ShiftArray<T>
    {
        public T this[int index]
        {
            get
            {
                return items[(index + Offset) % items.Length];
            }
            set
            {
                items[(index + Offset) % items.Length] = value;
            }
        }

        public int Offset { get; set; }

        private T[] items;

        public ShiftArray(int capacity)
        {
            items = new T[capacity];
        }
    }
}
