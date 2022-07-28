package configuration

import (
	"encoding/json"
	"os"
)

type Configuration struct {
	BindAddr         string
	ConnectionString string
	DistPath         string
}

func GetSettings() (*Configuration, error) {
	var file, err = os.Open("configuration.json")

	if err != nil {
		return nil, err
	}

	defer file.Close()
	configuration := Configuration{}
	decoder := json.NewDecoder(file)
	err = decoder.Decode(&configuration)

	if err != nil {
		return nil, err
	}

	return &configuration, nil
}
