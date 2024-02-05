
-- ╔═══════════════════════════════════════════════════════ AEROPUERTOS AMERICANOS ═══════════════════════════════════════════════════════╗ --
use master
go
-- ╔═══════════════════════════════════════════════════════ CREACIÓN DE LA BASE DE DATOS ═══════════════════════════════════════════════════════╗ --
-- Determina si está la base de datos Obligatorio --
if exists(select * from SysDataBases where name = 'AeropuertosAmericanos')
begin
	-- Borra la base de datos --
	drop database AeropuertosAmericanos
end
go

-- Crea la base de datos Obligatorio --
create database AeropuertosAmericanos
go

use AeropuertosAmericanos
go

-- ╔═══════════════════════════════════════════════════════ TABLAS BASE DE DATOS ═══════════════════════════════════════════════════════╗ --
-- EMPLEADO --
CREATE TABLE EMPLEADO
(
    usuario VARCHAR(15) PRIMARY KEY,
    nombreCompleto VARCHAR(30) NOT NULL,
    contrasenia VARCHAR(15) NOT NULL,
    labor VARCHAR(15) NOT NULL CHECK (labor IN ('GERENTE', 'VENDEDOR', 'ADMIN'))
)
GO

-- CLIENTE --
CREATE TABLE CLIENTE
(
	numeroPasaporte varchar(15) PRIMARY KEY,
	nombre varchar(30) NOT NULL,
	numeroTarjeta varchar(15) NOT NULL,
	contrasenia varchar(15) NOT NULL
)
GO

-- CIUDADES --
CREATE TABLE CIUDAD
(
    codigo VARCHAR(6) PRIMARY KEY CHECK (codigo LIKE '[A-Za-z][A-Za-z][A-Za-z][A-Za-z][A-Za-z][A-Za-z]'),
    nombre VARCHAR(30) NOT NULL,
    pais VARCHAR(30) NOT NULL
);


-- AEROPUERTO --
CREATE TABLE AEROPUERTO
(
    codigo VARCHAR(3) PRIMARY KEY CHECK (codigo LIKE '[A-Za-z][A-Za-z][A-Za-z]'),
    nombre VARCHAR(30),
    direccion VARCHAR(30),
    impuestoPartida DECIMAL NOT NULL,
    impuestoLlegada DECIMAL NOT NULL,
    idCiudad VARCHAR (6),

    FOREIGN KEY (idCiudad) REFERENCES CIUDAD (codigo),
    CHECK (impuestoPartida >= 0 AND impuestoLlegada >= 0)
)
GO

-- VUELO --
CREATE TABLE VUELO
(
    codigo VARCHAR(15) PRIMARY KEY,
    fecHorSalida DATETIME NOT NULL,
    fecHorLlegada DATETIME NOT NULL,
    precio DECIMAL(10, 2) NOT NULL,
    asientos INT CHECK (asientos BETWEEN 100 AND 300),
    idAeropuertoSalida VARCHAR(3) NOT NULL,
    idAeropuertoLlegada VARCHAR(3) NOT NULL,
    
    FOREIGN KEY (idAeropuertoSalida) REFERENCES AEROPUERTO(codigo),
    FOREIGN KEY (idAeropuertoLlegada) REFERENCES AEROPUERTO(codigo),
	CHECK(fecHorSalida < fecHorLlegada)
)
GO


-- PASAJE --
CREATE TABLE PASAJE
(
    numeroPasaje INT IDENTITY(1,1) PRIMARY KEY,
    fechaCompra DATETIME DEFAULT GETDATE() NOT NULL,
    precio DECIMAL(10, 2) NOT NULL,
    idCliente VARCHAR(15) NOT NULL,
    idVuelo VARCHAR(15) NOT NULL,
    
    FOREIGN KEY (idCliente) REFERENCES CLIENTE(numeroPasaporte),
    FOREIGN KEY (idVuelo) REFERENCES VUELO(codigo)
)
GO

-- ╔════════════════════════════════════════════════════ LOGUEOS ════════════════════════════════════════════════════╗ --
-- EMPLEADO --
CREATE PROCEDURE LogueoEmpleado
    @usuario VARCHAR(15),
    @contrasenia VARCHAR(15)
AS
BEGIN
    SELECT * FROM EMPLEADO 
	WHERE usuario = @usuario
    AND contrasenia = @contrasenia
END
GO

-- CLIENTE --
CREATE PROCEDURE LogueoCliente
	@numPasaporte VARCHAR(15),
	@contrasenia VARCHAR(15)
AS
BEGIN
	SELECT * FROM CLIENTE 
	WHERE numeroPasaporte = @numPasaporte
	AND contrasenia = @contrasenia
END
GO

-- ╔═══════════════════════════════════════════════════ CIUDADES ═══════════════════════════════════════════════════╗ --
-- ALTA --
CREATE PROCEDURE AltaCiudad
	@codigo VARCHAR(6),
	@nombre VARCHAR(30),
	@pais VARCHAR(30)
AS
BEGIN
	IF EXISTS (SELECT * FROM CIUDAD WHERE codigo = @codigo)
		RETURN -1 -- Ya existe una ciudad

	INSERT INTO CIUDAD (codigo, nombre, pais) VALUES (@codigo, @nombre, @pais)
		IF @@ERROR <> 0
			RETURN -2 -- Error inesperado

	RETURN 1
END
GO

-- MODIFICAR --
CREATE PROCEDURE ModificarCiudad
	@codigo VARCHAR(6),
	@nombre VARCHAR(30),
	@pais VARCHAR(30)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM CIUDAD WHERE codigo = @codigo)
		RETURN -1 -- No existe la ciudad

	UPDATE CIUDAD
	SET nombre = @nombre, pais = @pais
	WHERE codigo = @codigo
		IF @@ERROR <> 0
			RETURN -2
END
GO

-- BAJA --
CREATE PROCEDURE BajaCiudad
    @codigo VARCHAR(6)
AS
BEGIN

    IF NOT EXISTS (SELECT * FROM CIUDAD WHERE codigo = @codigo)
        RETURN -1  -- Ciudad no encontrada

    IF EXISTS (SELECT * FROM AEROPUERTO WHERE idCiudad = @codigo)
        RETURN -3  -- Ciudad con aeropuertos asociados, no elimina

    -- Eliminar la ciudad
    DELETE FROM CIUDAD WHERE codigo = @codigo

    IF @@ERROR <> 0
        RETURN -2  -- Error en la eliminación
END
GO

-- Buscar Ciudad --
CREATE PROCEDURE BuscarCiudadPorCodigo
	@codigo VARCHAR(06)
AS
BEGIN
	SELECT *
	FROM CIUDAD
	WHERE codigo = @codigo;
END
GO

CREATE PROCEDURE ListarCiudad
AS
BEGIN
	SELECT *
	FROM CIUDAD
END
GO

-- ╔═══════════════════════════════════════════════════ AEROPUERTOS ═══════════════════════════════════════════════════╗ --
-- ALTA --
CREATE PROCEDURE AltaAeropuerto
    @codigo VARCHAR(3),
    @nombre VARCHAR(30),
    @direccion VARCHAR(30),
    @idCiudad VARCHAR(6),
    @impuestoPartida INT,
    @impuestoLlegada INT
AS
BEGIN
    IF EXISTS (SELECT * FROM AEROPUERTO WHERE codigo = @codigo)
        RETURN -1  -- Código de aeropuerto ya existe

    INSERT INTO AEROPUERTO (codigo, nombre, direccion, idCiudad, impuestoPartida, impuestoLlegada)
    VALUES (@codigo, @nombre, @direccion, @idCiudad, @impuestoPartida, @impuestoLlegada)

    IF @@ERROR <> 0
        RETURN -2 
END
GO

-- MODIFICACIÓN --
CREATE PROCEDURE ModificarAeropuerto
    @codigo VARCHAR(3),
    @nombre VARCHAR(30),
    @direccion VARCHAR(30),
    @idCiudad VARCHAR(6),
    @impuestoPartida INT,
    @impuestoLlegada INT
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM AEROPUERTO WHERE codigo = @codigo)
        RETURN -1  -- Aeropuerto no encontrado

    UPDATE AEROPUERTO
    SET nombre = @nombre, direccion = @direccion, idCiudad = @idCiudad,
        impuestoPartida = @impuestoPartida, impuestoLlegada = @impuestoLlegada
    WHERE codigo = @codigo

    IF @@ERROR <> 0
        RETURN -2
END
GO

-- BAJA --
CREATE PROCEDURE BajaAeropuerto
    @codigo VARCHAR(3)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM AEROPUERTO WHERE codigo = @codigo)
        RETURN -1  -- Aeropuerto no encontrado

    IF EXISTS (SELECT * FROM VUELO WHERE idAeropuertoSalida = @codigo)
        RETURN -3  -- Aeropuertos con vuelos asociados, no elimina

    DELETE FROM AEROPUERTO WHERE codigo = @codigo

    IF @@ERROR <> 0
        RETURN -2  -- Error en la eliminación
END
GO

CREATE PROCEDURE ListarAeropuertos
AS
BEGIN
	SELECT * FROM AEROPUERTO;
END
GO

CREATE PROCEDURE ListarAeropuertosConVuelos
	@idCiudad varchar(6)
AS
BEGIN
	SELECT *
	FROM AEROPUERTO
	WHERE idCiudad = @idCiudad
END
GO

CREATE PROCEDURE BuscarAeropuertoPorCodigo
	@codigo VARCHAR(3)
AS
BEGIN
	SELECT *
	FROM AEROPUERTO
	WHERE codigo = @codigo;
END
GO

-- ╔═══════════════════════════════════════════════════ VUELOS ═══════════════════════════════════════════════════╗ --
-- BUSCAR --
CREATE PROCEDURE BuscarVueloPorCodigo
    @codigo VARCHAR(15)
AS
BEGIN
    SELECT *
    FROM VUELO
    WHERE codigo = @codigo;
END
GO

-- ALTA DE VUELO --
create PROCEDURE AltaVuelo
    @fecHorSalida DATETIME,
    @fecHorLlegada DATETIME,
    @idAeropuertoSalida VARCHAR(3),
    @idAeropuertoLlegada VARCHAR(3),
    @precio DECIMAL(10, 2),
    @asientos INT,
    @codigoVuelo VARCHAR(15) OUTPUT
AS
BEGIN
    -- Generando el código del vuelo
    SET @codigoVuelo = CONVERT(VARCHAR(8), @fecHorSalida, 112) +
                      FORMAT(@fecHorSalida, 'HHmm') +
                      @idAeropuertoSalida;

    -- Insertar el vuelo
    INSERT INTO VUELO (codigo, fecHorSalida, fecHorLlegada, precio, asientos, idAeropuertoSalida, idAeropuertoLlegada)
    VALUES (@codigoVuelo, @fecHorSalida, @fecHorLlegada, @precio, @asientos, @idAeropuertoSalida, @idAeropuertoLlegada);

    IF @@ERROR <> 0
        RETURN -1;  -- Error en la inserción

    RETURN 1;  -- Operación exitosa
END
GO

-- PARTIDAS --
CREATE PROCEDURE ObtenerPartidas
    @codigoAeropuerto VARCHAR(3)
AS
BEGIN
    SELECT
        VUELO.codigo AS CodigoVuelo,
        VUELO.fecHorSalida AS FechaHoraPartida,
        AEROPUERTO.nombre AS AeropuertoDestino
    FROM
        VUELO
    INNER JOIN
        AEROPUERTO ON VUELO.idAeropuertoLlegada = AEROPUERTO.codigo
    WHERE
        VUELO.idAeropuertoSalida = @codigoAeropuerto
        AND VUELO.fecHorSalida > GETDATE()  -- Vuelos que aún no han partido
    ORDER BY
        VUELO.fecHorSalida;
END
GO

-- ARRIBOS --
CREATE PROCEDURE ObtenerArribos
    @codigoAeropuerto VARCHAR(3)
AS
BEGIN
    SELECT
        VUELO.codigo AS CodigoVuelo,
        VUELO.fecHorLlegada AS FechaHoraLlegada,
        AEROPUERTO.nombre AS AeropuertoOrigen
    FROM
        VUELO
    INNER JOIN
        AEROPUERTO ON VUELO.idAeropuertoSalida = AEROPUERTO.codigo
    WHERE
        VUELO.idAeropuertoLlegada = @codigoAeropuerto
        AND VUELO.fecHorLlegada > GETDATE()  -- Vuelos que aún no han llegado
    ORDER BY
        VUELO.fecHorLlegada;
END
GO

-- LISTAR VUELOS
CREATE PROCEDURE ListarVuelos
AS
BEGIN
	SELECT *
	FROM VUELO
END
GO

-- ╔═══════════════════════════════════════════════════ CLIENTE ═══════════════════════════════════════════════════╗ --
-- BUSCAR CLIENTE
CREATE PROCEDURE BuscarCliente
	@pasaporte VARCHAR(15)
AS
BEGIN
	SELECT *
	FROM CLIENTE
	WHERE numeroPasaporte = @pasaporte
END
GO

-- ALTA CLIENTE
CREATE PROCEDURE AltaCliente
    @pasaporte VARCHAR(15),
    @nombre VARCHAR(30),
    @tarjeta VARCHAR(15),
    @contrasenia VARCHAR(15)
AS
BEGIN
    IF EXISTS (SELECT * FROM CLIENTE WHERE numeroPasaporte = @pasaporte)
        RETURN -1  -- Cliente ya existe

    INSERT INTO CLIENTE(numeroPasaporte, nombre, numeroTarjeta, contrasenia)
    VALUES (@pasaporte, @nombre, @tarjeta, @contrasenia)

    IF @@ERROR <> 0
        RETURN -2  -- Error en la inserción

    RETURN 1  -- Operación exitosa
END
GO

-- MODIFICAR CLIENTE
CREATE PROCEDURE ModificarCliente
    @pasaporte VARCHAR(15),
    @nombre VARCHAR(30),
    @tarjeta VARCHAR(15),
    @contrasenia VARCHAR(15)
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM CLIENTE WHERE numeroPasaporte = @pasaporte)
		RETURN -1

	UPDATE CLIENTE
	SET nombre = @nombre, numeroTarjeta = @tarjeta, contrasenia = @contrasenia
	WHERE numeroPasaporte = @pasaporte
		IF @@ERROR <> 0
			RETURN -2
END
GO

-- BAJA CLIENTE
CREATE PROCEDURE BajaCliente
    @numeroPasaporte VARCHAR(15)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE numeroPasaporte = @numeroPasaporte)
        RETURN -1  -- Cliente no encontrado

    IF EXISTS (SELECT * FROM PASAJE WHERE idCliente = @numeroPasaporte)
        RETURN -3  -- Cliente con pasajes asociados

    DELETE FROM CLIENTE WHERE numeroPasaporte = @numeroPasaporte

    IF @@ERROR <> 0
        RETURN -2  -- Error en la eliminación

    RETURN 1  -- Operación exitosa
END
GO

-- LISTAR CLIENTE
CREATE PROCEDURE ListarCliente
AS
BEGIN
	SELECT *
	FROM CLIENTE
END
GO

-- ╔═══════════════════════════════════════════════════ PASAJE ═══════════════════════════════════════════════════╗ --
-- VENTA DE PASAJE
CREATE PROCEDURE AltaPasaje
    @codigoVuelo VARCHAR (15),
    @numeroPasaporteCliente VARCHAR(15),
    @precioTotal DECIMAL(10,2)
AS
BEGIN
    -- Verificar existencia del cliente
    IF NOT EXISTS (SELECT * FROM CLIENTE WHERE numeroPasaporte = @numeroPasaporteCliente)
        RETURN -2

    -- Verificar existencia del vuelo
    IF NOT EXISTS (SELECT * FROM VUELO WHERE codigo = @codigoVuelo)
        RETURN -1

    -- Verificar asientos disponibles
    DECLARE @cantidadAsientos INT
    DECLARE @asientosOcupados INT

    SELECT @cantidadAsientos = asientos FROM VUELO WHERE codigo = @codigoVuelo
    SELECT @asientosOcupados = COUNT(*) FROM PASAJE WHERE idVuelo = @codigoVuelo

    IF (@cantidadAsientos - @asientosOcupados) <= 0
        RETURN -3  -- No hay asientos disponibles

    INSERT INTO PASAJE (fechaCompra, precio, idCliente, idVuelo)
    VALUES (GETDATE(), @precioTotal, @numeroPasaporteCliente, @codigoVuelo)

    DECLARE @numeroPasaje INT
    SET @numeroPasaje = SCOPE_IDENTITY()

    RETURN @numeroPasaje
END
GO

-- LISTAR PASAJE -- 
CREATE PROCEDURE ListarPasaje
AS
BEGIN
	SELECT * 
	FROM PASAJE
END
GO

-- ╔═══════════════════════════════════════════════════ HISTÓRICO DE COMPRAS ═══════════════════════════════════════════════════╗ --
CREATE PROCEDURE HistoricoCliente
    @pasaporteCliente VARCHAR(15)
AS
BEGIN
    SELECT
        P.numeroPasaje AS NumeroPasaje,
        P.fechaCompra AS FechaCompra,
        P.idCliente AS NumeroPasaporte,
        V.codigo AS CodigoVuelo,
        A1.nombre AS AeropuertoSalida,
        A2.nombre AS AeropuertoLlegada,
        V.precio AS MontoPasaje
    FROM
        PASAJE P
    JOIN
        VUELO V ON P.idVuelo = V.codigo
    JOIN
        AEROPUERTO A1 ON V.idAeropuertoSalida = A1.codigo
    JOIN
        AEROPUERTO A2 ON V.idAeropuertoLlegada = A2.codigo
    WHERE
        P.idCliente = @pasaporteCliente
    ORDER BY
        P.fechaCompra DESC;
END
GO

-- ╔═══════════════════════════════════════════════════ DATOS DE PRUEBA ═══════════════════════════════════════════════════╗ --
-- EMPLEADO -- 
INSERT EMPLEADO VALUES ('Usu1', 'Alexander Lemos', '11111', 'ADMIN'),
						('Usu2', 'Anderson Bruno', '22222', 'VENDEDOR'),
						('Usu3', 'Sofia Meneses', '33333', 'GERENTE')
GO

-- CLIENTE --
INSERT CLIENTE VALUES ('1000', 'Mathias Techera', '11345221', '12121'),
					  ('1001', 'Florencia Fernández', '11344421', '13131'),
					  ('1002', 'Ramiro Rodriguez', '13345221', '14141')
GO

-- CIUDADES --
INSERT CIUDAD VALUES ('MVDURU', 'Montevideo', 'Uruguay'),
                     ('POABRA', 'Porto Alegre', 'Brasil'),
                     ('SPLBRA', 'San Pablo', 'Brasil'),
					 ('RIVURU', 'Montevideo', 'Uruguay'),
					 ('BRABRA', 'Brasilia', 'Brasil'),
					 ('SUCBOL', 'Sucre', 'Bolivia'),
					 ('SANCHI', 'Santiago de Chile', 'Chile'),
					 ('BOGCOL', 'Bogotá', 'Colombia')
GO

-- INSERTAR AEROPEURTOS --
insert AEROPUERTO VALUES ('MVD', 'Aeropuerto de Carrasco', 'Ruta 101', 12, 200, 'MVDURU'),
                         ('POA', 'Aeroporto Beira Rio', 'Odilo Goncalves 1200', 150, 300, 'POABRA'),
                         ('SPL', 'Aeropuerto San Pablo', 'Rua Uruguai 228' ,980, 22, 'SPLBRA'),
						 ('BRA', ' Juscelino Kubitschek', 'Lago Sul, Brasília - DF 1121', 200, 220, 'BRABRA'),
						 ('SUC', 'Aeropuerto de Alcantarí', 'Yamparaez 1500', 100, 130, 'SUCBOL'),
						 ('SAN', 'Arturo Merino Benítez', 'Armando Cortínez 1704', 440, 390, 'SANCHI'),
						 ('BOG', 'Aeropuerto de El Dorado', 'Av. El Dorado 103', 300 , 310, 'BOGCOL')
GO

-- INSERTAR VUELOS CORREGIDOS --
INSERT INTO VUELO VALUES
						('202402011201MVD', '2024-02-01 12:01', '2024-02-02 12:01', 1200, 180, 'MVD', 'BOG'), -- Montevideo a Bogotá
						('202402021201BOG', '2024-02-02 12:01', '2024-02-03 12:01', 1200, 200, 'BOG', 'SPL'), -- Bogotá a San Pablo
						('202402031201SPL', '2024-02-03 12:01', '2024-02-04 12:01', 1200, 220, 'SPL', 'MVD'), -- San Pablo a Montevideo (corregido)
						('202402041201MVD', '2024-02-04 12:01', '2024-02-05 12:01', 1200, 250, 'MVD', 'SAN'), -- Montevideo a Santiago de Chile
						('202402051201SAN', '2024-02-05 12:01', '2024-02-06 12:01', 1200, 280, 'SAN', 'POA'), -- Santiago de Chile a Porto Alegre
						('202402061201POA', '2024-02-06 12:01', '2024-02-07 12:01', 1200, 300, 'POA', 'MVD'), -- Porto Alegre a Montevideo
						('202402071201MVD', '2024-02-07 12:01', '2024-02-08 12:01', 1200, 200, 'MVD', 'SPL'), -- Montevideo a San Pablo
						('202402081201SPL', '2024-02-08 12:01', '2024-02-09 12:01', 1200, 220, 'SPL', 'POA'), -- San Pablo a Porto Alegre
						('202402091201POA', '2024-02-09 12:01', '2024-02-10 12:01', 1200, 250, 'POA', 'SAN'), -- Porto Alegre a Santiago de Chile
						('202402101201SAN', '2024-02-10 12:01', '2024-02-11 12:01', 1200, 280, 'SAN', 'MVD'); -- Santiago de Chile a Montevideo
GO


--exec ListarAeropuertos

--exec ListarCiudad

--select * from PASAJE

--select * from CLIENTE

-- select * from VUELO