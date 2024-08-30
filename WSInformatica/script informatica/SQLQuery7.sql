USE [Informatica]
GO

/****** Object:  Table [dbo].[efectivo]    Script Date: 10/7/2023 07:52:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[efectivo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[legajo] [int] NOT NULL,
	[nombre] [nchar](20) NOT NULL,
	[apellido] [nchar](20) NOT NULL,
	[IdDependencia] [nchar](20) NULL,
 CONSTRAINT [PK_afectivo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


