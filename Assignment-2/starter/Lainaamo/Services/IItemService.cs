using Lainaamo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lainaamo.Services
{
    public interface IItemService
    {
        List<Item> GetItems();

        Item? GetById(int id);

        Item Create(int id, string name);

    }
}
