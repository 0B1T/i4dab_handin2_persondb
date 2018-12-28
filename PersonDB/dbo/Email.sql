--
-- Create Table    : 'Email'   
-- EmailID         :  
-- Email           :  
-- PersonID        :  (references Person.PersonID)
-- Type            :  
--
CREATE TABLE Email (
    [EmailID]        INT IDENTITY NOT NULL,
    [EmailAdr]       VARCHAR(50) NOT NULL,
    [PersonID]       INT NOT NULL,
    [Type]       VARCHAR(50) NOT NULL,
CONSTRAINT pk_Email PRIMARY KEY CLUSTERED ([EmailID]),
CONSTRAINT fk_Email FOREIGN KEY ([PersonID])
    REFERENCES Person ([PersonID])
    ON UPDATE CASCADE)