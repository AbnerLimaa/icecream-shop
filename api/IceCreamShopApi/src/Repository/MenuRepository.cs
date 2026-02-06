using System.Data;
using Dapper;
using IceCreamShopApi.Model.Data;
using Npgsql;

namespace IceCreamShopApi.Repository;

public class MenuRepository(IConfiguration configuration) : IRepository
{
    private readonly string? _connectionString = configuration.GetConnectionString("IceCreamShopDbConn")?? 
                                                 throw new InvalidOperationException("Connection string 'IceCreamShopDbConn' not found.");
    
    public string RepositoryName => "MenuRepository";

    public async Task<IReadOnlyCollection<MenuWithItem>> GetMenuWithItemsAsync(int menuId)
    {
        const string getMenuQuery = @"
            SELECT 
                menu.menu_id AS MenuId,
                menu.menu_name AS MenuName,
                items.menu_item_id AS MenuItemId,
                items.flavor AS Flavor, 
                items.price AS Price
            FROM tb_menu_items AS items
            INNER JOIN tb_menus AS menu ON items.menu_id = menu.menu_id
            WHERE items.menu_id = @MenuId";

        using IDbConnection db = new NpgsqlConnection(_connectionString);
        var rows = await db.QueryAsync<MenuWithItem>(getMenuQuery, new { MenuId = menuId });
        
        return rows.ToList();
    }
}