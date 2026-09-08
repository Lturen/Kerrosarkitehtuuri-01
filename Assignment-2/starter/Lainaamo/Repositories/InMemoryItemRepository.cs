using Lainaamo.Models;




namespace Lainaamo.Repositories
{
    public class InMemoryItemRepository : IItemRepository
    {
        private static readonly List<Item> _items = new()
    {
        new Item { Id = 1, Name = "Salibandymaila" },
        new Item { Id = 2, Name = "Projektori" },
        new Item { Id = 3, Name = "HDMI-kaapeli" }
    };
        private static int _nextItemId = 4;



        public List<Item> GetItems()
        {
            return _items;
        }

        public Item GetItem(int id)
        {
            return _items.FirstOrDefault(i => i.Id == id);
        }

        public Item AddItem(Item item)
        {
            item.Id = _nextItemId++;
            _items.Add(item);
            return item;
        }

        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }


    }
}
