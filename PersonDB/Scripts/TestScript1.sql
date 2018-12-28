--SELECT * FROM Person 
--	INNER JOIN Adresse ON Person.PersonID = Adresse.PersonID 
--	INNER JOIN Email ON Person.PersonID = Email.PersonID
--	INNER JOIN Telefon ON Person.PersonID = Telefon.PersonID



--SELECT	Person.PersonID, Person.Fornavn, Person.Mellemnavn, Person.Efternavn, Person.Noter,
--		Adresse.Vejnavn, Adresse.Nummer, Adresse.Postnummer, Adresse.Bynavn,
--		AltAdresse.Vejnavn, AltAdresse.Nummer, AltAdresse.Postnummer, AltAdresse.Bynavn, AltAdresse.Type,
--		Email.EmailAdr, Email.Type,
--		Telefon.Nummer, Telefon.Selskab, Telefon.Type
--FROM	Person INNER JOIN
--		Adresse ON Person.PersonID = Adresse.PersonID INNER JOIN
--		AltAdresse ON Person.PersonID = AltAdresse.PersonID INNER JOIN
--		Email ON Person.PersonID = Email.PersonID INNER JOIN
--		Telefon ON Person.PersonID = Telefon.PersonID

SELECT	*
FROM	Person INNER JOIN
		Adresse ON Person.PersonID = Adresse.PersonID INNER JOIN
		AltAdresse ON Person.PersonID = AltAdresse.PersonID INNER JOIN
		Email ON Person.PersonID = Email.PersonID INNER JOIN
		Telefon ON Person.PersonID = Telefon.PersonID