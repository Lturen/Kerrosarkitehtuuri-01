using Lainaamo.Models;
using Lainaamo.Repositories;
using Lainaamo.Exceptions;

namespace Lainaamo.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _items;

        private readonly ILoanRepository _loans;

        public ItemService(IItemRepository items, ILoanRepository loans)
        {
            _items = items;
            _loans = loans;
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
                throw new NotFoundException("There is no product like you are searching");
            }

            return item;
        }

        public Item Create(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Trim().Length < 3)
            {
                throw new ItemCantBeCreated("Item name must be at least 3 characters long");
            }

            name = name.Trim();

            bool nameTaken = _items.GetItems()
                .Any(i => string.Equals(i.Name, name, StringComparison.OrdinalIgnoreCase));

            if (nameTaken)
            {
                throw new ItemCantBeCreated("Item with the same name already exists");
            }

            return _items.AddItem(new Item { Name = name });
        }

        public void Delete(int id)
        {
            Item? item = _items.GetItem(id);

            if (item == null)
            {
                throw new NotFoundException($"Item {id} not found.");
            }

            bool onLoan = _loans.GetLoans()
                .Any(l => l.ItemId == id && l.ReturnedAt == null);

            if (onLoan)
            {
                throw new ItemCantBeDeleted($"Item {id} cannot be deleted while it is on loan.");
            }

            _items.RemoveItem(item);
        }
    }
}
