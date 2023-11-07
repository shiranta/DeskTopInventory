USE [master]
GO
/****** Object:  Database [AnyStore]    Script Date: 14/01/2021 15:09:14 ******/
CREATE DATABASE [AnyStore]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AnyStore', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AnyStore.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AnyStore_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\AnyStore_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AnyStore] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AnyStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AnyStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AnyStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AnyStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AnyStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AnyStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [AnyStore] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AnyStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AnyStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AnyStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AnyStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AnyStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AnyStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AnyStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AnyStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AnyStore] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AnyStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AnyStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AnyStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AnyStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AnyStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AnyStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AnyStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AnyStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AnyStore] SET  MULTI_USER 
GO
ALTER DATABASE [AnyStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AnyStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AnyStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AnyStore] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AnyStore] SET DELAYED_DURABILITY = DISABLED 
GO
USE [AnyStore]
GO
/****** Object:  Table [dbo].[tbl_categories]    Script Date: 14/01/2021 15:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Description] [text] NULL,
	[AddedDate] [datetime] NULL,
	[AddedBy] [int] NULL,
 CONSTRAINT [PK_tbl_categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Dea_Cust]    Script Date: 14/01/2021 15:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Dea_Cust](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](10) NULL,
	[Name] [varchar](100) NULL,
	[eMail] [varchar](150) NULL,
	[Contact] [varchar](25) NULL,
	[Address] [text] NULL,
	[AddedDate] [datetime] NULL,
	[AddedBy] [int] NULL,
 CONSTRAINT [PK_tbl_Dea_Cust] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Products]    Script Date: 14/01/2021 15:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Categories] [varchar](50) NULL,
	[Description] [text] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Qty] [decimal](18, 2) NULL,
	[AddedDate] [datetime] NULL,
	[AddedBy] [int] NULL,
 CONSTRAINT [PK_tbl_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Transction]    Script Date: 14/01/2021 15:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Transction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[Dea_Cust_Id] [int] NULL,
	[Grand_Total] [decimal](18, 2) NULL,
	[Transctiondate] [datetime] NULL,
	[Tax] [decimal](18, 2) NULL,
	[Discount] [decimal](18, 2) NULL,
	[Addedby] [int] NULL,
 CONSTRAINT [PK_tbl_Transctions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_transction_Detail]    Script Date: 14/01/2021 15:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_transction_Detail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Id] [int] NULL,
	[Rate] [decimal](18, 2) NULL,
	[Qty] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
	[Dea_Cust_Id] [int] NULL,
	[Added_Date] [datetime] NULL,
	[Added_By] [int] NULL,
	[TransHeader_Id] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_tbl_transction_Detail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Users]    Script Date: 14/01/2021 15:09:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[eMail] [varchar](150) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Contact] [varchar](50) NULL,
	[Address] [text] NULL,
	[Gender] [varchar](10) NULL,
	[UserType] [varchar](50) NULL,
	[AddedDate] [date] NULL,
	[AddedBy] [int] NULL,
 CONSTRAINT [PK_tbl_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [AnyStore] SET  READ_WRITE 
GO
