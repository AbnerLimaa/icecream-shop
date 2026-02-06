namespace IceCreamShopApi.Model.ViewModel;

public class EmptyViewModel : IViewModel
{
    public static readonly  EmptyViewModel Instance = new();
    
    private EmptyViewModel()
    {
        
    }
}