CREATE DATABASE RRHH_STOREFINAL
use RRHH_STOREFINAL
----------------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------------

CREATE TABLE Disciplina(
idDisciplina BIGINT IDENTITY (1,1) NOT NULL,
nombreDisciplina varchar(250)NOT NULL,
activo bit NOT NULL,
PRIMARY KEY (idDisciplina),
);

INSERT INTO Disciplina (nombreDisciplina, activo) values('Ciencias Exactas e Ingenierías', 1)
INSERT INTO Disciplina (nombreDisciplina, activo) values('Ciencias Económico Administrativas',1)
INSERT INTO Disciplina (nombreDisciplina, activo) values('Ciencias Sociales y Humanidades',1)
INSERT INTO Disciplina (nombreDisciplina, activo) values('General', 1)


-----------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------
CREATE TABLE Sexo(
idSexo BIGINT IDENTITY(1,1) NOT NULL,
nombreSexo varchar (20)  NOT NULL,
activo bit not null,
PRIMARY KEY(idSexo)
);

INSERT INTO Sexo(nombreSexo,activo) VALUES ('Femenino',1)
INSERT INTO Sexo(nombreSexo,activo) VALUES ('Masculino',1)
INSERT INTO Sexo(nombreSexo, activo) VALUES ('No Binario',1)
INSERT INTO Sexo(nombreSexo, activo) VALUES ('No Especificado',1)


-----------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------
CREATE TABLE Disponibilidad(
idDisponibilidad BIGINT IDENTITY (1,1) NOT NULL,
estadoDisponibilidad varchar(16)NOT NULL,
PRIMARY KEY (idDisponibilidad),
);

INSERT INTO Disponibilidad(estadoDisponibilidad) VALUES('Full-time')
INSERT INTO Disponibilidad(estadoDisponibilidad) VALUES('Part-time')
INSERT INTO Disponibilidad(estadoDisponibilidad) VALUES('No especificada')


-------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------
CREATE TABLE EstadoCivil(
idEstadoCivil BIGINT IDENTITY (1,1) NOT NULL,
nombreEstadoCivil varchar(22)NOT NULL,
PRIMARY KEY (idEstadoCivil),
);

INSERT INTO EstadoCivil(nombreEstadoCivil) VALUES('Soltero/a')
INSERT INTO EstadoCivil(nombreEstadoCivil) VALUES('Casado/a')
INSERT INTO EstadoCivil(nombreEstadoCivil) VALUES('Divorciado/a')
INSERT INTO EstadoCivil(nombreEstadoCivil) VALUES('Viudo/a')
INSERT INTO EstadoCivil(nombreEstadoCivil) VALUES('En Unión Convivencial')
INSERT INTO EstadoCivil(nombreEstadoCivil) VALUES('Separado/a')


--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
CREATE TABLE EstadoNivelEduc(
idEstadoNivelEduc BIGINT IDENTITY(1,1) NOT NULL,
nombreEstadoNivelEduc varchar(20) NOT NULL,
PRIMARY KEY (idEstadoNivelEduc)
);
INSERT INTO EstadoNivelEduc(nombreEstadoNivelEduc) VALUES ('Finalizado')
INSERT INTO EstadoNivelEduc(nombreEstadoNivelEduc) VALUES ('Cursando')


--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------

create table NivelEducativo(
id_Nivel bigint identity(1,1) not null,
NombreNivel varchar(30) not null,
primary key (id_Nivel),
);

insert into NivelEducativo(NombreNivel) values('Primario')
insert into NivelEducativo(NombreNivel) values('Secundario')
insert into NivelEducativo(NombreNivel) values('Terciario')
insert into NivelEducativo(NombreNivel) values('Universitario')
insert into NivelEducativo(NombreNivel) values('Postgrado')
insert into NivelEducativo(NombreNivel) values('Master')


---------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------
Create table TipoPostulante(
idTipoPostulante bigint identity(1,1) not null,
nombreTipoPostulante varchar(25)not null,
primary key (idTipoPostulante)
);

Insert into TipoPostulante(nombreTipoPostulante) values('Profesional Titulado')
Insert into TipoPostulante(nombreTipoPostulante) values('Profesional Idoneo')
Insert into TipoPostulante(nombreTipoPostulante) values('Estudiante')


--------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------
Create table EstadoPostulante (
idEstadoPostulante bigint identity (1,1) not null, 
nombreEstadoPostulante varchar (150) not null,
primary key (idEstadoPostulante)
); 

Insert into EstadoPostulante(nombreEstadoPostulante)values('Aspirante')
Insert into EstadoPostulante(nombreEstadoPostulante)values('Capacitado en León')
Insert into EstadoPostulante(nombreEstadoPostulante)values('Pasante')
Insert into EstadoPostulante(nombreEstadoPostulante)values('Empleado')



-------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------
CREATE TABLE Persona(
idPersona BIGINT IDENTITY (1,1) NOT NULL,
nombre varchar(150)NOT NULL,
apellido varchar(150)NOT NULL,
nacionalidad varchar(20)NOT NULL,
dni varchar(9)NOT NULL,
fechaNacimiento DateTime NOT NULL,
telefono varchar(12)NOT NULL,
domicilio varchar(200)NOT NULL,
mail varchar(200)NOT NULL,
telFijo varchar(12),
telAlternativo varchar(12),
hijos  bit,
cantHijos int,
foto image,
activa bit NOT NULL,
disponibilidadID BIGINT  NOT NULL,
estadoCivilID BIGINT  NOT NULL,
sexoID BIGINT  NOT NULL,
PRIMARY KEY (idPersona),
CONSTRAINT FK_Disponibilidad_Persona FOREIGN KEY(disponibilidadID)REFERENCES Disponibilidad(idDisponibilidad),
CONSTRAINT FK_EstadoCivil_Persona FOREIGN KEY(estadoCivilID)REFERENCES EstadoCivil(idEstadoCivil),
CONSTRAINT FK_Sexo_Persona FOREIGN KEY(sexoID) REFERENCES Sexo(idSexo)
);


INSERT INTO Persona(nombre,apellido,nacionalidad,dni,fechaNacimiento,telefono,domicilio,mail,hijos,activa,disponibilidadID,estadoCivilID, sexoID)values('Daiana Micaela', 'Ibrahim','Argentina', '39727934', 1996-07-28,'3816162301','Av. San Martín S/N. La Trinidad','micaela.ibrahim2896@gmail.com', 0,1, 2,1,1)
INSERT INTO Persona(nombre,apellido,nacionalidad,dni,fechaNacimiento,telefono,domicilio,mail,hijos,activa,disponibilidadID,estadoCivilID,sexoID)values('Damian' ,'Cordoba','Argentina', '39975898',1993-04-27,'3816162301','Peru 1285, Villa Urquiza','damianCordoba93@gmail.com', 2,1, 2,2,2)
INSERT INTO Persona(nombre,apellido,nacionalidad,dni,fechaNacimiento,telefono,domicilio,mail,hijos,activa,disponibilidadID,estadoCivilID,sexoID)values('Exequiel' ,'Rodriguez','Argentina', '36874323',1992-07-14,'3816162301','Pasaje Gonzalez, Alderetes','exeuielRodriguez@gmail.com', 1,1, 1,5,2)
INSERT INTO Persona(nombre,apellido,nacionalidad,dni,fechaNacimiento,telefono,domicilio,mail,hijos,activa,disponibilidadID,estadoCivilID,sexoID)values('Noemi' ,'Gonzales','Argentina', '43874323',1996-05-08,'3814262401','Av.Peron 583, Yerba Buena','noemiGonzales@gmail.com', 0,1, 2,1,1)


---------------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------------
Create table Administrador (
id_usuario BIGINT IDENTITY (1,1) NOT NULL,
usuario varchar (15) not null,
contraseña varchar(10) not null,
legajo varchar(10) not null,
activo bit,
admin_personaID BIGINT not null,
PRIMARY KEY (id_usuario),
CONSTRAINT FK_idPersona FOREIGN KEY (admin_personaID) REFERENCES Persona(idPersona)
);


INSERT INTO Administrador (usuario,contraseña,legajo,activo,admin_personaID) values ('damianRH','fundleon','001',1,2)
INSERT INTO Administrador (usuario,contraseña,legajo,activo,admin_personaID) values ('exequielRH','fundleon','002',1,3)
INSERT INTO Administrador (usuario,contraseña,legajo,activo,admin_personaID) values ('noemiRH','fundleon','003',1,4)
INSERT INTO Administrador (usuario,contraseña,legajo,activo,admin_personaID) values ('r','r','003',1,4)



-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
CREATE TABLE PerfilAcademico(
idPerfilAcademico BIGINT IDENTITY(1,1) NOT NULL,
Academico_NivelID BIGINT, 
estadoNivelID BIGINT, 
Titulo varchar(150),
Establecimiento varchar(150),
cursosRelacionados bit,
nombreArchivo varchar (200),
certificacion varbinary (max),
puntaje INT,
activo bit,
PRIMARY KEY(idPerfilAcademico),
CONSTRAINT FK_Nivel_PerfilAcademico FOREIGN KEY(Academico_NivelID) REFERENCES NivelEducativo(id_Nivel),
CONSTRAINT FK_EstadoNivel_PerfilAcademico FOREIGN KEY (estadoNivelID) REFERENCES EstadoNivelEduc(idEstadoNivelEduc),
);
 

-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------

 
CREATE TABLE PerfilPsicologico(
idPsicologico BIGINT IDENTITY (1,1) NOT NULL,
apto bit,
descripcion varchar(400),
puntaje INT,
activo bit,
PRIMARY KEY(idPsicologico),
);

-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------

CREATE TABLE PerfilProfesional(
idProfesional BIGINT IDENTITY(1,1) NOT NULL,
LinkedIn varchar(250),
Experiencia BIT, 
AñosExperiencia INT ,
Lugar_Empresa VARCHAR(100),
DescripcionPuesto VARCHAR (250), 
PeriodoInicio INT,
PeriodoFin INT,
nombreArchivo varchar (200),
cv varbinary (max),
puntaje INT,
activo bit,
PRIMARY KEY(idProfesional)
);

-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
CREATE TABLE Perfil(
idPerfil BIGINT IDENTITY (1,1) NOT NULL,
disciplinaID BIGINT,
tipoPostulanteID BIGINT,
estadoPostulanteID BIGINT,
PuntuaciónTotal INT,
activo bit,
PerfilPsicologico_id BIGINT ,
PerfilAcademico_id BIGINT ,
PerfilProfesional_id BIGINT,
PRIMARY KEY(idPerfil),
CONSTRAINT FK_Disciplina_Perfil FOREIGN KEY(disciplinaID) REFERENCES Disciplina(idDisciplina),
CONSTRAINT FK_TipoPostulante_Perfil FOREIGN KEY(tipoPostulanteID)REFERENCES TipoPostulante(idTipoPostulante),
CONSTRAINT FK_EstadoPostulante_Perfil FOREIGN KEY(estadoPostulanteID)REFERENCES EstadoPostulante(idEstadoPostulante),
CONSTRAINT FK_Perfil_PerfilPsicologico_id FOREIGN KEY(PerfilPsicologico_id)REFERENCES PerfilPsicologico(idPsicologico),
CONSTRAINT FK_Perfil_PerfilAcademico FOREIGN KEY(PerfilAcademico_id)REFERENCES PerfilAcademico(idPerfilAcademico),
CONSTRAINT FK_Perfil_PerfilProfesional FOREIGN KEY(PerfilProfesional_id)REFERENCES PerfilProfesional(idProfesional),

);



-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
CREATE TABLE Postulante(
idPostulante BIGINT IDENTITY (1,1) NOT NULL,
fechaRegistro DATETIME NOT NULL,
Perfil_id BIGINT NOT NULL,
Persona_Id BIGINT NOT NULL,
activo bit NOT NULL,
PRIMARY KEY(idPostulante),
CONSTRAINT FK_Postulante_Persona FOREIGN KEY (Persona_Id) REFERENCES Persona(idPersona),
CONSTRAINT FK_Postulante_Perfil FOREIGN KEY(Perfil_id)REFERENCES Perfil(idPerfil),
);


