--
-- Create Table    : 'Person'   
-- Fornavn         :  
-- Mellemnavn      :  
-- Efternavn       :  
-- PersonID        :  
-- Noter           :  
--
CREATE TABLE Person (
    [Fornavn]        VARCHAR(50) NOT NULL,
    [Mellemnavn]     VARCHAR(50) NULL,
    [Efternavn]      VARCHAR(50) NOT NULL,
    [PersonID]       INT IDENTITY NOT NULL,
    [Noter]          VARCHAR(200) NULL,
CONSTRAINT pk_Person PRIMARY KEY CLUSTERED ([PersonID]))