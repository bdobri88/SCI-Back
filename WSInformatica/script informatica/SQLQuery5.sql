USE [Informatica]
GO

/****** Object:  Table [dbo].[consulta]    Script Date: 10/7/2023 07:52:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[consulta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idDespachante] [int] NOT NULL,
	[idSolicitante] [int] NOT NULL,
	[movil] [int] NOT NULL,
	[lugar] [nchar](30) NOT NULL,
	[iDjuridiccion] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_consulta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[consulta]  WITH CHECK ADD  CONSTRAINT [FK_consulta_dependencia1] FOREIGN KEY([iDjuridiccion])
REFERENCES [dbo].[dependencia] ([id])
GO

ALTER TABLE [dbo].[consulta] CHECK CONSTRAINT [FK_consulta_dependencia1]
GO

ALTER TABLE [dbo].[consulta]  WITH CHECK ADD  CONSTRAINT [FK_consulta_efectivo] FOREIGN KEY([idSolicitante])
REFERENCES [dbo].[efectivo] ([id])
GO

ALTER TABLE [dbo].[consulta] CHECK CONSTRAINT [FK_consulta_efectivo]
GO

ALTER TABLE [dbo].[consulta]  WITH CHECK ADD  CONSTRAINT [FK_consulta_user] FOREIGN KEY([idDespachante])
REFERENCES [dbo].[user] ([id])
GO

ALTER TABLE [dbo].[consulta] CHECK CONSTRAINT [FK_consulta_user]
GO


