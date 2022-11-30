using Application.DaoInterfaces;
using Shared.Models;

namespace FileData.DAOs;

public class CardItemFileDao : ICardItemDao

{
    private readonly FileContext context;
    
    public CardItemFileDao(FileContext context)
    {
        this.context = context;
    }
    
    public Task<CardItem> CreateAsync(CardItem cardItem)
    {
        int Id = 1;
        if (context.CardItems.Any())
        {
            Id = context.CardItems.Max(u => u.Id);
            Id++;
        }

        cardItem.Id = Id;
        
        context.CardItems.Add(cardItem);
        context.SaveChanges();

        return Task.FromResult(cardItem);
    }

    public Task<IEnumerable<CardItem>> GetAsync()
    {
        IEnumerable<CardItem> result = context.CardItems.AsEnumerable();
        return Task.FromResult(result);
    }

    public Task<CardItem> GetByIdAsync(int id)
    {
        CardItem? existing = context.CardItems.FirstOrDefault(cardItem => cardItem.Id == id);
        
        if (existing == null)
        {
            throw new Exception($"Card item with id {id} does not exist!");
        }
        
        return Task.FromResult(existing);
    }

    public Task<CardItem> IncreaseQuantityAsync(int id)
    {
        CardItem? existing = context.CardItems.FirstOrDefault(cardItem => cardItem.Id == id);
        
        if (existing == null)
        {
            throw new Exception($"Card item with id {id} does not exist!");
        }

        context.CardItems.Remove(existing);
        existing.Quantity++;
        context.CardItems.Add(existing);
        context.SaveChanges();
        return Task.FromResult(existing);
    }

    public Task<CardItem> DecreaseQuantityAsync(int id)
    {
        CardItem? existing = context.CardItems.FirstOrDefault(cardItem => cardItem.Id == id);
        
        if (existing == null)
        {
            throw new Exception($"Card item with id {id} does not exist!");
        }

        context.CardItems.Remove(existing);
        existing.Quantity--;
        context.CardItems.Add(existing);
        context.SaveChanges();
        return Task.FromResult(existing);
    }
}