package models

import (
	"errors"
	"strings"
)

type Hero struct {
	Id   int    `json:"id" gorm:"primary_key"`
	Name string `json:"name"`
}

func (Hero) TableName() string {
	// Set Table Name as heroes
	return "heroes"
}

func NewHero(name string) (*Hero, error) {
	var hero = Hero{Id: 0}
	var err = hero.ChangeHeroName(name)

	if err != nil {
		return nil, err
	}

	return &hero, nil
}

func (hero *Hero) ChangeHeroName(name string) error {
	var heroName = strings.TrimSpace(name)

	if len(heroName) == 0 {
		return errors.New("Name cannot be empty")
	}

	if len(heroName) < 3 {
		return errors.New("Name should have at least 3 characters")
	}

	hero.Name = heroName
	return nil
}
