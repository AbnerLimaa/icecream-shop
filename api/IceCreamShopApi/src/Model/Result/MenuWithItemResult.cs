using IceCreamShopApi.Repository.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.Result;

public class MenuWithItemResult : IResult
{
    public string Flavor { get; }

    public string Price { get; }
    
    private MenuWithItemResult(string flavor, string price)
    {
        Flavor = flavor;
        Price = price;
    }

    public class Adapter : IAdapter<MenuWithItem, MenuWithItemResult>
    {
        public MenuWithItemResult Adapt(MenuWithItem source)
        {
            var price = $"{source.Price:C2}";

            return new MenuWithItemResult(source.Flavor, price);
        }
    }
}