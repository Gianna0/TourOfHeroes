package models

import (
	"testing"

	"github.com/stretchr/testify/assert"
)

func Should_create_hero(t *testing.T) {

	var name = "Asia"
	var hero, err = NewHero(name)

	assert.NotNil(t, hero)
	assert.Nil(t, err)
	assert.Equal(t, hero.Name, name)
}

func Empty_name_should_return_error(t *testing.T) {

	var name string = ""
	var hero, err = NewHero(name)

	assert.NotNil(t, err)
	assert.Nil(t, hero)
	assert.Contains(t, err, "cannot be empty")
}

func Name_too_short_return_error(t *testing.T) {

	var name string = "ab"
	var hero, err = NewHero(name)

	assert.NotNil(t, err)
	assert.Nil(t, hero)
	assert.Contains(t, err, "Name should have at least 3 characters")
}
