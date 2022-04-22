use db
GO 

BEGIN TRAN

CREATE TABLE City (
    CityID INT,
    City VARCHAR(255),
    Country VARCHAR(255),
    Longitude FLOAT,
    Altitude FLOAT,
    Latitude FLOAT,
    PRIMARY KEY (CityID)
);

CREATE TABLE ForecastWebsite (
    WebsiteID INT,
    WebsiteName VARCHAR(255),
    WebURL VARCHAR(255),
    Auth VARCHAR(255),
    PRIMARY KEY (WebsiteID)
);

CREATE TABLE ForecastData (
    ForecastID INT,
	PRIMARY KEY (ForecastID),
    ForecastDate DATETIME,
    Temperature FLOAT,
    Windspeed FLOAT,
    WindDirection INT,
    WindspeedGust FLOAT,
    Pressure FLOAT,
    Humidity FLOAT,
    ProbOfRain FLOAT,
    AmountRain FLOAT,
    CloudAreaFraction FLOAT,
    FogAreaFraction FLOAT,
    ProbOfThunder FLOAT,
	FK_WebsiteID INT NOT NULL,
	FK_CityID INT NOT NULL,
    FOREIGN KEY (FK_CityID) REFERENCES City(CityID),
    --FOREIGN KEY (FK_WebsiteID) REFERENCES Forecast_data_website(Id)
);

CREATE TABLE Forecast_data_website (
    Id INT,
    PRIMARY KEY (Id),
	ConnectionDate DATETIME,
	FK_ForecastID INT,
	FK_WebsiteID INT,
    FOREIGN KEY (FK_ForecastID) REFERENCES ForecastData(ForecastID),
    FOREIGN KEY (FK_WebsiteID) REFERENCES ForecastWebsite(WebsiteID)
);

CREATE TABLE Admin (
    AdminID INT,
    FullName VARCHAR(255),
    Username VARCHAR(255),
    UserPassword VARCHAR(255),
    Email VARCHAR(255),
    PRIMARY KEY (AdminID)
);

CREATE TABLE WeatherType (
    WeatherID INT,
    Cloudy BIT ,
    Sunny BIT ,
    Rainy BIT ,
    Snowy BIT ,
    Stormy BIT ,
    PRIMARY KEY (WeatherID),
	FK_ForecastID INT NOT NULL,
    FOREIGN KEY (FK_ForecastID) REFERENCES ForecastData(ForecastID)
);

ALTER TABLE ForecastData 
	ADD CONSTRAINT FK_WebsiteID 
	FOREIGN KEY (FK_WebsiteID) REFERENCES Forecast_data_website(Id)

--LLBACK TRAN
COMMIT TRAN
