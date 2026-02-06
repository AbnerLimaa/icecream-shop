using IceCreamShopApi.Model.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.ViewModel;

public interface IViewModelAdapter<in T, out TK> : IAdapter<T, TK> where T : IData where TK : IViewModel;