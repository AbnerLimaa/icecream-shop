package database

import (
	"context"
	"log"

	"github.com/jackc/pgx/v5/pgxpool"
)

func GetPool(dbUrl string) *pgxpool.Pool {
	pgConfig, err := pgxpool.ParseConfig(dbUrl)
	if err != nil {
		log.Fatalf("database config error: %v", err)
	}

	ctx := context.Background()
	pool, err := pgxpool.NewWithConfig(ctx, pgConfig)
	if err != nil {
		log.Fatalf("unable to connect to database: %v", err)
	}

	log.Println("created database connection pool successfully!")
	return pool
}
