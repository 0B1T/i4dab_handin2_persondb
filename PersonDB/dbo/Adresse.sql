--
-- Create Table    : 'Adresse'   
-- AdresseID       :  
-- Vejnavn         :  
-- Nummer          :  
-- Postnummer      :  
-- Bynavn          :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE Adresse (
    [AdresseID]      INT IDENTITY NOT NULL,
    [Vejnavn]        VARCHAR(50) NOT NULL,
    [Nummer]          VARCHAR(50) NOT NULL,
    [Postnummer]         VARCHAR(50) NOT NULL,
    [Bynavn]         VARCHAR(50) NOT NULL,
    [PersonID]       INT NOT NULL,
CONSTRAINT pk_Adresse PRIMARY KEY CLUSTERED ([AdresseID]),
CONSTRAINT fk_Adresse FOREIGN KEY ([PersonID])
    REFERENCES Person ([PersonID])
	
    ON UPDATE CASCADE
	)