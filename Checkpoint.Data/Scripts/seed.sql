-- Seed básico para Checkpoint (Versión Corregida)
SET NOCOUNT ON;
GO

-- 1. Crear Roles (manera segura)
IF NOT EXISTS (SELECT 1 FROM [Rol] WHERE Nombre = 'Admin')
    INSERT INTO Rol (Id, Nombre) VALUES (NEWID(), 'Admin');
IF NOT EXISTS (SELECT 1 FROM [Rol] WHERE Nombre = 'Control de Calidad')
    INSERT INTO Rol (Id, Nombre) VALUES (NEWID(), 'Control de Calidad');
IF NOT EXISTS (SELECT 1 FROM [Rol] WHERE Nombre = 'Personal de Bodega')
    INSERT INTO Rol (Id, Nombre) VALUES (NEWID(), 'Personal de Bodega');
GO

-- 2. Crear usuario admin con password 'admin' (Hash corregido)
DECLARE @adminId UNIQUEIDENTIFIER = NEWID();
DECLARE @rolId UNIQUEIDENTIFIER;
SELECT @rolId = Id FROM Rol WHERE Nombre = 'Admin';

-- Insertar usuario
INSERT INTO Usuario (Id, Nombre, Email, PasswordHash, Activo) 
VALUES (
    @adminId, 
    'Admin', 
    'admin@local', 
    '100000.aAZMMvGB5PYiC2PgEdHsqA==.3nMeDy95PGvlfC+4uIUjTGG5jPv3e+RgnVNP37IQ+M=', -- 🎯 TU HASH CORRECTO
    1
);

-- Asignar rol
IF @rolId IS NOT NULL
BEGIN
    INSERT INTO [UserRole] (UsuarioId, RolId, AsignadoEn, AsignadoPorUsuarioId) 
    VALUES (@adminId, @rolId, GETDATE(), NULL);
END
GO