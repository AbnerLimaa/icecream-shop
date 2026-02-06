namespace IceCreamShopApi.Patterns;

public interface IAdapter<in T, out TK>
{
    TK Adapt(T source);
}