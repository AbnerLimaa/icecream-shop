using IceCreamShopApi.Model.Data;

namespace IceCreamShopApi.Model.ViewModel;

public class MenuWithItemViewModel : IViewModel
{
    public string Flavor { get; }

    public string Price { get; }
    
    private MenuWithItemViewModel(string flavor, string price)
    {
        Flavor = flavor;
        Price = price;
    }

    public class Adapter : IViewModelAdapter<MenuWithItem, MenuWithItemViewModel>
    {
        public MenuWithItemViewModel Adapt(MenuWithItem source)
        {
            var price = $"{source.Price:C2}";

            return new MenuWithItemViewModel(source.Flavor, price);
        }
    }
}