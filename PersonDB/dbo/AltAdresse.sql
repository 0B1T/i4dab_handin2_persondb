--
-- Create Table    : 'AltAdresse'   
-- AltAdresseID    :  
-- Vejnavn         :  
-- Nummer          :  
-- Postnummer      :  
-- Bynavn          :  
-- Type            :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE AltAdresse (
    [AltAdresseID]       INT IDENTITY NOT NULL,
    [Vejnavn]        VARCHAR(50) NOT NULL,
    [Nummer]          VARCHAR(50) NOT NULL,
    [Postnummer]         VARCHAR(50) NOT NULL,
    [Bynavn]         VARCHAR(50) NOT NULL,
    [Type]    VARCHAR(50) NOT NULL,
    [PersonID]       INT NOT NULL,
CONSTRAINT pk_AltAdresse PRIMARY KEY CLUSTERED ([AltAdresseID]),
CONSTRAINT fk_AltAdresse FOREIGN KEY ([PersonID])
    REFERENCES Person ([PersonID])
    ON UPDATE CASCADE)