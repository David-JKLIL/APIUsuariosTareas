-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
CREATE TABLE [dbo].[__EFMigrationsHistory] (
[MigrationId] nvarchar(150) NOT NULL ,
[ProductVersion] nvarchar(32) NOT NULL 
)


GO

-- ----------------------------
-- Table structure for Tareas
-- ----------------------------
DROP TABLE [dbo].[Tareas]
GO
CREATE TABLE [dbo].[Tareas] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Titulo] nvarchar(MAX) NOT NULL ,
[Descripcion] nvarchar(MAX) NOT NULL ,
[Estado] nvarchar(MAX) NOT NULL ,
[FechaVencimiento] datetime2(7) NOT NULL ,
[UsuarioId] int NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Tareas]', RESEED, 8)
GO

-- ----------------------------
-- Table structure for Usuarios
-- ----------------------------
DROP TABLE [dbo].[Usuarios]
GO
CREATE TABLE [dbo].[Usuarios] (
[Id] int NOT NULL IDENTITY(1,1) ,
[Nombre] nvarchar(MAX) NOT NULL ,
[correo] nvarchar(MAX) NOT NULL ,
[Contrasena] nvarchar(MAX) NOT NULL 
)


GO
DBCC CHECKIDENT(N'[dbo].[Usuarios]', RESEED, 9)
GO

-- ----------------------------
-- Indexes structure for table __EFMigrationsHistory
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD PRIMARY KEY ([MigrationId])
GO

-- ----------------------------
-- Indexes structure for table Tareas
-- ----------------------------
CREATE INDEX [IX_Tareas_UsuarioId] ON [dbo].[Tareas]
([UsuarioId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table Tareas
-- ----------------------------
ALTER TABLE [dbo].[Tareas] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table Usuarios
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table Usuarios
-- ----------------------------
ALTER TABLE [dbo].[Usuarios] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[Tareas]
-- ----------------------------
ALTER TABLE [dbo].[Tareas] ADD FOREIGN KEY ([UsuarioId]) REFERENCES [dbo].[Usuarios] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
