using IceCreamShopApi.Model.Data;
using IceCreamShopApi.Patterns;

namespace IceCreamShopApi.Model.ViewModel;

public interface IViewModelMultipleAdapter<in T, out TK> : IMultipleAdapter<T, TK> where T : IData where TK : IViewModel;