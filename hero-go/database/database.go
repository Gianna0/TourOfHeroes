package database

import (
	"github.com/jwojtasinska/hero/configuration"
	"github.com/jwojtasinska/hero/models"
	"gorm.io/driver/sqlite"
	"gorm.io/gorm"
)

func AddDatabase(configuration *configuration.Configuration) (*gorm.DB, error) {
	db, err := gorm.Open(sqlite.Open(configuration.ConnectionString), &gorm.Config{})

	if err != nil {
		return nil, err
	}

	// Migrate the schema
	err = db.AutoMigrate(&models.Hero{})

	if err != nil {
		return nil, err
	}

	return db, err
}
