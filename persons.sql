-- EonixWebApi.dbo.Persons definition

-- Drop table

-- DROP TABLE EonixWebApi.dbo.Persons;

CREATE TABLE EonixWebApi.dbo.Persons (
	Id uniqueidentifier NOT NULL,
	LastName nvarchar COLLATE French_CI_AS NOT NULL,
	FirstName nvarchar COLLATE French_CI_AS NOT NULL,
	CONSTRAINT PK_Persons PRIMARY KEY (Id)
);