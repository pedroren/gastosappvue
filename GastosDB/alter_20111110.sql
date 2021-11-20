use GastosDB
go

alter table dbo.Usuarios
add  Nombre nvarchar(100) null
go

alter table dbo.Usuarios
add  Email nvarchar(100) null
go