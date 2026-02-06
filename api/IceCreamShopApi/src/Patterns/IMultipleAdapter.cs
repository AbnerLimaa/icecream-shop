namespace IceCreamShopApi.Patterns;

public interface IMultipleAdapter<in T, out TK>
{
    TK Adapt(IEnumerable<T> sources);
}