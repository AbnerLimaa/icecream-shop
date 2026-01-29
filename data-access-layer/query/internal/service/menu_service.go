package service

import (
	"context"
	"fmt"

	menuPb "github.com/AbnerLimaa/icecream-shop/data-access-layer/query/proto/menu-proto"
	"github.com/jackc/pgx/v5/pgxpool"
)

type MenuService struct {
	menuPb.UnimplementedMenuServiceServer
	Db *pgxpool.Pool
}

func (menuService *MenuService) GetMenu(ctx context.Context, _ *menuPb.MenuRequest) (*menuPb.MenuResponse, error) {
	rows, err := menuService.Db.Query(ctx, "select menu_id, flavor, price from tb_menu")
	if err != nil {
		return nil, fmt.Errorf("failed to query for menu entries: %w", err)
	}
	defer rows.Close()

	var items []*menuPb.MenuItem

	for rows.Next() {
		var item menuPb.MenuItem
		err := rows.Scan(&item.MenuId, &item.Flavor, &item.Price)
		if err != nil {
			return nil, fmt.Errorf("failed to scan row: %w", err)
		}
		items = append(items, &item)
	}

	if err := rows.Err(); err != nil {
		return nil, fmt.Errorf("error during row iteration: %w", err)
	}

	return &menuPb.MenuResponse{
		Items: items,
	}, nil
}
