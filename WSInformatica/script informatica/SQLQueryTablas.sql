USE [Informatica]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Efectivo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Legajo] [int] NOT NULL,
	[Nombre] varchar(50) NOT NULL,
	[Apellido] varchar(50) NOT NULL,
	[IdDependencia] [int] NULL,
 CONSTRAINT [PK_afectivo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Dependencia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] varchar(50) NOT NULL,
 CONSTRAINT [PK_dependencia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[User] (
    Id INT IDENTITY(1,1) NOT NULL,
    IdEfectivo INT NOT NULL,  
    Password VARCHAR(100) NOT NULL,  
    EsAdmin BIT NOT NULL,  
    CONSTRAINT PK_User PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT FK_User_Efectivo FOREIGN KEY (IdEfectivo) REFERENCES Efectivo(Id),
    CONSTRAINT UC_User_Efectivo UNIQUE (IdEfectivo) 
);
GO

CREATE TABLE [dbo].[Consulta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDespachante] [int] NOT NULL,
	[IdSolicitante] [int] NOT NULL,
	[Movil] [int] NOT NULL,
	[Lugar] varchar(50) NOT NULL,
	[IDjuridiccion] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	CONSTRAINT [FK_consulta_dependencia] FOREIGN KEY([iDjuridiccion])REFERENCES [dbo].[Dependencia] ([Id]),
	CONSTRAINT [FK_consulta_efectivo] FOREIGN KEY([idSolicitante])REFERENCES [dbo].[Efectivo] ([Id]),
	CONSTRAINT [FK_consulta_user] FOREIGN KEY([idDespachante])REFERENCES [dbo].[User] ([Id]),
 CONSTRAINT [PK_consulta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Automotor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	ConsultaId [int] NULL,
	[TipoAutomotorId] [int] NULL,
	[Dominio] [nchar](10) NULL,
	[Chasis] [nchar](20) NULL,
	[Motor] [nchar](20) NULL,
	[Resultado] [bit] NULL,
	CONSTRAINT [FK_automotor_consulta] FOREIGN KEY([ConsultaId])REFERENCES [dbo].[Consulta] ([Id]),
	CONSTRAINT [FK_automotor_tipo_automotor] FOREIGN KEY([TipoAutomotorId])REFERENCES [dbo].[TipoAutomotor] ([Id]),
 CONSTRAINT [PK_automotor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TipoAutomotor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tipo] varchar(50)NOT NULL,
 CONSTRAINT [PK_tipo_automotor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Persona](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConsultaId] [int] NULL,
	[Dni] [int] NULL,
	[Nombre1] varchar(50) NULL,
	[Nombre2] varchar(50) NULL,
	[Apellido1] varchar(50) NULL,
	[Apellido2] varchar(50) NULL,
	[Clase] [int] NULL,
	[Resultado] [bit] NULL,
	CONSTRAINT [FK_persona_consulta] FOREIGN KEY([ConsultaId])REFERENCES [dbo].[Consulta] ([Id]),
	CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Arma](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConsultaId] [int] NULL,
	[NumArma] varchar(50) NOT NULL,
	[Marca] varchar(50) NULL,
	[Tipo] varchar(50) NULL,
	[Calibre] [int] NULL,
	[Resultado] [bit] NULL,
	CONSTRAINT [FK_arma_consulta] FOREIGN KEY([ConsultaId])REFERENCES [dbo].[Consulta] ([Id]),
 CONSTRAINT [PK_arma] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO