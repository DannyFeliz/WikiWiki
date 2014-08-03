
go
create trigger aprovacionTrigger on publicaciones
after insert 
as
begin
declare @publicacion_id int, @usuario_id varchar(50), @estado_id int;

	select @publicacion_id = publicacion_id, @estado_id = 1
	from inserted;

	insert into Aprovacions (publicacion_id, estado_id) values (@publicacion_id, @estado_id);
end
go


go
create trigger usuarioTrigger on [Registroes] after insert as 
begin
declare @usuario varchar(50), @clave varchar(50), @registro_id int, @rol varchar(50), @email varchar(50)
	
	select @usuario = usuario, @clave = clave, @email = email, @registro_id = registro_id
	from inserted

	insert into Usuarios (usuario1, clave, email, registro_id, estado) values (@usuario, @clave, @email, @registro_id, 6);
end
go

go
create trigger RolTrigger on [Usuarios] after insert as 
begin
declare @usuario_id int

	select @usuario_id = usuario_id
	from inserted

	insert into Roles_de_usuario (usuario_id, rol_id) values (@usuario_id, 2);
end
go


/* fin trigger*/

/* View */
  create view Por_aprovar as 
  select p.publicacion_id, p.usuario_id, p.titulo, p.informacion, u.usuario1, e.estado1, e.estado_id, u.foto, p.fecha_publicacion, p.categoria
  from publicaciones p 
	join Usuarios u on (p.usuario_id = u.usuario_id)
	join Estadoes e on (p.estado_id = e.estado_id);