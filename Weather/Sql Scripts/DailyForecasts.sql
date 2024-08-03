USE [Weather]
GO

/****** Object:  Table [dbo].[DailyForecasts]    Script Date: 8/3/2024 2:55:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DailyForecasts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
	[Temperature] [int] NOT NULL,
	[WindSpeed] [nvarchar](max) NOT NULL,
	[WindDirection] [nvarchar](max) NOT NULL,
	[ShortForecast] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DailyForecasts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


