USE [master]
GO
/****** Object:  Database [ProyectoBD]    Script Date: 11/14/2023 10:14:37 PM ******/
CREATE DATABASE [ProyectoBD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoBD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoBD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProyectoBD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoBD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProyectoBD] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoBD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoBD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoBD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoBD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoBD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoBD] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoBD] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProyectoBD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoBD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoBD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoBD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoBD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoBD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoBD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoBD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoBD] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProyectoBD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoBD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoBD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoBD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoBD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoBD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProyectoBD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoBD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProyectoBD] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoBD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoBD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoBD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoBD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProyectoBD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProyectoBD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProyectoBD] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProyectoBD] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProyectoBD]
GO
/****** Object:  Table [dbo].[TProvincia]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TProvincia](
	[ConProvincia] [bigint] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TProvincia] PRIMARY KEY CLUSTERED 
(
	[ConProvincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TRol]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TRol](
	[ConRol] [bigint] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TRol] PRIMARY KEY CLUSTERED 
(
	[ConRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TUsuario]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TUsuario](
	[IdUsuario] [bigint] IDENTITY(1,1) NOT NULL,
	[identificacion] [varchar](20) NOT NULL,
	[nombre] [varchar](200) NOT NULL,
	[usuario] [varchar](25) NOT NULL,
	[correo] [varchar](50) NOT NULL,
	[contrasenna] [varchar](25) NOT NULL,
	[estado] [bit] NOT NULL,
	[esClaveTemp] [bit] NOT NULL,
	[vencimientoClaveTemp] [datetime] NOT NULL,
	[ConRol] [bigint] NOT NULL,
	[ConProvincia] [bigint] NOT NULL,
 CONSTRAINT [PK_TUsuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TUsuario]  WITH CHECK ADD  CONSTRAINT [FK_TUsuario_TProvincia] FOREIGN KEY([ConProvincia])
REFERENCES [dbo].[TProvincia] ([ConProvincia])
GO
ALTER TABLE [dbo].[TUsuario] CHECK CONSTRAINT [FK_TUsuario_TProvincia]
GO
ALTER TABLE [dbo].[TUsuario]  WITH CHECK ADD  CONSTRAINT [FK_TUsuario_TRol] FOREIGN KEY([ConRol])
REFERENCES [dbo].[TRol] ([ConRol])
GO
ALTER TABLE [dbo].[TUsuario] CHECK CONSTRAINT [FK_TUsuario_TRol]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarClaveTemporal]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarClaveTemporal]
	@IdUsuario				bigint,
	@contrasennaTemporal	varchar(4)
AS
BEGIN

	UPDATE dbo.TUsuario
	SET contrasenna = @contrasennaTemporal,
		esClaveTemp = 1,
        vencimientoClaveTemp = DATEADD(mi,30,GETDATE())
	WHERE IdUsuario = @IdUsuario

END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCuenta]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarCuenta]
	@identificacion varchar(20),
	@nombre varchar(200),
	@usuario varchar(25),
	@correo varchar(50),
	@ConProvincia bigint,
	@IdUsuario bigint
AS
BEGIN
	
	UPDATE	TUsuario
	   SET	identificacion = @identificacion,
			nombre = @nombre,
			usuario = @usuario,
			correo = @correo,
			ConProvincia = @ConProvincia
	 WHERE	IdUsuario = @IdUsuario

END
GO
/****** Object:  StoredProcedure [dbo].[CambiarClaveCuenta]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CambiarClaveCuenta]
	@IdUsuario				bigint,
	@contrasennaTemporal	varchar(4),
	@contrasenna			varchar(25)
AS
BEGIN

	IF EXISTS(SELECT IdUsuario
			  FROM	 dbo.TUsuario
			  WHERE	IdUsuario = @IdUsuario
				AND contrasenna = @contrasennaTemporal
				AND esClaveTemp = 1
				AND vencimientoClaveTemp >= GETDATE())
	BEGIN

		UPDATE dbo.TUsuario
		SET contrasenna = @contrasenna,
			esClaveTemp = 0,
			vencimientoClaveTemp = GETDATE()
		WHERE IdUsuario = @IdUsuario

	END

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarProvincias]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarProvincias]
	
AS
BEGIN

	SELECT	ConProvincia 'Value',
			Descripcion 'Text'
	FROM	dbo.TProvincia

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuario]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarUsuario]
	@IdUsuario BIGINT
AS
BEGIN
	
	SELECT IdUsuario,
		   identificacion,
		   nombre,
		   usuario,
		   correo,
		   estado,
		   ConRol,
		   ConProvincia
	  FROM dbo.TUsuario
	  WHERE IdUsuario = @IdUsuario

END
GO
/****** Object:  StoredProcedure [dbo].[ConsultarUsuarios]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarUsuarios]

AS
BEGIN
	
	SELECT IdUsuario,
		   identificacion,
		   nombre,
		   usuario,
		   correo,
		   estado,
		   ConRol,
		   ConProvincia
	  FROM dbo.TUsuario

END
GO
/****** Object:  StoredProcedure [dbo].[IniciarSesion]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IniciarSesion]
	@usuario		VARCHAR(25),
	@contrasenna	VARCHAR(25)
AS
BEGIN

	SELECT	IdUsuario, nombre, identificacion, correo, usuario, estado
	FROM	TUsuario
	WHERE	(usuario = @usuario OR correo = @usuario)
		AND contrasenna = @contrasenna
		AND estado		= 1
		AND esClaveTemp = 0

END
GO
/****** Object:  StoredProcedure [dbo].[RecuperarCuenta]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RecuperarCuenta]
	@usuario		VARCHAR(25)
AS
BEGIN
	
	SELECT	IdUsuario, nombre, correo
	FROM	TUsuario
	WHERE	(usuario = @usuario OR correo = @usuario)
		AND estado		= 1

END
GO
/****** Object:  StoredProcedure [dbo].[RegistrarCuenta]    Script Date: 11/14/2023 10:14:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegistrarCuenta]
	@identificacion varchar(20),
	@nombre			varchar(200),
	@usuario		varchar(25),
	@correo			varchar(50),
	@contrasenna	varchar(25)
AS
BEGIN
	
	INSERT INTO TUsuario (identificacion,nombre,usuario,correo,contrasenna,estado,esClaveTemp,vencimientoClaveTemp,ConRol,ConProvincia)
    VALUES (@identificacion,@nombre,@usuario,@correo,@contrasenna,1,0,GETDATE(),2,0)

END
GO
USE [master]
GO
ALTER DATABASE [ProyectoBD] SET  READ_WRITE 
GO
