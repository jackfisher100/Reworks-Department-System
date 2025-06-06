USE [Bentley]
GO
/****** Object:  Table [dbo].[BayInfo]    Script Date: 19/04/2025 15:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BayInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BayNumber] [int] NOT NULL,
	[BentleyID] [varchar](255) COLLATE Latin1_General_CI_AS NOT NULL,
	[AndonOn] [bit] NOT NULL,
	[TimeLogged] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BreakInfo]    Script Date: 19/04/2025 15:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BreakInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[BayNumber] [int] NOT NULL,
	[BentleyID] [varchar](255) COLLATE Latin1_General_CI_AS NOT NULL,
	[BreakOn] [bit] NOT NULL,
	[TimeLogged] [datetime] NULL,
	[Barcode] [varchar](255) COLLATE Latin1_General_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 19/04/2025 15:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[EntryID] [int] IDENTITY(1,1) NOT NULL,
	[Barcode] [varchar](64) COLLATE Latin1_General_CI_AS NOT NULL,
	[TimeOfEntry] [datetime] NULL,
	[TimeOfExit] [datetime] NULL,
	[BayNo] [int] NULL,
	[WorkerID] [varchar](32) COLLATE Latin1_General_CI_AS NULL,
	[ManagerID] [varchar](32) COLLATE Latin1_General_CI_AS NULL,
	[Issue] [text] COLLATE Latin1_General_CI_AS NULL,
	[Prediction] [int] NULL,
	[Dwell] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 19/04/2025 15:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[SettingsID] [int] IDENTITY(1,1) NOT NULL,
	[SettingKey] [varchar](255) COLLATE Latin1_General_CI_AS NOT NULL,
	[SettingValue] [varchar](255) COLLATE Latin1_General_CI_AS NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 19/04/2025 15:51:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[BentleyID] [varchar](20) COLLATE Latin1_General_CI_AS NULL,
	[NameOfUser] [varchar](100) COLLATE Latin1_General_CI_AS NULL,
	[Manager] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/********* Insert a manager into the table **********/
INSERT INTO [dbo].[Users] ([BentleyID], [NameOfUser], [Manager]) VALUES ('123456', 'John Doe', 1)
GO