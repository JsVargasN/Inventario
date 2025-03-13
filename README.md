# Inventario

***IMPORTANTE***
El proyecto debe tener instalado Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Design

**BASE DE DATOS:** *Insertar en el SqlServer*


CREATE DATABASE bd_inventario;
GO

USE bd_inventario;
GO

CREATE TABLE Categorias (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Productos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    CategoriaId INT NOT NULL,
    CONSTRAINT FK_Productos_Categorias FOREIGN KEY (CategoriaId)
        REFERENCES Categorias(Id)
);
GO

-- Insertar categorías
INSERT INTO Categorias (Nombre) VALUES ('Electrónica');
INSERT INTO Categorias (Nombre) VALUES ('Ropa');
GO

-- Insertar productos
INSERT INTO Productos (Nombre, Precio, Stock, CategoriaId) 
VALUES ('Televisor', 499.99, 10, 1);
INSERT INTO Productos (Nombre, Precio, Stock, CategoriaId) 
VALUES ('Camiseta', 19.99, 50, 2);
GO

------------//--------------------------------//------------------------------------

En el archivo appsettings.json cambiar la cadena de conexión por la que aparece en su SQL SERVER

"ConnectionStrings": {
    "DefaultConnection": "Server=*TU_SERVIDOR*;Database=bd_inventario;Trusted_Connection=True;MultipleActiveResultSets=true; Encrypt=False"
},
