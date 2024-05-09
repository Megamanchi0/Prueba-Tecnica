CREATE DATABASE Logistica;

GO

USE Logistica;

GO

CREATE TABLE TipoProducto(
id int primary key identity,
nombre nvarchar(100)
)

GO

CREATE TABLE Bodega(
id int primary key,
nombre nvarchar(200),
telefono nvarchar(50),
direccion nvarchar(200)
)

GO

CREATE TABLE Puerto(
id int primary key,
nombre nvarchar(200),
telefono nvarchar(50),
direccion nvarchar(200)
)

GO

CREATE TABLE Vehiculo(
placa nvarchar(10) primary key,
tipoVehiculo nvarchar(100),
modelo nvarchar(100),
marca nvarchar(200),
capacidad decimal
)

GO

CREATE TABLE Flota(
numeroFlota nvarchar(10) primary key,
tipoFlota nvarchar(200),
capacidad nvarchar(50),
)

GO

CREATE TABLE Cliente(
documento nvarchar(100) primary key,
nombre nvarchar(100),
apellido nvarchar(100),
telefono nvarchar(20),
correo nvarchar(100)
)

GO

CREATE TABLE EnvioMaritimo(
numeroGuia nvarchar(15) primary key,
documentoCliente nvarchar(100),
idTipoProducto int,
cantidad int,
fechaRegistro date,
fechaEntrega date,
idPuerto int,
numeroFlota nvarchar(10),
precioEnvio money,
descuento money,
valorTotal money

CONSTRAINT PK_ENVIOM_CLIENTE FOREIGN KEY (documentoCliente) REFERENCES Cliente(documento),
CONSTRAINT PK_ENVIOM_TIPOPRODUCTO FOREIGN KEY (idTipoProducto) REFERENCES TipoProducto(id),
CONSTRAINT PK_ENVIOM_PUERTO FOREIGN KEY (idPuerto) REFERENCES Puerto(id),
CONSTRAINT PK_ENVIOM_FLOTA FOREIGN KEY (numeroFlota) REFERENCES Flota(numeroFlota)

)

GO

CREATE TABLE EnvioTerrestre(
numeroGuia nvarchar(15) primary key,
documentoCliente nvarchar(100),
idTipoProducto int,
cantidad int,
fechaRegistro date,
fechaEntrega date,
idBodega int,
placaVehiculo nvarchar(10),
precioEnvio money,
descuento money,
valorTotal money

CONSTRAINT PK_ENVIOT_CLIENTE FOREIGN KEY (documentoCliente) REFERENCES Cliente(documento),
CONSTRAINT PK_ENVIOT_TIPOPRODUCTO FOREIGN KEY (idTipoProducto) REFERENCES TipoProducto(id),
CONSTRAINT PK_ENVIOT_BODEGA FOREIGN KEY (idBodega) REFERENCES Bodega(id),
CONSTRAINT PK_ENVIOT_VEHICULO FOREIGN KEY (placaVehiculo) REFERENCES Vehiculo(placa)
)

GO

/* Inserción de datos necesarios para hacer el CRUD únicamente del Envío marítimo y terrestre */

INSERT INTO Bodega(id, nombre, telefono, direccion)
VALUES
(1,'Bodega principal','63453247','Cr. 4 # 43-25')

GO

INSERT INTO Puerto(id, nombre, telefono, direccion)
VALUES
(1,'Puerto principal','658796723','Cr. 6 # 41-55')

GO

INSERT INTO Cliente(documento, nombre, apellido, telefono, correo)
VALUES
('123584543','Carlos','Barboza','5784543432', 'carlos123@gmail.com'),
('476435223','Luis','Ortiz','5466854534', 'luis123@gmail.com')

GO

INSERT INTO Flota(numeroFlota, tipoFlota, capacidad)
VALUES
('FFO5234T','General',14.5),
('NRT6254F','Ligera',5)

GO

INSERT INTO Vehiculo(placa, tipoVehiculo, modelo,marca, capacidad)
VALUES
('RTX839','Furgón','2023','Mercedes-Benz',3),
('FYJ194','Camión','2012', 'Renault', 7.5)

GO

INSERT INTO TipoProducto(nombre)
VALUES
('Alimentos'),
('Productos tecnológicos')