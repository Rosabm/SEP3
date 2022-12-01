using Shared.Models;

namespace Application.DaoInterfaces;

public interface ICardItemDao
{
    Task<CardItem> CreateAsync(CardItem cardItem);
    Task<IEnumerable<CardItem>> GetAsync();
    Task<CardItem> GetByIdAsync(int id);
    Task<int> GetQuantityById(int id);
    Task<CardItem> UpdateQuantityAsync(int id, int newQuantity);
}