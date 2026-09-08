using Lainaamo.Models;

namespace Lainaamo.Repositories
{
    public interface IItemRepository
    {
        List<Item> GetItems();

        Item? GetItem(int id);

        Item AddItem(Item item);

        void RemoveItem(Item item);


    }
}
