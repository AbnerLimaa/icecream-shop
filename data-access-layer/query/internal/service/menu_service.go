package service

import (
	"context"
	"fmt"
	"log"

	menuPb "github.com/AbnerLimaa/icecream-shop/data-access-layer/query/proto/menu/menu-proto"
	"github.com/jackc/pgx/v5/pgxpool"
)

type MenuService struct {
	menuPb.UnimplementedMenuServiceServer
	Db *pgxpool.Pool
}

const getMenuQuery = `
	SELECT menu.menu_name, items.flavor, items.price 
	FROM tb_menu_items AS items 
	INNER JOIN tb_menus AS menu ON items.menu_id = menu.menu_id 
	WHERE items.menu_id=$1
`

func (menuService *MenuService) GetMenu(ctx context.Context, request *menuPb.MenuRequest) (*menuPb.MenuResponse, error) {
	log.Println("Get Menu")

	rows, err := menuService.Db.Query(ctx, getMenuQuery, request.MenuId)
	if err != nil {
		return nil, fmt.Errorf("failed to query for menu entries: %w", err)
	}
	defer rows.Close()

	var items []*menuPb.MenuItem
	var menuName string
	for rows.Next() {
		item := &menuPb.MenuItem{}
	
		err := rows.Scan(&menuName, &item.Flavor, &item.Price)
		if err != nil {
			return nil, fmt.Errorf("failed to scan row: %w", err)
		}
		items = append(items, item)
	}

	if err := rows.Err(); err != nil {
		return nil, fmt.Errorf("error during row iteration: %w", err)
	}

	log.Println("Get Menu Success")
	return &menuPb.MenuResponse{
		MenuName: menuName,
		Items: items,
	}, nil
}
