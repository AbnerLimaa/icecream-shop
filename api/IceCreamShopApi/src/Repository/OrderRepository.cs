using System.Data;
using Dapper;
using IceCreamShopApi.Model.Data;
using Npgsql;

namespace IceCreamShopApi.Repository;

public class OrderRepository(IConfiguration configuration) : IRepository
{
    private readonly string? _connectionString = configuration.GetConnectionString("IceCreamShopDbConn")?? 
                                                 throw new InvalidOperationException("Connection string 'IceCreamShopDbConn' not found.");
    
    public string RepositoryName => "OrderRepository";

    public async Task<IReadOnlyCollection<OrderByClient>> GetAllOrdersByClientsAsync()
    {
        const string query = @"
            SELECT orders.order_id AS OrderId, 
                   clients.client_name AS ClientName, 
                   items.flavor AS Flavor,
                   items.price AS UnitPrice,
                   orders.quantity AS Quantity, 
                   orders.order_date AS OrderDate
            FROM tb_orders AS orders
            INNER JOIN tb_menu_items items ON orders.menu_item_id = items.menu_item_id
            INNER JOIN tb_clients clients ON orders.client_id = clients.client_id";

        using IDbConnection db = new NpgsqlConnection(_connectionString);
        var rows = await db.QueryAsync<OrderByClient>(query);
        
        return rows.ToList();
    }
}