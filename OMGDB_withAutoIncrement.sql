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

CREATE TABLE WeatherType (
	Id INT IDENTITY(1, 1), 
	PRIMARY KEY (Id),

    Cloudy BIT,
    Sunny BIT,
    Rainy BIT,
    Snowy BIT,
    Stormy BIT,
	
	FK_WeatherId INT NOT NULL,
    FOREIGN KEY (FK_WeatherId) REFERENCES WeatherData(Id)
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

INSERT INTO [Source] 
	([Name], [URL], [Authentication])
VALUES
	('YR', 'www.yr.no', 'admin'),
	('STORM', 'www.storm.no', 'admin'),
	('FreeWeather', 'www.freeweather.com', 'admin');
GO

INSERT INTO City 
	([Name], Country, Altitude, Latitude, Longitude)
VALUES
	('Stavanger', 'Norway', 0, 58.97044361825643, 5.733197834639823),
	('Bergen', 'Norway', 0, 60.394309639588755, 5.3212627601051405),
	('Trondheim', 'Norway', 0, 63.43176491726623, 10.394850990213525),
	('Oslo', 'Norway', 0, 59.91521319366343, 10.75256976589296),
	('Kristiansand', 'Norway', 0, 58.16090705867245, 8.018480108853817);
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
	(FK_CityId, [Date], Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder)
VALUES
	(@FK_StavangerId, GETDATE(),  14.3, 2.5, 293.8, 5.6, 1023.7, 42.4, 0, 0, 13.5, 0, 0),
	(@FK_StavangerId, GETDATE() + 1,  14.6, 2.2, 290.2, 5.5, 1023.7, 43.2, 0.01, 0.02, 13.6, 0, 0),
	(@FK_StavangerId, GETDATE() + 2,  14.4, 2.4, 292.8, 5.2, 1022.7, 42.6, 0, 0, 13.4, 0, 0),
	(@FK_BergenId, GETDATE() + 3,  15.4, 2.5, 309.8, 5.8, 1023.8, 45.8, 0, 0, 36.1, 0, 0),
	(@FK_BergenId, GETDATE() + 4,  15.0, 2.3, 310.1, 5.6, 1023.5, 45.4, 0, 0, 35.8, 0, 0),
	(@FK_BergenId, GETDATE() + 5,  15.2, 2.4, 309.5, 5.9, 1024.3, 44.9, 0, 0, 35.6, 0, 0),
	(@FK_TrondheimId, GETDATE() + 6,  12.5, 1.8, 8.7, 4.2, 1025.7, 53.7, 0, 0, 0, 0, 0),
	(@FK_TrondheimId, GETDATE() + 7,  12.6, 2.1, 8.4, 4.2, 1025.4, 53.2, 0, 0, 0.2, 0, 0),
	(@FK_TrondheimId, GETDATE() + 8,  12.3, 1.7, 8.5, 4.1, 1025.6, 53.9, 0, 0, 0.1, 0, 0),
	(@FK_OsloId, GETDATE() + 9,  17.9, 1.9, 247.3, 5.3, 1024.9, 57.1, 0, 0, 0.1, 0, 0),
	(@FK_OsloId, GETDATE() + 10,  18.1, 2.1, 247.1, 5.4, 1024.7, 57.2, 0, 0, 0, 0, 0),
	(@FK_OsloId, GETDATE() + 11,  17.7, 2.2, 247.5, 5.1, 1025.1, 56.9, 0, 0, 0.2, 0, 0),
	(@FK_KristiansandId, GETDATE() + 12,  17.0, 4.6, 85.7, 9.4, 1023.8, 44.6, 0, 0, 0, 0, 0),
	(@FK_KristiansandId, GETDATE() + 13,  16.9, 4.5, 86.1, 9.2, 1023.7, 44.4, 0, 0, 0.1, 0, 0),
	(@FK_KristiansandId, GETDATE() + 14,  16.8, 4.4, 85.8, 9.5, 1023.5, 43.9, 0, 0, 0, 0, 0);
GO

DECLARE @DummyWeatherId INT
SELECT @DummyWeatherId = Id FROM WeatherData

DECLARE @FK_SourceYRId INT
DECLARE @FK_SourceStormId INT
DECLARE @FK_SourceFreeWeatherId INT
SELECT @FK_SourceYRId = Id FROM [Source] WHERE [Name] = 'YR'
SELECT @FK_SourceStormId = Id FROM [Source] WHERE [Name] = 'STORM'
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

DECLARE @Dummy2WeatherId INT
SELECT @Dummy2WeatherId = Id FROM WeatherData


INSERT INTO WeatherType
	(FK_WeatherId, Cloudy, Sunny, Rainy, Snowy, Stormy)
VALUES
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0),
	(@Dummy2WeatherId, 0, 1, 0, 0, 0);

UPDATE WeatherType
SET FK_WeatherId = (
	SELECT Id FROM WeatherData WHERE WeatherType.Id = WeatherData.Id
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
--SELECT * FROM WeatherType;
--SELECT * FROM [Admin];

--DECLARE @city_id INT   
--SELECT @city_id = Id FROM City AS c WHERE c.Name = 'Bergen'

--INSERT INTO WeatherData ([Date], Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId)
--VALUES ('2022-05-03', 10.3, 4.9, 318.3, 9.9, 1020.5, 48.1, 0, 0, 55.7, 0, 0, @city_id)

--INSERT INTO WeatherData ([Date], Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId)
--VALUES ('2022-05-03', 10.3, 4.9, 318.3, 9.9, 1020.5, 48.1, 0, 0, 55.7, 0, 0, @city_id)

--INSERT INTO WeatherData ([Date], Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId)
--VALUES ('2022-05-03', 10.3, 4.9, 318.3, 9.9, 1020.5, 48.1, 0, 0, 55.7, 0, 0, @city_id)

--SELECT * FROM WeatherData

ROLLBACK TRAN
--COMMIT TRAN