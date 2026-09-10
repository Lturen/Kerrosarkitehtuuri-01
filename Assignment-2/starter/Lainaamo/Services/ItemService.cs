using Lainaamo.Models;
using Lainaamo.Repositories;
using System.Xml;

namespace Lainaamo.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _items;

        public ItemService (IItemRepository items) 
        {
            _items = items;
        }

        public List<Item> GetItems()
        {
            return _items.GetItems();
        }

        public Item? GetById(int id)
        {
            Item? item = _items.GetItem(id);

            if (item == null)
            {
                throw new DllNotFoundException("There is no product like you are searching");
            }


            return item;
        }

        public Item Create(Item item)
        {
            var nimet = _items.GetItems().Select(i => i.Name).ToList();

            if (nimet.Contains(item.Name))
            {
                throw new InvalidOperationException("Item with the same name already exists");
            }
            else if (item.Name == null || item.Name == " " || item.Name.Length < 3)
            {
                throw new InvalidOperationException("Item name must be at least 3 characters long");
            }

            return _items.AddItem(item);
        }



    }
}
