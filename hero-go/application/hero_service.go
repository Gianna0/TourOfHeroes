package application

import (
	"errors"
	"fmt"

	"github.com/jwojtasinska/hero/models"
)

type heroService struct {
	heroRepo HeroRepository
}

type HeroServiceInterface interface {
	Save(*models.Hero) (*models.Hero, error)
	GetAll() ([]models.Hero, error)
	Get(int) (*models.Hero, error)
	Update(*models.Hero) (*models.Hero, error)
	Delete(int) error
}

func NewHeroService(repo HeroRepository) HeroServiceInterface {
	return &heroService{heroRepo: repo}
}

var _ HeroServiceInterface = &heroService{}

func (service *heroService) Save(hero *models.Hero) (*models.Hero, error) {
	var newHero, err = models.NewHero(hero.Name)

	if err != nil {
		return nil, err
	}

	return service.heroRepo.Save(newHero)
}

func (service *heroService) Update(hero *models.Hero) (*models.Hero, error) {
	var heroToUpdate, err = service.heroRepo.Get(hero.Id)

	if err != nil {
		return nil, err
	}

	if heroToUpdate == nil {
		return nil, errors.New(fmt.Sprint("Hero with id '", hero.Id, "' was not found"))
	}

	err = heroToUpdate.ChangeHeroName(hero.Name)

	if err != nil {
		return nil, err
	}

	return service.heroRepo.Update(heroToUpdate)
}

func (service *heroService) Get(id int) (*models.Hero, error) {
	var hero, err = service.heroRepo.Get(id)

	if err != nil {
		return nil, err
	}

	return hero, nil
}

func (service *heroService) GetAll() ([]models.Hero, error) {
	return service.heroRepo.GetAll()
}

func (service *heroService) Delete(id int) error {
	var hero, err = service.heroRepo.Get(id)

	if err != nil {
		return err
	}

	if hero == nil {
		return errors.New(fmt.Sprint("Hero with id '", id, "' was not found"))
	}

	err = service.heroRepo.Delete(hero)

	if err != nil {
		return err
	}

	return nil
}
