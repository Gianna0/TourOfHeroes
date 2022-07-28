package main

import (
	"fmt"
	"net/http"
	"strconv"

	"github.com/gin-contrib/cors"
	"github.com/gin-contrib/static"
	"github.com/gin-gonic/gin"
	"github.com/jwojtasinska/hero/application"
	"github.com/jwojtasinska/hero/configuration"
	"github.com/jwojtasinska/hero/database"
	"github.com/jwojtasinska/hero/models"
)

var repo application.HeroRepository
var service application.HeroServiceInterface

func healthCheck(c *gin.Context) {
	c.IndentedJSON(http.StatusOK, "Welcome to Heroes API!")
}

func getHeroes(c *gin.Context) {
	var heroes, err = service.GetAll()

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	c.IndentedJSON(http.StatusOK, heroes)
}

func getHero(c *gin.Context) {
	stringId := c.Param("id")
	id, err := strconv.Atoi(stringId)

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": "Invalid id"})
		return
	}

	hero, err := service.Get(int(id))

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	if hero == nil {
		c.IndentedJSON(http.StatusNotFound, gin.H{"error": fmt.Sprint("Hero with id '", id, "' was not found")})
		return
	}

	c.IndentedJSON(http.StatusOK, hero)

}

func addHero(c *gin.Context) {
	var newHero models.Hero
	if err := c.BindJSON(&newHero); err != nil {
		return
	}

	var hero, err = service.Save(&newHero)

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	c.IndentedJSON(http.StatusCreated, &hero)
}

func updateHero(c *gin.Context) {
	stringId := c.Param("id")
	id, err := strconv.Atoi(stringId)
	var heroUpdate models.Hero

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": "Invalid id"})
		return
	}

	if err := c.BindJSON(&heroUpdate); err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	heroUpdate.Id = id
	heroUpdated, err := service.Update(&heroUpdate)

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	c.IndentedJSON(http.StatusOK, &heroUpdated)
}

func deleteHero(c *gin.Context) {
	stringId := c.Param("id")
	id, err := strconv.Atoi(stringId)

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": "Invalid id"})
		return
	}

	err = service.Delete(id)

	if err != nil {
		c.IndentedJSON(http.StatusBadRequest, gin.H{"error": err.Error()})
		return
	}

	c.IndentedJSON(http.StatusOK, gin.H{"success": fmt.Sprint("Hero with '", id, "' deleted")})
}

func main() {
	config, err := configuration.GetSettings()

	if err != nil {
		fmt.Println("error:", err)
		return
	}

	db, err := database.AddDatabase(config)

	if err != nil {
		fmt.Println("error:", err)
		return
	}

	repo = application.NewHeroRepository(db)
	service = application.NewHeroService(repo)

	router := gin.Default()
	router.Use(cors.New(cors.Config{
		AllowOrigins:  []string{"*"},
		AllowMethods:  []string{"*"},
		AllowHeaders:  []string{"*"},
		ExposeHeaders: []string{"*"},
	}))
	router.Use(static.Serve("/", static.LocalFile(config.DistPath, false)))
	router.NoRoute(func(c *gin.Context) {
		c.File(config.DistPath)
	})

	api := router.Group("/api")
	api.GET("/api", healthCheck)

	apiHeroes := api.Group("/heroes")
	apiHeroes.GET("", getHeroes)
	apiHeroes.POST("", addHero)
	apiHeroes.GET(":id", getHero)
	apiHeroes.PUT(":id", updateHero)
	apiHeroes.DELETE(":id", deleteHero)

	router.Run(config.BindAddr)
}
