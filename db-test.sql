CREATE TABLE forcastWebSite(
    PRIMARY KEY (forcastWebSiteId),
    date timestamp,
    temperatur FLOAT,
    windspeed FLOAT
)

CREATE TABLE forcastData (
    forcastDataId int NOT NULL,
    PRIMARY KEY (forcastDataId),

    websiteName VARCHAR(255),
    url VARCHAR(255),
    autorization  VARCHAR(255)
)


CREATE TABLE forcastDataWebSite(
    forcastDataWebSiteId int NOT NULL,
    PRIMARY KEY (forcastDataWebSiteId),
    FOREIGN KEY (FK_forcastDataId) REFERENCES forcastData(forcastDataId),
    FOREIGN KEY (FK_forcastWebSiteId) REFERENCES forcastWebSite(forcastWebSiteId)
)



