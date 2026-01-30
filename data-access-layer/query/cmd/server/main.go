package main

import (
	"log"
	"net"
	"strconv"

	"github.com/AbnerLimaa/icecream-shop/data-access-layer/query/internal/config"
	"github.com/AbnerLimaa/icecream-shop/data-access-layer/query/internal/database"
	"github.com/jackc/pgx/v5/pgxpool"
	"google.golang.org/grpc"
	"google.golang.org/grpc/reflection"

	"github.com/AbnerLimaa/icecream-shop/data-access-layer/query/internal/service"
	menuPb "github.com/AbnerLimaa/icecream-shop/data-access-layer/query/proto/menu/menu-proto"
)

func main() {
	environmentConfig := config.Load()
	port := strconv.Itoa(environmentConfig.Port)

	dbPool := database.GetPool(environmentConfig.DbUrl)
	defer dbPool.Close()

	createServer(port, dbPool)
}

func createServer(port string, dbPool *pgxpool.Pool) {
	listener, err := net.Listen("tcp", ":"+port)
	if err != nil {
		log.Fatalf("failed to listen: %v", err)
	}

	server := grpc.NewServer()
	menuPb.RegisterMenuServiceServer(server, &service.MenuService{Db: dbPool})
	reflection.Register(server)
	log.Printf("server listening on port: %s\n", port)

	if err := server.Serve(listener); err != nil {
		log.Fatalf("failed to serve: %v", err)
	}
}
