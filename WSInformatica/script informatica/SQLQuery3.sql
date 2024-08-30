USE [Informatica]
GO

/****** Object:  Table [dbo].[arma]    Script Date: 10/7/2023 07:50:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[arma](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[IdConsulta] [int] NULL,
	[num_arma] [nchar](20) NOT NULL,
	[marca] [nchar](15) NULL,
	[tipo] [nchar](10) NULL,
	[calibre] [int] NULL,
	[Resultado] [bit] NULL,
 CONSTRAINT [PK_arma] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[arma]  WITH CHECK ADD  CONSTRAINT [FK_arma_consulta] FOREIGN KEY([IdConsulta])
REFERENCES [dbo].[consulta] ([id])
GO

ALTER TABLE [dbo].[arma] CHECK CONSTRAINT [FK_arma_consulta]
GO


