using Lainaamo.Models;

namespace Lainaamo.Services
{
    public interface IItemService
    {
        List<Item> GetItems();

        Item? GetById(int id);

        Item Create(int id, string name);

        void Delete(int id);
    }
}
