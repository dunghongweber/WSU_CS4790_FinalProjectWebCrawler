USE [CS2550Tutor]
GO

/****** Object:  Table [dbo].[MyLinks]    Script Date: 4/24/2019 9:24:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MyLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Link] [varchar](255) NULL,
	[Crawled] [int] NULL
) ON [PRIMARY]
GO


