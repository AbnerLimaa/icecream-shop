package config

import (
	"log"
	"os"
	"sync"

	"github.com/joho/godotenv"
	"github.com/kelseyhightower/envconfig"
)

type EnvironmentConfig struct {
	DbUrl string `envconfig:"DB_URL" required:"true"`
	Port  int    `envconfig:"PORT" default:"50051"`
}

var (
	environmentConfig *EnvironmentConfig
	once              sync.Once
)

func Load() *EnvironmentConfig {
	once.Do(func() {
		environment := os.Getenv("GO_ENV")
		if environment == "" {
			environment = "development"
		}

		err := godotenv.Load(".env." + environment)
		if err != nil {
			log.Fatalf("error loading .env file: %v", err)
			return
		}

		var ec EnvironmentConfig
		err = envconfig.Process("", &ec)
		if err != nil {
			log.Fatalf("error loading EnvironmentConfig data: %v", err)
		}

		environmentConfig = &ec
		log.Println("configuration loaded successfully!")
	})

	return environmentConfig
}
