package application

import (
	"testing"

	"github.com/jwojtasinska/hero/models"
	"github.com/stretchr/testify/assert"
)

var repo = NewHeroStubRepo()
var service = NewHeroService(repo)

func Should_add_hero(t *testing.T) {
	var name = "Asia"
	var hero, err = models.NewHero(name)

	heroAdded, err := service.Save(hero)

	assert.NotNil(t, heroAdded)
	assert.Nil(t, err)
	assert.Equal(t, heroAdded.Name, hero.Name)
}

func Name_too_short_return_error(t *testing.T) {
	var name = "AB"
	var hero, err = models.NewHero(name)

	heroAdded, err := service.Save(hero)

	assert.NotNil(t, err)
	assert.Nil(t, heroAdded.Name)
	assert.Contains(t, err, "Name should have at least 3 characters")
}

func Empty_name_return_error(t *testing.T) {
	var name = ""
	var hero, err = models.NewHero(name)

	heroAdded, err := service.Save(hero)

	assert.NotNil(t, err)
	assert.Nil(t, heroAdded)
	assert.Contains(t, err, "cannot be empty")
}

func Should_get_hero(t *testing.T) {
	var name = "Asia"
	var hero = &models.Hero{Id: 1, Name: name}
	repo.Save(hero)

	heroFromDb, err := service.Get(hero.Id)

	assert.NotNil(t, heroFromDb)
	assert.Nil(t, err)
	assert.Equal(t, heroFromDb.Name, hero.Name)
}

func Invalid_id_when_update_should_return_error(t *testing.T) {
	var id = 1
	var newName = "Gianna"
	var newHero = &models.Hero{Id: id, Name: newName}

	heroFromDb, err := service.Update(newHero)

	assert.NotNil(t, heroFromDb)
	assert.Nil(t, newHero)
	assert.Contains(t, err, "Hero with id")
}

func Invalid_id_when_remove_should_return_error(t *testing.T) {
	var id = 1

	err := service.Delete(id)

	assert.NotNil(t, err)
	assert.Nil(t, id)
	assert.Contains(t, err, "Hero with id")
}
