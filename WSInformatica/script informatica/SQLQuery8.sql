USE [Informatica]
GO

/****** Object:  Table [dbo].[persona]    Script Date: 10/7/2023 07:52:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[persona](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IdConsulta] [int] NULL,
	[dni] [int] NULL,
	[nombre1] [nchar](20) NULL,
	[nombre2] [nchar](12) NULL,
	[apellido1] [nchar](20) NULL,
	[apellido2] [nchar](10) NULL,
	[clase] [int] NULL,
	[Resultado] [bit] NULL,
 CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[persona]  WITH CHECK ADD  CONSTRAINT [FK_persona_consulta] FOREIGN KEY([IdConsulta])
REFERENCES [dbo].[consulta] ([id])
GO

ALTER TABLE [dbo].[persona] CHECK CONSTRAINT [FK_persona_consulta]
GO


