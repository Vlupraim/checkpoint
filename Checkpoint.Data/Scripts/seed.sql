-- Seed básico para Checkpoint
SET NOCOUNT ON;

-- Crear usuario admin con password 'admin' (se recomienda cambiar)
DECLARE @adminId UNIQUEIDENTIFIER = NEWID();
INSERT INTO Usuario (Id, Nombre, Email, PasswordHash, Activo) VALUES (@adminId, 'Admin', 'admin@local', '100000.3yZ0r3G7r/2aQqv0FqkZVg==.3A2d5T3u6KJm9Vq4Yk1n2b3c4d5e6f7g8h9i0j1k2l=',1);
DECLARE @rolId UNIQUEIDENTIFIER = NEWID();
INSERT INTO Rol (Id, Nombre) VALUES (@rolId, 'Administrador');
INSERT INTO [UserRole] (UsuarioId, RolId, AsignadoEn, AsignadoPorUsuarioId) VALUES (@adminId, @rolId, GETDATE(), NULL);
