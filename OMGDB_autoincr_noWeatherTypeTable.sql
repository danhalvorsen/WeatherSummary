USE DB
GO

BEGIN TRAN

CREATE TABLE [Source] (
	Id INT IDENTITY(1, 1), 
	PRIMARY KEY (Id),
	
	[Name] VARCHAR(255),
	[URL] VARCHAR(255),
	[Authentication] VARCHAR(255)
);
GO

CREATE TABLE City (
	Id INT IDENTITY(1, 1), 
	PRIMARY KEY (Id),
	
	[Name] VARCHAR(255),
    Country VARCHAR(255),
    Altitude FLOAT,
	Longitude FLOAT,
    Latitude FLOAT,
);
GO

CREATE TABLE WeatherData (
	Id INT IDENTITY(1, 1),
	PRIMARY KEY (Id),
	
	[Date] DATETIME,
    WeatherType VARCHAR(255),
    Temperature FLOAT,
    Windspeed FLOAT,
    WindDirection FLOAT,
    WindspeedGust FLOAT,
    Pressure FLOAT,
    Humidity FLOAT,
    ProbOfRain FLOAT,
    AmountRain FLOAT,
    CloudAreaFraction FLOAT,
    FogAreaFraction FLOAT,
    ProbOfThunder FLOAT,
	
	FK_CityId INT NOT NULL,
	FOREIGN KEY (FK_CityId) REFERENCES City(Id)
);
GO

CREATE TABLE SourceWeatherData (
	Id INT IDENTITY(1, 1), 
	PRIMARY KEY (Id),

	ConnectionDate DATETIME,

	FK_SourceId INT NOT NULL,
	FK_WeatherDataId INT NOT NULL, 
	FOREIGN KEY (FK_SourceId) REFERENCES [Source](Id),
	FOREIGN KEY (FK_WeatherDataId) REFERENCES WeatherData(Id)
);
GO

CREATE TABLE [Admin] (
    Id INT IDENTITY(1, 1),
    PRIMARY KEY (Id),

	[Name] VARCHAR(255),
    Username VARCHAR(255),
    [Password] VARCHAR(255),
    Email VARCHAR(255),
);
GO

CREATE TABLE Score (
    Id INT IDENTITY(1, 1),
    PRIMARY KEY (Id),

	Score FLOAT,
	FK_WeatherDataId INT NOT NULL,
    FOREIGN KEY (FK_WeatherDataId) REFERENCES WeatherData(Id)
);
GO

INSERT INTO [Source] 
	([Name], [URL], [Authentication])
VALUES
	('Yr', 'www.yr.no', 'admin'),
	('OpenWeather', 'www.openweathermap.org', 'admin'),
	('FreeWeather', 'www.freeweather.com', 'admin');
GO

INSERT INTO City 
	([Name], Country, Altitude, Latitude, Longitude)
VALUES
	('Stavanger', 'Norway', 0, 59.1020129, 5.7126113572757),
	('Bergen', 'Norway', 0, 60.3943055, 5.3259192),
	('Trondheim', 'Norway', 0, 63.4305658, 10.3951929),
	('Oslo', 'Norway', 0, 59.9133301, 10.7389701),
	('Kristiansand', 'Norway', 0, 58.14615, 7.9957333);
GO

DECLARE @FK_StavangerId INT
DECLARE @FK_BergenId INT
DECLARE @FK_TrondheimId INT
DECLARE @FK_OsloId INT
DECLARE @FK_KristiansandId INT

SELECT @FK_StavangerId = Id FROM City WHERE [Name] = 'Stavanger'
SELECT @FK_BergenId = Id FROM City WHERE [Name] = 'Bergen'
SELECT @FK_TrondheimId = Id FROM City WHERE [Name] = 'Trondheim'
SELECT @FK_OsloId = Id FROM City WHERE [Name] = 'Oslo'
SELECT @FK_KristiansandId = Id FROM City WHERE [Name] = 'Kristiansand'


INSERT INTO WeatherData 
	(FK_CityId, [Date], WeatherType, Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder)
VALUES
	(@FK_StavangerId, GETDATE(), 'sunny',  14.3, 2.5, 293.8, 5.6, 1023.7, 42.4, 0, 0, 13.5, 0, 0),
	(@FK_StavangerId, GETDATE() + 1, 'rain',  14.6, 2.2, 290.2, 5.5, 1023.7, 43.2, 0.01, 0.02, 13.6, 0, 0),
	(@FK_StavangerId, GETDATE() + 2, 'cloudy',  14.4, 2.4, 292.8, 5.2, 1022.7, 42.6, 0, 0, 13.4, 0, 0),
	(@FK_BergenId, GETDATE() + 3, 'rain',  15.4, 2.5, 309.8, 5.8, 1023.8, 45.8, 0, 0, 36.1, 0, 0),
	(@FK_BergenId, GETDATE() + 4, 'cloudy',  15.0, 2.3, 310.1, 5.6, 1023.5, 45.4, 0, 0, 35.8, 0, 0),
	(@FK_BergenId, GETDATE() + 5, 'light rain',  15.2, 2.4, 309.5, 5.9, 1024.3, 44.9, 0, 0, 35.6, 0, 0),
	(@FK_TrondheimId, GETDATE() + 6, 'thunder',  12.5, 1.8, 8.7, 4.2, 1025.7, 53.7, 0, 0, 0, 0, 0),
	(@FK_TrondheimId, GETDATE() + 7, 'sunny',  12.6, 2.1, 8.4, 4.2, 1025.4, 53.2, 0, 0, 0.2, 0, 0),
	(@FK_TrondheimId, GETDATE() + 8, 'cloudy',  12.3, 1.7, 8.5, 4.1, 1025.6, 53.9, 0, 0, 0.1, 0, 0),
	(@FK_OsloId, GETDATE() + 9, 'snow',  17.9, 1.9, 247.3, 5.3, 1024.9, 57.1, 0, 0, 0.1, 0, 0),
	(@FK_OsloId, GETDATE() + 10, 'clear sky',  18.1, 2.1, 247.1, 5.4, 1024.7, 57.2, 0, 0, 0, 0, 0),
	(@FK_OsloId, GETDATE() + 11, 'light rain',  17.7, 2.2, 247.5, 5.1, 1025.1, 56.9, 0, 0, 0.2, 0, 0),
	(@FK_KristiansandId, GETDATE() + 12, 'sleet',  17.0, 4.6, 85.7, 9.4, 1023.8, 44.6, 0, 0, 0, 0, 0),
	(@FK_KristiansandId, GETDATE() + 13, 'snow',  16.9, 4.5, 86.1, 9.2, 1023.7, 44.4, 0, 0, 0.1, 0, 0),
	(@FK_KristiansandId, GETDATE() + 14, 'heavy snow',  16.8, 4.4, 85.8, 9.5, 1023.5, 43.9, 0, 0, 0, 0, 0);
GO

DECLARE @DummyWeatherId INT
SELECT @DummyWeatherId = Id FROM WeatherData

DECLARE @FK_SourceYRId INT
DECLARE @FK_SourceStormId INT
DECLARE @FK_SourceFreeWeatherId INT
SELECT @FK_SourceYRId = Id FROM [Source] WHERE [Name] = 'Yr'
SELECT @FK_SourceStormId = Id FROM [Source] WHERE [Name] = 'OpenWeather'
SELECT @FK_SourceFreeWeatherId = Id FROM [Source] WHERE [Name] = 'FreeWeather'


INSERT INTO  SourceWeatherData
	(FK_WeatherDataId, FK_SourceId, ConnectionDate)
VALUES
	(@DummyWeatherId, @FK_SourceYRId, GETDATE()),
	(@DummyWeatherId, @FK_SourceStormId, GETDATE() + 1),
	(@DummyWeatherId, @FK_SourceFreeWeatherId, GETDATE() + 2),
	(@DummyWeatherId, @FK_SourceYRId, GETDATE() + 3),
	(@DummyWeatherId, @FK_SourceStormId, GETDATE() + 4),
	(@DummyWeatherId, @FK_SourceFreeWeatherId, GETDATE() + 5),
	(@DummyWeatherId, @FK_SourceYRId, GETDATE() + 6),
	(@DummyWeatherId, @FK_SourceStormId, GETDATE() + 7),
	(@DummyWeatherId, @FK_SourceFreeWeatherId, GETDATE() + 8),
	(@DummyWeatherId, @FK_SourceYRId, GETDATE() + 9),
	(@DummyWeatherId, @FK_SourceStormId, GETDATE() + 10),
	(@DummyWeatherId, @FK_SourceFreeWeatherId, GETDATE() + 11),
	(@DummyWeatherId, @FK_SourceYRId, GETDATE() + 12),
	(@DummyWeatherId, @FK_SourceStormId, GETDATE() + 13),
	(@DummyWeatherId, @FK_SourceFreeWeatherId, GETDATE() + 14);

UPDATE SourceWeatherData
SET FK_WeatherDataId = (
	SELECT Id FROM WeatherData WHERE SourceWeatherData.Id = WeatherData.Id
	);
GO

INSERT INTO [Admin] 
	([Name], Username, [Password], Email)
VALUES
	('Torbjørn Jacobsen', 'tobjac', 'bouvet1', 'tj@bouvet.no'),
	('Mohammad Ghasempour', 'mohgha', 'bouvet2', 'mg@bouvet.no'),
	('Rebecca Sjøen', 'rebsjo', 'bouvet3', 'rs@bouvet.no'),
	('Dan Edvard Halvorsen', 'danhal', 'bouvet4', 'deh@bouvet.no');
GO


--SELECT * FROM [Source];
--SELECT * FROM City;
--SELECT * FROM WeatherData;
--SELECT * FROM SourceWeatherData;
--SELECT * FROM [Admin];

ROLLBACK TRAN
--COMMIT TRAN