CREATE TABLE Categorias(
CategoriaID int IDENTITY(1,1) PRIMARY KEY,
Nombre nvarchar(80) NOT NULL);

CREATE TABLE Tipos(
TipoID int IDENTITY(1,1) PRIMARY KEY,
Nombre nvarchar(80) NOT NULL);

CREATE TABLE Estado(
EstadoID int IDENTITY(1,1) PRIMARY KEY,
Descripcion nvarchar(20) NOT NULL);


CREATE TABLE Comensales(
ComensalID int IDENTITY(1,1) PRIMARY KEY,
Nombre nvarchar(80) NOT NULL,
Apellido nvarchar(80) NOT NULL,
Email nvarchar(200) NOT NULL);


CREATE TABLE FoodImages(
ID int IDENTITY(1,1),
NameFile nvarchar(120),
CONSTRAINT PK_FoodImages PRIMARY KEY(ID));


CREATE TABLE Alimentos(
ID int IDENTITY(1,1) PRIMARY KEY,
Nombre nvarchar(80) NOT NULL,
CategoriaID int NOT NULL,
Precio numeric(6,2) NOT NULL,
tipoID int NOT NULL,
CONSTRAINT FK_Categories_Alimentos FOREIGN KEY(CategoriaID) REFERENCES Categorias(CategoriaID),
CONSTRAINT FK_Tipos_Alimentos FOREIGN KEY(tipoID) REFERENCES Tipos(tipoID));


CREATE TABLE Ordenes(
OrdenID int IDENTITY(1,1) PRIMARY KEY,
ComensalID int  NOT NULL,
EstadoID int  NOT NULL,
CONSTRAINT FK_Ordenes_Comensal FOREIGN KEY(ComensalID) REFERENCES Comensales(ComensalID),
CONSTRAINT FK_Ordenes_Estado FOREIGN KEY(EstadoID) REFERENCES Estado(EstadoID));


CREATE TABLE Menu(
MenuID int IDENTITY(1,1) PRIMARY KEY,
OrdenID int,
sopaID int,
platoFuerteID int,
bebidaID int,
postreID int,
complementoID int,
bocadilloID int,
CONSTRAINT FK_menu_ordenes FOREIGN KEY(OrdenID) REFERENCES Ordenes(OrdenID)
                                                ON DELETE CASCADE,
CONSTRAINT FK_menu_sopa FOREIGN KEY(sopaID) REFERENCES Alimentos(ID),
CONSTRAINT FK_menu_platoFuerte FOREIGN KEY(platoFuerteID) REFERENCES Alimentos(ID),
CONSTRAINT FK_menu_bebida FOREIGN KEY(bebidaID) REFERENCES Alimentos(ID),
CONSTRAINT FK_menu_postre FOREIGN KEY(postreID) REFERENCES Alimentos(ID),
CONSTRAINT FK_menu_complemento FOREIGN KEY(complementoID) REFERENCES Alimentos(ID),
CONSTRAINT FK_menu_bocadillo FOREIGN KEY(bocadilloID) REFERENCES Alimentos(ID));


CREATE TABLE FoodImageMapping(
ID int IDENTITY(1,1),
AlimentosID int,
ImageNumber int
AlimentosImageID int,
CONSTRAINT PK_FoodImageMapping PRIMARY KEY(ID),
CONSTRAINT FK_FoodImageMapping_Alimentos FOREIGN KEY(AlimentosID) REFERENCES Alimentos(ID)
                                                                  ON DELETE CASCADE,
CONSTRAINT FK_FOODImageMapping_FoodImage FOREIGN KEY(AlimentosImageID) REFERENCES FoodImages(ID));




Scaffold-DbContext "Data Source=DESKTOP-QU97L6I\SQLEXPRESS;Initial Catalog=HungryDb;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models






