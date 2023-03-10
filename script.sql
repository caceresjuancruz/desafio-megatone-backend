USE [master]
GO
/****** Object:  Database [DBPRODUCTOS]    Script Date: 31/1/2023 00:51:35 ******/
CREATE DATABASE [DBPRODUCTOS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBPRODUCTOS', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DBPRODUCTOS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBPRODUCTOS_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\DBPRODUCTOS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DBPRODUCTOS] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBPRODUCTOS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBPRODUCTOS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBPRODUCTOS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBPRODUCTOS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBPRODUCTOS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBPRODUCTOS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBPRODUCTOS] SET  MULTI_USER 
GO
ALTER DATABASE [DBPRODUCTOS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBPRODUCTOS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBPRODUCTOS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBPRODUCTOS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBPRODUCTOS] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBPRODUCTOS] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBPRODUCTOS] SET QUERY_STORE = ON
GO
ALTER DATABASE [DBPRODUCTOS] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DBPRODUCTOS]
GO
/****** Object:  Table [dbo].[Familia]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Familia](
	[IdFamilia] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
	[Baja] [bit] NOT NULL,
	[FechaBaja] [datetime] NULL,
 CONSTRAINT [PK_Familia] PRIMARY KEY CLUSTERED 
(
	[IdFamilia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marca]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marca](
	[IdMarca] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
	[Baja] [bit] NOT NULL,
	[FechaBaja] [datetime] NULL,
 CONSTRAINT [PK_Marca] PRIMARY KEY CLUSTERED 
(
	[IdMarca] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[CodigoProducto] [nvarchar](50) NOT NULL,
	[DescripcionProducto] [nvarchar](50) NOT NULL,
	[PrecioCosto] [decimal](18, 2) NOT NULL,
	[PrecioVenta] [decimal](18, 2) NOT NULL,
	[IdMarca] [int] NOT NULL,
	[IdFamilia] [int] NOT NULL,
	[FechaModificacion] [datetime] NOT NULL,
	[Baja] [bit] NOT NULL,
	[FechaBaja] [datetime] NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[CodigoProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Familia] ADD  CONSTRAINT [DF_Familia_Baja]  DEFAULT ((0)) FOR [Baja]
GO
ALTER TABLE [dbo].[Marca] ADD  CONSTRAINT [DF_Marca_Baja]  DEFAULT ((0)) FOR [Baja]
GO
ALTER TABLE [dbo].[Productos] ADD  CONSTRAINT [DF_Productos_Baja]  DEFAULT ((0)) FOR [Baja]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Familia] FOREIGN KEY([IdFamilia])
REFERENCES [dbo].[Familia] ([IdFamilia])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Familia]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Marca] FOREIGN KEY([IdMarca])
REFERENCES [dbo].[Marca] ([IdMarca])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Marca]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [CK_Productos_PrecioCosto] CHECK  (([PrecioCosto]>(0)))
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [CK_Productos_PrecioCosto]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [CK_Productos_PrecioVenta] CHECK  (([PrecioVenta]>(0)))
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [CK_Productos_PrecioVenta]
GO
/****** Object:  StoredProcedure [dbo].[sp_AltaFamilia]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_AltaFamilia](
	@Descripcion nvarchar(50),
	@Registrado bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	begin
		insert into Familia(Descripcion, FechaModificacion)
		values (@Descripcion, GETDATE())
		set @Registrado = 1
		set @Mensaje = 'Alta de Familia exitosa'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AltaMarca]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_AltaMarca](
	@Descripcion nvarchar(50),
	@Registrado bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	begin
		insert into Marca(Descripcion, FechaModificacion)
		values (@Descripcion, GETDATE())
		set @Registrado = 1
		set @Mensaje = 'Alta de Marca exitosa'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_AltaProducto]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_AltaProducto](
	@CodigoProducto nvarchar(50),
	@DescripcionProducto nvarchar(50),
	@PrecioCosto decimal(18,2),
	@PrecioVenta decimal(18,2),
	@IdMarca int,
	@IdFamilia int,
	@Registrado bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	begin
		insert into Productos(CodigoProducto, DescripcionProducto, PrecioCosto, PrecioVenta, IdMarca, IdFamilia, FechaModificacion)
		values (@CodigoProducto, @DescripcionProducto, @PrecioCosto, @PrecioVenta, @IdMarca, @IdFamilia, GETDATE())
		set @Registrado = 1
		set @Mensaje = 'Alta producto exitosa'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BajaFamilia]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_BajaFamilia](
	@IdFamilia nvarchar(50),
	@Baja bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	if(exists(select * from Familia where IdFamilia = @IdFamilia))
	begin
		update Familia
		set Baja = 1, FechaBaja = GETDATE()
		where IdFamilia = @IdFamilia
		set @Baja = 1
		set @Mensaje = 'Baja de Familia exitosa'
	end
	else
	begin
		set @Baja = 0
		set @Mensaje = 'La Familia con ese ID no se encontro o no existe'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BajaMarca]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_BajaMarca](
	@IdMarca nvarchar(50),
	@Baja bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	if(exists(select * from Marca where IdMarca = @IdMarca))
	begin
		update Marca
		set Baja = 1, FechaBaja = GETDATE()
		where IdMarca = @IdMarca
		set @Baja = 1
		set @Mensaje = 'Baja de Marca exitosa'
	end
	else
	begin
		set @Baja = 0
		set @Mensaje = 'La Marca con ese ID no se encontro o no existe'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BajaProducto]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_BajaProducto](
	@CodigoProducto nvarchar(50),
	@Baja bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	if(exists(select * from Productos where CodigoProducto = @CodigoProducto))
	begin
		update Productos
		set Baja = 1, FechaBaja = GETDATE()
		where CodigoProducto = @CodigoProducto
		set @Baja = 1
		set @Mensaje = 'Baja producto exitosa'
	end
	else
	begin
		set @Baja = 0
		set @Mensaje = 'El producto con ese codigo no se encontro o no existe'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificacionFamilia]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ModificacionFamilia](
	@IdFamilia int,
	@Descripcion nvarchar(50),
	@Modificado bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	if(exists(select * from Familia where IdFamilia = @IdFamilia))
	begin
		update Familia
		set Descripcion = @Descripcion,
			FechaModificacion = GETDATE()
		where IdFamilia = @IdFamilia
		set @Modificado = 1
		set @Mensaje = 'Familia modificada con exito'
	end
	else
	begin
		set @Modificado = 0
		set @Mensaje = 'La Familia con ese ID no se encontro o no existe'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificacionMarca]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ModificacionMarca](
	@IdMarca int,
	@Descripcion nvarchar(50),
	@Modificado bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	if(exists(select * from Marca where IdMarca = @IdMarca))
	begin
		update Marca
		set Descripcion = @Descripcion,
			FechaModificacion = GETDATE()
		where IdMarca = @IdMarca
		set @Modificado = 1
		set @Mensaje = 'Marca modificada con exito'
	end
	else
	begin
		set @Modificado = 0
		set @Mensaje = 'La Marca con ese ID no se encontro o no existe'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ModificacionProducto]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_ModificacionProducto](
	@CodigoProducto nvarchar(50),
	@DescripcionProducto nvarchar(50) = NULL,
	@PrecioCosto decimal(18,2) = NULL,
	@PrecioVenta decimal(18,2) = NULL,
	@IdMarca int = NULL,
	@IdFamilia int = NULL,
	@Modificado bit output,
	@Mensaje varchar(100) output
)
as
begin
	
	if(exists(select * from Productos where CodigoProducto = @CodigoProducto))
	begin
		update Productos
		set DescripcionProducto = coalesce(@DescripcionProducto, DescripcionProducto),
			PrecioCosto = coalesce(@PrecioCosto, PrecioCosto),
			PrecioVenta = coalesce(@PrecioVenta, PrecioVenta),
			IdMarca = coalesce(@IdMarca, IdMarca),
			IdFamilia = coalesce(@IdFamilia, IdFamilia),
			FechaModificacion = GETDATE()
		where CodigoProducto = @CodigoProducto
		set @Modificado = 1
		set @Mensaje = 'Producto modificado con exito'
	end
	else
	begin
		set @Modificado = 0
		set @Mensaje = 'El producto con ese codigo no se encontro o no existe'
	end
	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerProductos]    Script Date: 31/1/2023 00:51:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[sp_ObtenerProductos](
	@CodigoProducto nvarchar(50) = NULL,
	@IdMarca int = NULL,
	@IdFamilia int = NULL
)
as
begin
	begin
		select * from Productos
		where (@CodigoProducto is NULL OR CodigoProducto = @CodigoProducto) AND
			  (@IdMarca is NULL OR IdMarca = @IdMarca) AND
			  (@IdFamilia is NULL OR IdFamilia = @IdFamilia)
	end
	
end
GO
USE [master]
GO
ALTER DATABASE [DBPRODUCTOS] SET  READ_WRITE 
GO
