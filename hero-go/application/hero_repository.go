package application

import (
	"errors"

	"github.com/jwojtasinska/hero/models"
	"gorm.io/gorm"
)

type HeroRepository interface {
	Save(*models.Hero) (*models.Hero, error)
	Get(int) (*models.Hero, error)
	GetAll() ([]models.Hero, error)
	Update(*models.Hero) (*models.Hero, error)
	Delete(*models.Hero) error
}

type heroRepo struct {
	db *gorm.DB
}

func NewHeroRepository(db *gorm.DB) HeroRepository {
	return &heroRepo{db}
}

func (repo *heroRepo) Save(hero *models.Hero) (*models.Hero, error) {
	var err = repo.db.Create(&hero).Error

	if err != nil {
		return nil, err
	}

	return hero, nil
}

func (repo *heroRepo) Update(hero *models.Hero) (*models.Hero, error) {
	var err = repo.db.Updates(&hero).Error

	if err != nil {
		return nil, err
	}

	return hero, nil
}

func (repo *heroRepo) Get(id int) (*models.Hero, error) {
	var hero models.Hero
	var err = repo.db.First(&hero, "id = ?", id).Error

	if errors.Is(err, gorm.ErrRecordNotFound) {
		return nil, nil
	}

	if err != nil {
		return nil, err
	}

	return &hero, nil
}

func (repo *heroRepo) GetAll() ([]models.Hero, error) {
	var heroes []models.Hero

	if err := repo.db.Find(&heroes).Error; err != nil {
		return nil, err
	}

	return heroes, nil
}

func (repo *heroRepo) Delete(hero *models.Hero) error {
	var err = repo.db.Delete(&hero, "id = ?", hero.Id).Error

	if err != nil {
		return err
	}

	return nil
}
