namespace Application.DTOs.PriorityQueue
{
    public class PriorityQueue<T>
    {
        private List<(T item, double? priority)> elements = new List<(T item, double? priority)>();

        public int Count => elements.Count;

        public void Enqueue(T item, double? priority)
        {
            elements.Add((item, priority));
            elements = elements.OrderBy(e => e.priority).ToList();
        }

        public T Dequeue()
        {
            var item = elements.First().item;
            elements.RemoveAt(0);
            return item;
        }
    }
}
