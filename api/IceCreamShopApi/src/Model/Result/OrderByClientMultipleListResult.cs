using System.Collections;

namespace IceCreamShopApi.Model.Result;

public class OrderByClientMultipleListResult(IEnumerable<OrderByClientListResult> _items) : IEnumerable<OrderByClientListResult>, IResult
{
    public IEnumerator<OrderByClientListResult> GetEnumerator()
    {
        return _items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}