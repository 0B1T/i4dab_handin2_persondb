--
-- Create Table    : 'Telefon'   
-- TelefonID       :  
-- Type            :  
-- Nummer          :  
-- Selskab         :  
-- PersonID        :  (references Person.PersonID)
--
CREATE TABLE Telefon (
    [TelefonID]      INT IDENTITY NOT NULL,
    [Type]    VARCHAR(50) NULL,
    [Nummer]         VARCHAR(50) NOT NULL,
    [Selskab]        VARCHAR(50) NOT NULL,
    [PersonID]       INT NOT NULL,
CONSTRAINT pk_Telefon PRIMARY KEY CLUSTERED ([TelefonID]),
CONSTRAINT fk_Telefon FOREIGN KEY ([PersonID])
    REFERENCES Person ([PersonID])
    ON UPDATE CASCADE)