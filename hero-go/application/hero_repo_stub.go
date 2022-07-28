package application

import (
	"github.com/jwojtasinska/hero/models"
)

type stubHeroRepo struct {
	container []*models.Hero
}

func NewHeroStubRepo() HeroRepository {
	return &stubHeroRepo{container: make([]*models.Hero, 0)}
}

func (repo *stubHeroRepo) Save(hero *models.Hero) (*models.Hero, error) {
	repo.container = append(repo.container, hero)
	return hero, nil
}

func (repo *stubHeroRepo) Update(hero *models.Hero) (*models.Hero, error) {
	return hero, nil
}

func (repo *stubHeroRepo) Get(id int) (*models.Hero, error) {
	for _, v := range repo.container {
		if v.Id == id {
			return v, nil
		}
	}

	return nil, nil
}

func (repo *stubHeroRepo) GetAll() ([]models.Hero, error) {
	var arrayHeroes = make([]models.Hero, len(repo.container))

	for _, v := range repo.container {
		arrayHeroes = append(arrayHeroes, *v)
	}

	return arrayHeroes, nil
}

func (repo *stubHeroRepo) Delete(hero *models.Hero) error {
	for i, v := range repo.container {
		if v.Id == hero.Id {
			repo.container = repo.container[:i+copy(repo.container[i:], repo.container[i+1:])]
			break
		}
	}

	return nil
}
