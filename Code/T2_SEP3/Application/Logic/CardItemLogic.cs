using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class CardItemLogic : ICardItemLogic
{
    private readonly ICardItemDao dao;
    private readonly ISocksInventoryDao invDao;
    private readonly ISocksDao sockDao;
    
    
    public CardItemLogic(ICardItemDao dao,ISocksInventoryDao invDao,ISocksDao sockDao)
    {
        this.dao = dao;
        this.invDao = invDao;
        this.sockDao = sockDao;
    }

 /*   public void InvCheck(int cardId, int qnt,int cardItemId)
    {
       IEnumerable<Inventory> inventories = invDao.GetByCardIdAsync(cardId).Result;

       CardItem cardItem = dao.GetByIdAsync(cardItemId).Result;

       Socks sockToCompare = sockDao.GetById(cardItem.ProductId).Result;
       
      
       Inventory? toCompare = null ;

       foreach (var x in inventories)
       {
           if (x.Color.Equals(sockToCompare.Color)&&x.Size.Equals(sockToCompare.Size))
           {
               toCompare = x;
           }
       }

       if (toCompare.Quantity >= qnt) throw new Exception("quantity is more than the avaiable inv");
    }
**/
    public async Task<CardItem> CreateAsync(CreateCardItemDto dto)
    {
        CardItem cardItem = new CardItem(dto.ShoppingCartId, dto.ProductId);
        CardItem created = await dao.CreateAsync(cardItem);
        return cardItem;
    }

    public Task<IEnumerable<CardItem>> GetAsync()
    {
        return dao.GetAsync() ?? throw new Exception("CardItems were not found!!!");
    }


    public async Task<CardItem> GetByIdsAsync(int idProduct, int idShoppingCard)
    {
        ValidateById(idProduct);
        ValidateById(idShoppingCard);
        
        return await dao.GetByIdsAsync(idProduct, idShoppingCard) ?? throw new Exception($"CardItem with product id {idProduct} and shoppingCart id {idShoppingCard} were not found!!!");
    }

    public async Task<CardItem> GetByIdAsync(int id)
    {
        ValidateById(id);
        return await dao.GetByIdAsync(id) ?? throw new Exception($"CardItem with ID {id} was not found!!!");
    }

    public async Task<int> GetQuantityById(int id)
    {
        ValidateById(id);
        CardItem? cardItem = await dao.GetByIdAsync(id);
        if (cardItem == null)
        {
            throw new Exception($"CardItem with ID {id} was not found!!!");
        }

        return await dao.GetQuantityById(id);
    }

    public Task<CardItem> UpdateQuantityAsync(int id, int newQuantity)
    {
        ValidateById(id);
        ValidateById(newQuantity);
        return dao.UpdateQuantityAsync(id, newQuantity) ?? throw new Exception($"CardItem with ID {id} was not found!!!");
    }

    public Task DeleteAsync(int cardId)
    {
        ValidateById(cardId);
        return dao.DeleteAsync(cardId) ?? throw new Exception($"CardItem with ID {cardId} was not found!!!");
    }

    public void ValidateCardItem(CardItem cardItem)
    {
        if (cardItem.Quantity <= 0) throw new Exception("Quantity must be > 0!!!");
        if (cardItem.ProductId <= 0) throw new Exception("Product id must be > 0!!!");
        if (cardItem.ShoppingCartId <= 0) throw new Exception("ShoppingCart id must be > 0!!!");
    }

    public void ValidateById(int id)
    {
        if (id <= 0) throw new Exception("Id must be > 0!!!");
    }
}