CREATE DATABASE qulixtest
GO

USE qulixtest;
GO

CREATE TABLE Company (
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	Title nvarchar(255),
	Size int,
	TypeOfBusiness nvarchar(255)
);
GO

CREATE TABLE Worker (
	ID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	FirstName nvarchar(255),
	LastName nvarchar(255),
	Patronymic nvarchar(255),
	Position nvarchar(255),
	EmploymentDate date,
	CompanyId int FOREIGN KEY REFERENCES Company(Id)
);
GO