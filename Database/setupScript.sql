USE [master]
GO
/****** Object:  Database [ClewbayFarm]    Script Date: 05/12/2024 15:58:04 ******/
CREATE DATABASE [ClewbayFarm]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClewbayFarm', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ClewbayFarm.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClewbayFarm_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ClewbayFarm_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ClewbayFarm] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClewbayFarm].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClewbayFarm] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClewbayFarm] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClewbayFarm] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClewbayFarm] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClewbayFarm] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClewbayFarm] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ClewbayFarm] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClewbayFarm] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClewbayFarm] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClewbayFarm] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClewbayFarm] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClewbayFarm] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClewbayFarm] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClewbayFarm] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClewbayFarm] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ClewbayFarm] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClewbayFarm] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClewbayFarm] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClewbayFarm] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClewbayFarm] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClewbayFarm] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ClewbayFarm] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClewbayFarm] SET RECOVERY FULL 
GO
ALTER DATABASE [ClewbayFarm] SET  MULTI_USER 
GO
ALTER DATABASE [ClewbayFarm] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClewbayFarm] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClewbayFarm] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClewbayFarm] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClewbayFarm] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClewbayFarm] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ClewbayFarm', N'ON'
GO
ALTER DATABASE [ClewbayFarm] SET QUERY_STORE = ON
GO
ALTER DATABASE [ClewbayFarm] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ClewbayFarm]
GO
/****** Object:  User [ApiUser]    Script Date: 05/12/2024 15:58:04 ******/
CREATE USER [ApiUser] FOR LOGIN [ApiUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [ApiUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [ApiUser]
GO
/****** Object:  Table [dbo].[BedCrops]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BedCrops](
	[BedCropId] [int] IDENTITY(1,1) NOT NULL,
	[BedId] [int] NOT NULL,
	[CropId] [int] NOT NULL,
	[PlantingDate] [date] NOT NULL,
	[RemovalDate] [date] NULL,
	[PlantingWeek]  AS (datepart(week,[PlantingDate])),
	[RemovalWeek]  AS (datepart(week,[RemovalDate])),
PRIMARY KEY CLUSTERED 
(
	[BedCropId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Beds]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beds](
	[BedId] [int] IDENTITY(1,1) NOT NULL,
	[BlockId] [int] NOT NULL,
	[Position] [int] NOT NULL,
	[Length] [float] NOT NULL,
	[Width] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blocks]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blocks](
	[BlockId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[BlockTypeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BlockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockTypes]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockTypes](
	[BlockTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BlockTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Covers]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Covers](
	[CoverId] [int] IDENTITY(1,1) NOT NULL,
	[CropId] [int] NOT NULL,
	[CoverType] [nvarchar](100) NOT NULL,
	[StartWeek] [int] NOT NULL,
	[EndWeek] [int] NOT NULL,
	[Notes] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[CoverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CropBedAttributes]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CropBedAttributes](
	[CropId] [int] NOT NULL,
	[TimeToMaturity] [int] NOT NULL,
	[RowSpacing] [decimal](5, 2) NOT NULL,
	[PlantSpacing] [decimal](5, 2) NOT NULL,
	[Notes] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[CropId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CropPropagationAttributes]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CropPropagationAttributes](
	[CropId] [int] NOT NULL,
	[PropagationTime] [int] NOT NULL,
	[GerminationTime] [int] NOT NULL,
	[PreferredTemperature] [decimal](5, 2) NULL,
	[Notes] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[CropId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE CropTypes (
    CropTypeId INT PRIMARY KEY IDENTITY(1,1), 
    Family NVARCHAR(100) NOT NULL CHECK (Family IN ('Allium', 'Apiaceae', 'Asteraceae', 'Solanaceae', 'Brassicaceae', 'Cucurbitaceae', 'Fabaceae')),
    UNIQUE (Family) -- Ensure unique crop type names
);
/****** Object:  Table [dbo].[Crops]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE Crops (
    CropId INT PRIMARY KEY IDENTITY(1,1),
    CropTypeId INT NOT NULL, -- Foreign Key to CropTypes
    Variety NVARCHAR(100) NOT NULL,
    IsDirectSow BIT NOT NULL,
    -- Foreign Key Constraint
    FOREIGN KEY (CropTypeId) REFERENCES CropTypes (CropTypeId) ON DELETE CASCADE
);
GO
/****** Object:  Table [dbo].[ModuleTrays]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleTrays](
	[TrayId] [int] IDENTITY(1,1) NOT NULL,
	[AreaId] [int] NOT NULL,
	[TrayTypeId] [int] NOT NULL,
	[CropId] [int] NULL,
	[SeedsPerModule] [int] NOT NULL,
	[PlantingDate] [date] NULL,
	[PlantingWeek] [int] NULL,
	[RemovalDate] [date] NULL,
	[RemovalWeek] [int] NULL,
	[BedCropId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TrayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleTrayTypes]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleTrayTypes](
	[TrayTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[NumberOfModules] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TrayTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropagationAreas]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropagationAreas](
	[AreaId] [int] IDENTITY(1,1) NOT NULL,
	[TunnelId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[MaxTrays] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PropagationTunnel]    Script Date: 05/12/2024 15:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropagationTunnel](
	[TunnelId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TunnelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Crops] ADD  DEFAULT ((0)) FOR [IsDirectSow]
GO
ALTER TABLE [dbo].[BedCrops]  WITH CHECK ADD FOREIGN KEY([BedId])
REFERENCES [dbo].[Beds] ([BedId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BedCrops]  WITH CHECK ADD FOREIGN KEY([CropId])
REFERENCES [dbo].[Crops] ([CropId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Beds]  WITH CHECK ADD FOREIGN KEY([BlockId])
REFERENCES [dbo].[Blocks] ([BlockId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Blocks]  WITH CHECK ADD FOREIGN KEY([BlockTypeId])
REFERENCES [dbo].[BlockTypes] ([BlockTypeId])
GO
ALTER TABLE [dbo].[Covers]  WITH CHECK ADD FOREIGN KEY([CropId])
REFERENCES [dbo].[Crops] ([CropId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CropBedAttributes]  WITH CHECK ADD FOREIGN KEY([CropId])
REFERENCES [dbo].[Crops] ([CropId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CropPropagationAttributes]  WITH CHECK ADD FOREIGN KEY([CropId])
REFERENCES [dbo].[Crops] ([CropId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ModuleTrays]  WITH CHECK ADD FOREIGN KEY([AreaId])
REFERENCES [dbo].[PropagationAreas] ([AreaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ModuleTrays]  WITH CHECK ADD FOREIGN KEY([BedCropId])
REFERENCES [dbo].[BedCrops] ([BedCropId])
GO
ALTER TABLE [dbo].[ModuleTrays]  WITH CHECK ADD FOREIGN KEY([CropId])
REFERENCES [dbo].[Crops] ([CropId])
GO
ALTER TABLE [dbo].[ModuleTrays]  WITH CHECK ADD FOREIGN KEY([TrayTypeId])
REFERENCES [dbo].[ModuleTrayTypes] ([TrayTypeId])
GO
ALTER TABLE [dbo].[PropagationAreas]  WITH CHECK ADD FOREIGN KEY([TunnelId])
REFERENCES [dbo].[PropagationTunnel] ([TunnelId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BedCrops]  WITH CHECK ADD CHECK  (([RemovalDate] IS NULL OR [RemovalDate]>[PlantingDate]))
GO
ALTER TABLE [dbo].[ModuleTrays]  WITH CHECK ADD CHECK  (([SeedsPerModule]>(0)))
GO
ALTER TABLE [dbo].[ModuleTrays]  WITH CHECK ADD CHECK  (([RemovalDate] IS NULL OR [RemovalDate]>[PlantingDate]))
GO
USE [master]
GO
ALTER DATABASE [ClewbayFarm] SET  READ_WRITE 
GO
