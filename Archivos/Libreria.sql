CREATE DATABASE Libreria
GO

USE Libreria
GO


CREATE TABLE Autor
(
	AutorId INT NOT NULL IDENTITY (1, 1),
	NombreAutor NVARCHAR (120) NOT NULL,
	FechaNacimiento DATETIME NOT NULL, 
	CONSTRAINT PK_Autor PRIMARY KEY (AutorId),
	CONSTRAINT UQ_NombreAutor UNIQUE (NombreAutor)
)
GO

CREATE TABLE Categoria 
(
	CategoriaId INT NOT NULL IDENTITY (1, 1),
	Genero NVARCHAR (100),
	CONSTRAINT PK_Categoria PRIMARY KEY (CategoriaId),
	CONSTRAINT UQ_Genero UNIQUE (Genero)
)
GO

CREATE TABLE Libro 
(
	LibroId INT NOT NULL IDENTITY (1, 1),
	CategoriaId INT NOT NULL,
	Titulo NVARCHAR (180) NOT NULL,
	FechaLanzamiento DATETIME NOT NULL,
	Idioma NVARCHAR (50) NOT NULL,
	Paginas INT NOT NULL,
	InformacionEditorial NVARCHAR (MAX) NOT NULL,
	Descripcion NVARCHAR (MAX) NOT NULL,
	PrecioAlquiler FLOAT NOT NULL,
	PrecioVenta FLOAT NOT NULL,
	PrecioPromocion FLOAT NULL,
	Portada VARBINARY NULL,
	CONSTRAINT PK_Libro PRIMARY KEY (LibroId),
	CONSTRAINT FK_Libro_Categoria FOREIGN KEY (CategoriaId) REFERENCES Categoria (CategoriaId),
	CONSTRAINT UQ_Titulo UNIQUE (Titulo)
)
GO

CREATE TABLE AutorLibro
(
	LibroId INT NOT NULL,
	AutorId INT NOT NULL,
	AutorPrincipal NVARCHAR (120) NOT NULL,
	LibroPrincipal NVARCHAR (180) NOT NULL,
	CONSTRAINT FK_AutorLibro_Libro FOREIGN KEY (LibroId) REFERENCES Libro (LibroId),
	CONSTRAINT FK_AutorLibro_Autor FOREIGN KEY (AutorId) REFERENCES Autor (AutorId)
)
GO

--PROCEDIMIENTOS ALMACENADOS

CREATE  PROCEDURE sp_ObtenerAutores
AS
BEGIN
	SET NOCOUNT ON 
	SELECT a.AutorId,
		   a.NombreAutor,
		   a.FechaNacimiento
	FROM Autor a
	ORDER BY a.NombreAutor
END