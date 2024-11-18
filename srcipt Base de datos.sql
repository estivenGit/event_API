--USE master
--CREATE DATABASE [API_PruebaDB]

USE [API_PruebaDB]
GO
/****** Object:  Table [dbo].[Evento_Asistentes]    Script Date: 18/11/2024 11:33:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evento_Asistentes](
	[Id_EventoAsistente] [int] IDENTITY(1,1) NOT NULL,
	[Id_Evento] [int] NULL,
	[Id_Usuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_EventoAsistente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Eventos]    Script Date: 18/11/2024 11:33:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Eventos](
	[Id_Evento] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[Descripcion] [varchar](2000) NULL,
	[FechaHora] [datetime] NULL,
	[Ubicacion] [varchar](500) NULL,
	[Capacidad] [int] NULL,
	[Id_UsuarioCreador] [int] NULL,
	[Activo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 18/11/2024 11:33:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[Id_Rol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 18/11/2024 11:33:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NULL,
	[Pass] [nvarchar](200) NULL,
	[Email] [varchar](200) NULL,
	[Activo] [bit] NULL,
	[Usuario] [varchar](100) NULL,
	[Id_Rol] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios_TokenRefresh]    Script Date: 18/11/2024 11:33:29 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios_TokenRefresh](
	[Id_TokenRefresh] [int] IDENTITY(1,1) NOT NULL,
	[Id_Usuario] [int] NOT NULL,
	[Token] [nvarchar](500) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaExpiracion] [datetime] NOT NULL,
	[FechaRevocacion] [datetime] NULL,
	[TokenReemplazo] [nvarchar](500) NULL,
	[EstaActivo] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id_TokenRefresh] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Evento_Asistentes] ON 
GO
INSERT [dbo].[Evento_Asistentes] ([Id_EventoAsistente], [Id_Evento], [Id_Usuario]) VALUES (1, 3, 1)
GO
INSERT [dbo].[Evento_Asistentes] ([Id_EventoAsistente], [Id_Evento], [Id_Usuario]) VALUES (10, 5, 1)
GO
INSERT [dbo].[Evento_Asistentes] ([Id_EventoAsistente], [Id_Evento], [Id_Usuario]) VALUES (6, 5, 2)
GO
INSERT [dbo].[Evento_Asistentes] ([Id_EventoAsistente], [Id_Evento], [Id_Usuario]) VALUES (14, 32, 4)
GO
INSERT [dbo].[Evento_Asistentes] ([Id_EventoAsistente], [Id_Evento], [Id_Usuario]) VALUES (15, 37, 4)
GO
SET IDENTITY_INSERT [dbo].[Evento_Asistentes] OFF
GO
SET IDENTITY_INSERT [dbo].[Eventos] ON 
GO
INSERT [dbo].[Eventos] ([Id_Evento], [Nombre], [Descripcion], [FechaHora], [Ubicacion], [Capacidad], [Id_UsuarioCreador], [Activo]) VALUES (3, N'Evento de prueba', N'Descripción del evento', CAST(N'2024-11-18T15:00:00.000' AS DateTime), N'Bogotá', 100, 2, 1)
GO
INSERT [dbo].[Eventos] ([Id_Evento], [Nombre], [Descripcion], [FechaHora], [Ubicacion], [Capacidad], [Id_UsuarioCreador], [Activo]) VALUES (5, N'Evento de prueba 2', N'Descripción del evento', CAST(N'2024-11-18T15:00:00.000' AS DateTime), N'Bogotá', 100, 4, 1)
GO
INSERT [dbo].[Eventos] ([Id_Evento], [Nombre], [Descripcion], [FechaHora], [Ubicacion], [Capacidad], [Id_UsuarioCreador], [Activo]) VALUES (31, N'Evento Capacitacion', N'Capacitacion Empleados', CAST(N'2024-11-18T12:00:00.000' AS DateTime), N'Meet 1', 4, 4, 1)
GO
INSERT [dbo].[Eventos] ([Id_Evento], [Nombre], [Descripcion], [FechaHora], [Ubicacion], [Capacidad], [Id_UsuarioCreador], [Activo]) VALUES (32, N'Sesion Administrativa', N'reglas internas', CAST(N'2024-11-19T08:00:00.000' AS DateTime), N'oficina principal', 2, 1, 1)
GO
INSERT [dbo].[Eventos] ([Id_Evento], [Nombre], [Descripcion], [FechaHora], [Ubicacion], [Capacidad], [Id_UsuarioCreador], [Activo]) VALUES (33, N'Conferencia ', N'Conferencia ', CAST(N'2024-11-20T10:00:00.000' AS DateTime), N'auditorio', 100, 4, 1)
GO
INSERT [dbo].[Eventos] ([Id_Evento], [Nombre], [Descripcion], [FechaHora], [Ubicacion], [Capacidad], [Id_UsuarioCreador], [Activo]) VALUES (36, N'Evento fin de año', N'todas las personas de la empresa', CAST(N'2024-12-07T09:00:00.000' AS DateTime), N'Parque Campestre', 50, 1, 1)
GO
INSERT [dbo].[Eventos] ([Id_Evento], [Nombre], [Descripcion], [FechaHora], [Ubicacion], [Capacidad], [Id_UsuarioCreador], [Activo]) VALUES (37, N'Socializacion clientes', N'Socializacion clientes actuales', CAST(N'2024-11-30T10:00:00.000' AS DateTime), N'meet', 1, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Eventos] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 
GO
INSERT [dbo].[Rol] ([Id_Rol], [Nombre]) VALUES (1, N'administrador')
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([Id_Usuario], [Nombre], [Pass], [Email], [Activo], [Usuario], [Id_Rol]) VALUES (1, N'Estiven Rodriguez', N'YK01OnZO36PZwBBWEhpXPA==', N'estiven.role@gmail.com', 1, N'EstivenR', 1)
GO
INSERT [dbo].[Usuarios] ([Id_Usuario], [Nombre], [Pass], [Email], [Activo], [Usuario], [Id_Rol]) VALUES (2, N'Prueba1', N'YK01OnZO36PZwBBWEhpXPA==', N'prueba@gmailcom', 1, N'prueba1', 1)
GO
INSERT [dbo].[Usuarios] ([Id_Usuario], [Nombre], [Pass], [Email], [Activo], [Usuario], [Id_Rol]) VALUES (3, N'Prueba2', N'YK01OnZO36PZwBBWEhpXPA==', N'prueba@gmailcom', 1, N'prueba2', 1)
GO
INSERT [dbo].[Usuarios] ([Id_Usuario], [Nombre], [Pass], [Email], [Activo], [Usuario], [Id_Rol]) VALUES (4, N'Prueba3', N'YK01OnZO36PZwBBWEhpXPA==', N'prueba@gmailcom', 1, N'prueba3', 1)
GO
INSERT [dbo].[Usuarios] ([Id_Usuario], [Nombre], [Pass], [Email], [Activo], [Usuario], [Id_Rol]) VALUES (5, N'Prueba4', N'YK01OnZO36PZwBBWEhpXPA==', N'prueba@gmailcom', 1, N'prueba4', 1)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios_TokenRefresh] ON 
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (1, 1, N'yV0XymeyolR6hm3YH+Xt8GM55UT0BtHX0DmvC7bHv60=', CAST(N'2024-11-16T11:31:52.603' AS DateTime), CAST(N'2024-11-16T15:36:50.933' AS DateTime), NULL, N'B5U2LX89q2Bsv+uK+WxsLx1OE82cYn2/3Dhiilpq7VM=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (2, 1, N'6fWIjPPUrX6LReo8G89EtnC8fOaoCccAbhXokJ8Lhyo=', CAST(N'2024-11-16T14:51:50.390' AS DateTime), CAST(N'2024-11-16T15:36:50.933' AS DateTime), NULL, N'B5U2LX89q2Bsv+uK+WxsLx1OE82cYn2/3Dhiilpq7VM=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (3, 1, N'viGxYBl5xktHmjScWiRAx07vpCyCw/QX474Ef5PCsYs=', CAST(N'2024-11-16T14:52:55.057' AS DateTime), CAST(N'2024-11-16T15:36:50.933' AS DateTime), NULL, N'B5U2LX89q2Bsv+uK+WxsLx1OE82cYn2/3Dhiilpq7VM=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (4, 1, N'llrI3HoVqIjImmR4Pk48lkwpoKEQvgF4OEwZBDS2x4g=', CAST(N'2024-11-16T15:18:05.043' AS DateTime), CAST(N'2024-11-16T15:36:50.933' AS DateTime), NULL, N'B5U2LX89q2Bsv+uK+WxsLx1OE82cYn2/3Dhiilpq7VM=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (5, 1, N'rR4KwInCd1xwwFptuOtIw2swnAurXEFEitCqSU45mfY=', CAST(N'2024-11-16T15:18:52.150' AS DateTime), CAST(N'2024-11-16T15:36:50.933' AS DateTime), NULL, N'B5U2LX89q2Bsv+uK+WxsLx1OE82cYn2/3Dhiilpq7VM=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (6, 1, N'zBewQswmZkNbVpavv+gdDHRk/TawWf5K+jCVUAuWZw4=', CAST(N'2024-11-16T15:21:36.730' AS DateTime), CAST(N'2024-11-16T15:36:50.933' AS DateTime), NULL, N'B5U2LX89q2Bsv+uK+WxsLx1OE82cYn2/3Dhiilpq7VM=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (7, 1, N'0B9ZxlGebseU5iefu8WguGApdoapI9VfdpUD3HKNfB4=', CAST(N'2024-11-16T15:21:49.913' AS DateTime), CAST(N'2024-11-16T15:36:50.933' AS DateTime), CAST(N'2024-11-16T10:40:01.360' AS DateTime), N'GJ1j+O5OjxLQSvdHR+tYUq86dBcCG5T6urJCRA0E3l0=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (8, 1, N'GJ1j+O5OjxLQSvdHR+tYUq86dBcCG5T6urJCRA0E3l0=', CAST(N'2024-11-16T15:40:00.990' AS DateTime), CAST(N'2024-11-16T19:40:00.990' AS DateTime), CAST(N'2024-11-16T11:50:43.507' AS DateTime), N'qFB0R/Jn1aIAfmJ8+/FqMzCqjWjX3vdecUBTxyMTbXA=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (9, 1, N'qFB0R/Jn1aIAfmJ8+/FqMzCqjWjX3vdecUBTxyMTbXA=', CAST(N'2024-11-16T16:50:43.067' AS DateTime), CAST(N'2024-11-16T20:50:43.067' AS DateTime), CAST(N'2024-11-16T11:51:42.790' AS DateTime), N'ada0luHKS8mvfvjD9tdZ7LNJyCW98W8V4AnUMGXU24s=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (10, 1, N'ada0luHKS8mvfvjD9tdZ7LNJyCW98W8V4AnUMGXU24s=', CAST(N'2024-11-16T11:51:42.767' AS DateTime), CAST(N'2024-11-16T15:51:42.767' AS DateTime), CAST(N'2024-11-16T12:19:44.627' AS DateTime), N'HTtDgYMxqBznWLWRBYaJogitD5iTBJ73cq3tNBCD+6g=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (11, 1, N'HTtDgYMxqBznWLWRBYaJogitD5iTBJ73cq3tNBCD+6g=', CAST(N'2024-11-16T12:19:43.993' AS DateTime), CAST(N'2024-11-16T16:19:43.993' AS DateTime), CAST(N'2024-11-16T12:20:36.840' AS DateTime), N'Dq1jt+brakV8zrxxy49jBIcqdc/FjPyPInMWIVgHkJ4=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (12, 1, N'Dq1jt+brakV8zrxxy49jBIcqdc/FjPyPInMWIVgHkJ4=', CAST(N'2024-11-16T12:20:36.827' AS DateTime), CAST(N'2024-11-16T16:20:36.827' AS DateTime), CAST(N'2024-11-16T12:22:56.783' AS DateTime), N'eK2swzNeLvwd+9O1mWH+qWbCJHzZWmb4DhBH2woDsoM=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (13, 1, N'eK2swzNeLvwd+9O1mWH+qWbCJHzZWmb4DhBH2woDsoM=', CAST(N'2024-11-16T12:22:56.110' AS DateTime), CAST(N'2024-11-16T16:22:56.110' AS DateTime), CAST(N'2024-11-16T12:23:32.527' AS DateTime), N'zoBDY1xtvZunbKpiG9fYooxe24l9R79GIhUXWTSkKvo=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (14, 1, N'zoBDY1xtvZunbKpiG9fYooxe24l9R79GIhUXWTSkKvo=', CAST(N'2024-11-16T12:23:32.520' AS DateTime), CAST(N'2024-11-16T16:23:32.520' AS DateTime), CAST(N'2024-11-16T12:25:51.213' AS DateTime), N'jBFAF3Tb1LNpjGeINi+xGBlcQpB9+kfvU3CVCxarqYc=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (15, 1, N'jBFAF3Tb1LNpjGeINi+xGBlcQpB9+kfvU3CVCxarqYc=', CAST(N'2024-11-16T12:25:50.597' AS DateTime), CAST(N'2024-11-16T16:25:50.597' AS DateTime), CAST(N'2024-11-16T12:27:25.150' AS DateTime), N'H3r5FLukEMDRyt9/KS+we1V2y2s3r7suOAOASuGRfaY=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (16, 1, N'H3r5FLukEMDRyt9/KS+we1V2y2s3r7suOAOASuGRfaY=', CAST(N'2024-11-16T12:27:24.520' AS DateTime), CAST(N'2024-11-16T16:27:24.520' AS DateTime), CAST(N'2024-11-16T12:28:58.500' AS DateTime), N'tE/loQbwPv9Fa6ZHEBdYpk9kirkudkVWBqnu/kkdUr8=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (17, 1, N'tE/loQbwPv9Fa6ZHEBdYpk9kirkudkVWBqnu/kkdUr8=', CAST(N'2024-11-16T12:28:58.493' AS DateTime), CAST(N'2024-11-16T16:28:58.493' AS DateTime), CAST(N'2024-11-16T12:38:53.827' AS DateTime), N'PFA/hc43VBv1c0qzn3OnlSsN+nkI0ZEQxflmUBy+6+g=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (18, 1, N'PFA/hc43VBv1c0qzn3OnlSsN+nkI0ZEQxflmUBy+6+g=', CAST(N'2024-11-16T12:38:53.163' AS DateTime), CAST(N'2024-11-16T16:38:53.163' AS DateTime), CAST(N'2024-11-16T12:43:29.503' AS DateTime), N'kgzU59ht1roMmxHfQ5ydcmHgAYd+z5VdJs8jORUfxhw=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (19, 1, N'kgzU59ht1roMmxHfQ5ydcmHgAYd+z5VdJs8jORUfxhw=', CAST(N'2024-11-16T12:43:29.007' AS DateTime), CAST(N'2024-11-16T16:43:29.007' AS DateTime), CAST(N'2024-11-16T13:27:26.607' AS DateTime), N'9upz4noyYFUTzUISng4csWlaEiWoKFM0DakTfhLfNN4=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (20, 1, N'9upz4noyYFUTzUISng4csWlaEiWoKFM0DakTfhLfNN4=', CAST(N'2024-11-16T13:27:26.127' AS DateTime), CAST(N'2024-11-16T17:27:26.127' AS DateTime), CAST(N'2024-11-16T15:15:15.890' AS DateTime), N'BgOkAlrKRulswkHTNC/F6oiuNE4gpz7cS3n0y5t9PKU=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (21, 1, N'BgOkAlrKRulswkHTNC/F6oiuNE4gpz7cS3n0y5t9PKU=', CAST(N'2024-11-16T15:15:15.243' AS DateTime), CAST(N'2024-11-16T19:15:15.243' AS DateTime), CAST(N'2024-11-16T15:46:08.537' AS DateTime), N'FOZ410lT4E41Jvg2DVDK0a0upNjsEHtY9JhSZr+v1IY=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (22, 1, N'FOZ410lT4E41Jvg2DVDK0a0upNjsEHtY9JhSZr+v1IY=', CAST(N'2024-11-16T15:46:07.430' AS DateTime), CAST(N'2024-11-16T19:46:07.430' AS DateTime), CAST(N'2024-11-16T16:17:30.523' AS DateTime), N'10tUWJXlCe4wZnNC1NRpueFfpqYhcNTv/Htxa+7V4cU=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (23, 1, N'10tUWJXlCe4wZnNC1NRpueFfpqYhcNTv/Htxa+7V4cU=', CAST(N'2024-11-16T16:17:30.087' AS DateTime), CAST(N'2024-11-16T20:17:30.087' AS DateTime), CAST(N'2024-11-16T16:48:09.697' AS DateTime), N'lLl5TMCx2sZyfS8bmpUj3EYb6NUsTcpFqtv3CHw2FP8=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (24, 1, N'lLl5TMCx2sZyfS8bmpUj3EYb6NUsTcpFqtv3CHw2FP8=', CAST(N'2024-11-16T16:48:09.140' AS DateTime), CAST(N'2024-11-16T20:48:09.140' AS DateTime), CAST(N'2024-11-16T17:18:37.337' AS DateTime), N'ccR3Y/j+1FxdJ7r1kAoBeTpCb4sTSiV3kC2qiPdwFJo=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (25, 1, N'ccR3Y/j+1FxdJ7r1kAoBeTpCb4sTSiV3kC2qiPdwFJo=', CAST(N'2024-11-16T17:18:36.837' AS DateTime), CAST(N'2024-11-16T21:18:36.837' AS DateTime), CAST(N'2024-11-16T19:34:24.083' AS DateTime), N'Z1IdsE++C7LVfOqEx6+dWnC1ce+oX46IEnsvxa7Stgk=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (26, 1, N'Z1IdsE++C7LVfOqEx6+dWnC1ce+oX46IEnsvxa7Stgk=', CAST(N'2024-11-16T19:34:22.937' AS DateTime), CAST(N'2024-11-16T23:34:22.937' AS DateTime), CAST(N'2024-11-16T20:07:33.717' AS DateTime), N'zL9XsEJ92CdOoZst5JqnJ6dC0Gn4F72V+YZq5lFMgNc=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (27, 1, N'zL9XsEJ92CdOoZst5JqnJ6dC0Gn4F72V+YZq5lFMgNc=', CAST(N'2024-11-16T20:07:33.073' AS DateTime), CAST(N'2024-11-17T00:07:33.073' AS DateTime), CAST(N'2024-11-17T10:36:33.630' AS DateTime), N'gOIJA+fuC/bhq3IOxzvtnj6wLdUimntUMGMNmdZAT6M=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (28, 1, N'gOIJA+fuC/bhq3IOxzvtnj6wLdUimntUMGMNmdZAT6M=', CAST(N'2024-11-17T10:36:33.087' AS DateTime), CAST(N'2024-11-17T14:36:33.087' AS DateTime), CAST(N'2024-11-17T10:37:26.887' AS DateTime), N'7ubMilsfiza/EKgTRBdq1yf6NGbjbXcvbQV5yT2dCzo=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (29, 1, N'7ubMilsfiza/EKgTRBdq1yf6NGbjbXcvbQV5yT2dCzo=', CAST(N'2024-11-17T10:37:26.877' AS DateTime), CAST(N'2024-11-17T14:37:26.877' AS DateTime), CAST(N'2024-11-17T13:33:17.810' AS DateTime), N'CYw8QWA3MrWVEnSFJQd8QvC0D6kUM2DPUtJArR08I+c=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (30, 1, N'CYw8QWA3MrWVEnSFJQd8QvC0D6kUM2DPUtJArR08I+c=', CAST(N'2024-11-17T13:33:17.107' AS DateTime), CAST(N'2024-11-17T17:33:17.107' AS DateTime), CAST(N'2024-11-17T15:47:58.633' AS DateTime), N'yykO3W0pa40syqF1nC/ovLLUxhuvgmKlwxVUcq3gKrA=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (31, 1, N'yykO3W0pa40syqF1nC/ovLLUxhuvgmKlwxVUcq3gKrA=', CAST(N'2024-11-17T15:47:57.827' AS DateTime), CAST(N'2024-11-17T19:47:57.827' AS DateTime), CAST(N'2024-11-17T15:48:16.387' AS DateTime), N'BRAt03J5zDuDDzhhftO7iITaC0rb8WKO+xC0vtxTXEg=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (32, 1, N'BRAt03J5zDuDDzhhftO7iITaC0rb8WKO+xC0vtxTXEg=', CAST(N'2024-11-17T15:48:16.380' AS DateTime), CAST(N'2024-11-17T19:48:16.380' AS DateTime), CAST(N'2024-11-17T15:49:16.007' AS DateTime), N'tFnc+dE4YYPvEHY4yLQkN5cOimb47kuVr1DgQSILpLk=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (33, 1, N'tFnc+dE4YYPvEHY4yLQkN5cOimb47kuVr1DgQSILpLk=', CAST(N'2024-11-17T15:49:15.550' AS DateTime), CAST(N'2024-11-17T19:49:15.550' AS DateTime), CAST(N'2024-11-17T15:50:08.810' AS DateTime), N'0biQwJDR1HfE8nBIWRyYetjBxezxZB0EAWINq+Z+U9E=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (34, 1, N'0biQwJDR1HfE8nBIWRyYetjBxezxZB0EAWINq+Z+U9E=', CAST(N'2024-11-17T15:50:08.370' AS DateTime), CAST(N'2024-11-17T19:50:08.370' AS DateTime), CAST(N'2024-11-17T15:51:43.867' AS DateTime), N'7iF6BRzQO9TTHKQK1/mOqWN1KcwpwyIiS77v+M13W+I=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (35, 1, N'7iF6BRzQO9TTHKQK1/mOqWN1KcwpwyIiS77v+M13W+I=', CAST(N'2024-11-17T15:51:43.860' AS DateTime), CAST(N'2024-11-17T19:51:43.860' AS DateTime), CAST(N'2024-11-17T16:07:10.573' AS DateTime), N'WI1zS+MWZJh52vY7qcUj0bva4wzz5HxUZQQhraJJdh8=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (36, 1, N'WI1zS+MWZJh52vY7qcUj0bva4wzz5HxUZQQhraJJdh8=', CAST(N'2024-11-17T16:07:10.067' AS DateTime), CAST(N'2024-11-17T20:07:10.067' AS DateTime), CAST(N'2024-11-17T16:07:29.227' AS DateTime), N'QXFZnOGaFcmeJqbYt688iEYwl2oEFj0OlLpO5YO5WR4=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (37, 1, N'QXFZnOGaFcmeJqbYt688iEYwl2oEFj0OlLpO5YO5WR4=', CAST(N'2024-11-17T16:07:29.220' AS DateTime), CAST(N'2024-11-17T20:07:29.220' AS DateTime), CAST(N'2024-11-17T16:11:11.953' AS DateTime), N'wx9XI9ve2slinYIDPplZS46hoJKXnpj6bRp9Bxh5iEk=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (38, 1, N'wx9XI9ve2slinYIDPplZS46hoJKXnpj6bRp9Bxh5iEk=', CAST(N'2024-11-17T16:11:11.927' AS DateTime), CAST(N'2024-11-17T20:11:11.927' AS DateTime), CAST(N'2024-11-17T17:13:24.217' AS DateTime), N'cNm4BSyb6pzeTksNsSk/FWJBMAtrL4f3RKj5IbniYRE=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (39, 1, N'cNm4BSyb6pzeTksNsSk/FWJBMAtrL4f3RKj5IbniYRE=', CAST(N'2024-11-17T17:13:24.203' AS DateTime), CAST(N'2024-11-17T21:13:24.203' AS DateTime), CAST(N'2024-11-17T18:04:40.387' AS DateTime), N'Ym5swQLjfwqKlRgVNyS5RZOQ2pFsppZyXGb+PQ4I/eo=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (40, 1, N'Ym5swQLjfwqKlRgVNyS5RZOQ2pFsppZyXGb+PQ4I/eo=', CAST(N'2024-11-17T18:04:39.303' AS DateTime), CAST(N'2024-11-17T22:04:39.303' AS DateTime), CAST(N'2024-11-17T18:35:46.060' AS DateTime), N'jT8XiJTfgXc2nfPTF7A7RVOcDxqTjlFMAuDE6+PfbDE=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (41, 1, N'jT8XiJTfgXc2nfPTF7A7RVOcDxqTjlFMAuDE6+PfbDE=', CAST(N'2024-11-17T18:35:45.233' AS DateTime), CAST(N'2024-11-17T22:35:45.233' AS DateTime), CAST(N'2024-11-17T19:10:23.797' AS DateTime), N'F30ZpmevZBP6qYp3dO+wkmm/rWf5Dpt4htx9o2Eb7+o=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (42, 1, N'F30ZpmevZBP6qYp3dO+wkmm/rWf5Dpt4htx9o2Eb7+o=', CAST(N'2024-11-17T19:10:22.810' AS DateTime), CAST(N'2024-11-17T23:10:22.810' AS DateTime), CAST(N'2024-11-17T19:43:12.363' AS DateTime), N'Z1sNiP0PHaOarNvozNg5MA+WGHPGHAnNuyJh9bXZcTU=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (43, 1, N'Z1sNiP0PHaOarNvozNg5MA+WGHPGHAnNuyJh9bXZcTU=', CAST(N'2024-11-17T19:43:11.860' AS DateTime), CAST(N'2024-11-17T23:43:11.860' AS DateTime), CAST(N'2024-11-17T20:33:08.427' AS DateTime), N'e3nd7FODipyCAehB8LAgi2coWq0497i/YEfgvOJBkI0=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (44, 1, N'e3nd7FODipyCAehB8LAgi2coWq0497i/YEfgvOJBkI0=', CAST(N'2024-11-17T20:33:08.070' AS DateTime), CAST(N'2024-11-18T00:33:08.070' AS DateTime), CAST(N'2024-11-17T20:35:19.853' AS DateTime), N'qXrTw1jOfVBUOGXQ16tSL5ARnIVchTNAsUgUDEQ1J3o=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (45, 1, N'qXrTw1jOfVBUOGXQ16tSL5ARnIVchTNAsUgUDEQ1J3o=', CAST(N'2024-11-17T20:35:19.847' AS DateTime), CAST(N'2024-11-18T00:35:19.847' AS DateTime), CAST(N'2024-11-17T20:35:40.817' AS DateTime), N'aFVfdUnIK/AkqTZnIUGgQIFsqyU0LnJIFwDx+RRlkUU=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (46, 1, N'aFVfdUnIK/AkqTZnIUGgQIFsqyU0LnJIFwDx+RRlkUU=', CAST(N'2024-11-17T20:35:40.807' AS DateTime), CAST(N'2024-11-18T00:35:40.807' AS DateTime), CAST(N'2024-11-17T20:36:52.553' AS DateTime), N'tHMQMJs9/lfxYDgPLEX0r+r3c2P66RtsevFy9M68Uvc=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (47, 1, N'tHMQMJs9/lfxYDgPLEX0r+r3c2P66RtsevFy9M68Uvc=', CAST(N'2024-11-17T20:36:52.547' AS DateTime), CAST(N'2024-11-18T00:36:52.547' AS DateTime), CAST(N'2024-11-17T22:06:44.267' AS DateTime), N'hRUteVObRTEd9a0fpvszEZRgBctBz7aoFM2TqxS5vmU=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (48, 1, N'hRUteVObRTEd9a0fpvszEZRgBctBz7aoFM2TqxS5vmU=', CAST(N'2024-11-17T22:06:44.250' AS DateTime), CAST(N'2024-11-18T02:06:44.250' AS DateTime), CAST(N'2024-11-17T23:16:30.947' AS DateTime), N'ljUJkvPdRr+6Yq5NX6Rhp0X7Sv0KCVq36i0x/ZGDhes=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (49, 1, N'ljUJkvPdRr+6Yq5NX6Rhp0X7Sv0KCVq36i0x/ZGDhes=', CAST(N'2024-11-17T23:16:30.937' AS DateTime), CAST(N'2024-11-18T03:16:30.937' AS DateTime), CAST(N'2024-11-18T00:17:18.347' AS DateTime), N'vKWRUzrKPqeuA8zghmZpEWbAiaJ+PNryI9jR2yRrpOg=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (50, 1, N'vKWRUzrKPqeuA8zghmZpEWbAiaJ+PNryI9jR2yRrpOg=', CAST(N'2024-11-18T00:17:18.337' AS DateTime), CAST(N'2024-11-18T04:17:18.337' AS DateTime), CAST(N'2024-11-18T01:19:03.080' AS DateTime), N's7AARzDdoc1LmGBAj0eje69wXNTKf58CcIiPCaujG/I=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (51, 1, N's7AARzDdoc1LmGBAj0eje69wXNTKf58CcIiPCaujG/I=', CAST(N'2024-11-18T01:19:02.723' AS DateTime), CAST(N'2024-11-18T05:19:02.723' AS DateTime), CAST(N'2024-11-18T01:20:41.870' AS DateTime), N'3+fqnh+B5TA7RxBGuiQDotN0TWFUuE2KzYffmfJMQaY=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (52, 1, N'3+fqnh+B5TA7RxBGuiQDotN0TWFUuE2KzYffmfJMQaY=', CAST(N'2024-11-18T01:20:41.853' AS DateTime), CAST(N'2024-11-18T05:20:41.853' AS DateTime), CAST(N'2024-11-18T06:14:12.010' AS DateTime), N'2C60N8E9K7LHzCqYSSz+/j8HFQjvUx5OBrFpjnDxsfI=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (53, 1, N'2C60N8E9K7LHzCqYSSz+/j8HFQjvUx5OBrFpjnDxsfI=', CAST(N'2024-11-18T06:14:11.990' AS DateTime), CAST(N'2024-11-18T10:14:11.990' AS DateTime), CAST(N'2024-11-18T07:15:16.923' AS DateTime), N'9uStqi8janRH4OdXxebFNhfQCo4SYnH8IkaaTiOIe4g=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (54, 1, N'9uStqi8janRH4OdXxebFNhfQCo4SYnH8IkaaTiOIe4g=', CAST(N'2024-11-18T07:15:16.033' AS DateTime), CAST(N'2024-11-18T11:15:16.033' AS DateTime), CAST(N'2024-11-18T08:18:59.600' AS DateTime), N'eVtHAbiF6+g/IwBaFxF4OCkhXV01sbHfHibiFZbDWnU=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (55, 1, N'eVtHAbiF6+g/IwBaFxF4OCkhXV01sbHfHibiFZbDWnU=', CAST(N'2024-11-18T08:18:59.103' AS DateTime), CAST(N'2024-11-18T12:18:59.103' AS DateTime), CAST(N'2024-11-18T08:37:43.703' AS DateTime), N'ydZsoFsvVYBMDcvGnv6oXZfO1o8vqGPXOGluW2xru+E=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (56, 1, N'ydZsoFsvVYBMDcvGnv6oXZfO1o8vqGPXOGluW2xru+E=', CAST(N'2024-11-18T08:37:43.680' AS DateTime), CAST(N'2024-11-18T12:37:43.680' AS DateTime), CAST(N'2024-11-18T09:42:50.883' AS DateTime), N'gE5764v1LYpiZu+ejMf/SRT1YXdJzczTiS28JK3GGQo=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (57, 1, N'gE5764v1LYpiZu+ejMf/SRT1YXdJzczTiS28JK3GGQo=', CAST(N'2024-11-18T09:42:50.860' AS DateTime), CAST(N'2024-11-18T13:42:50.860' AS DateTime), CAST(N'2024-11-18T10:37:24.767' AS DateTime), N'rLI5aeh0DuDQVdXY4FAFhhpYPDKLadr50LfZkuNmHZ0=', 0)
GO
INSERT [dbo].[Usuarios_TokenRefresh] ([Id_TokenRefresh], [Id_Usuario], [Token], [FechaCreacion], [FechaExpiracion], [FechaRevocacion], [TokenReemplazo], [EstaActivo]) VALUES (58, 1, N'rLI5aeh0DuDQVdXY4FAFhhpYPDKLadr50LfZkuNmHZ0=', CAST(N'2024-11-18T10:37:24.733' AS DateTime), CAST(N'2024-11-18T14:37:24.733' AS DateTime), NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Usuarios_TokenRefresh] OFF
GO
ALTER TABLE [dbo].[Usuarios_TokenRefresh] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Evento_Asistentes]  WITH CHECK ADD FOREIGN KEY([Id_Evento])
REFERENCES [dbo].[Eventos] ([Id_Evento])
GO
ALTER TABLE [dbo].[Evento_Asistentes]  WITH CHECK ADD FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Eventos]  WITH CHECK ADD FOREIGN KEY([Id_UsuarioCreador])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([Id_Rol])
REFERENCES [dbo].[Rol] ([Id_Rol])
GO
ALTER TABLE [dbo].[Usuarios_TokenRefresh]  WITH CHECK ADD FOREIGN KEY([Id_Usuario])
REFERENCES [dbo].[Usuarios] ([Id_Usuario])
GO
