USE [Informatica]
GO

/****** Object:  Table [dbo].[automotor]    Script Date: 10/7/2023 07:51:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[automotor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IdConsulta] [int] NULL,
	[tipo_automotor] [int] NULL,
	[dominio] [nchar](10) NULL,
	[chasis] [nchar](20) NULL,
	[motor] [nchar](20) NULL,
	[Resultado] [bit] NULL,
 CONSTRAINT [PK_automotor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[automotor]  WITH CHECK ADD  CONSTRAINT [FK_automotor_consulta] FOREIGN KEY([IdConsulta])
REFERENCES [dbo].[consulta] ([id])
GO

ALTER TABLE [dbo].[automotor] CHECK CONSTRAINT [FK_automotor_consulta]
GO

ALTER TABLE [dbo].[automotor]  WITH CHECK ADD  CONSTRAINT [FK_automotor_tipo_automotor] FOREIGN KEY([tipo_automotor])
REFERENCES [dbo].[tipo_automotor] ([id])
GO

ALTER TABLE [dbo].[automotor] CHECK CONSTRAINT [FK_automotor_tipo_automotor]
GO


