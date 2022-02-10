CREATE DATABASE SPMedicalGroup;
GO

USE SPMedicalGroup;
GO

CREATE TABLE TipoUsuario (
	idTipoUsuario INT PRIMARY KEY IDENTITY,
	tituloTipoUsuario VARCHAR(100) NOT NULL UNIQUE
);
GO


CREATE TABLE INSTITUICAO(
	idInstituicao INT PRIMARY KEY IDENTITY,
	nomeFantasia VARCHAR(50) NOT NULL,
	endereco VARCHAR(50) NOT NULL,
	razaoSocial VARCHAR(50) NOT NULL UNIQUE,
	CNPJ CHAR(18) NOT NULL UNIQUE
);



CREATE TABLE ESPECIALIDADE(
	idEspecialidade INT PRIMARY KEY IDENTITY,
	tipoEspecialidade VARCHAR(50) NOT NULL
);

CREATE TABLE SITUACAO(
	idSituacao INT PRIMARY KEY IDENTITY,
	descricao VARCHAR(25) NOT NULL
);

CREATE TABLE USUARIO(
	idUsuario INT PRIMARY KEY IDENTITY,
	idTipoUsuario INT FOREIGN KEY REFERENCES tipoUsuario(idTipoUsuario),
	nomeUsuario VARCHAR(50) NOT NULL,
	emailUsuario VARCHAR(256) NOT NULL UNIQUE,
	senhaUsuario VARCHAR(15) NOT NULL
);

CREATE TABLE PACIENTE(
	idPaciente INT PRIMARY KEY IDENTITY,
	idUsuario INT FOREIGN KEY REFERENCES usuario(idUsuario),
	dataNasc DATETIME NOT NULL,
	CPF VARCHAR(11) NOT NULL UNIQUE,
	RG VARCHAR(15) NOT NULL UNIQUE,
	Telefone VARCHAR(15),
	enderecoPaciente VARCHAR(150) NOT NULL 
);


CREATE TABLE MEDICO(
	idMedico INT PRIMARY KEY IDENTITY,
	idUsuario INT FOREIGN KEY REFERENCES USUARIO(idUsuario),
	idEspecialidade INT FOREIGN KEY REFERENCES ESPECIALIDADE(idEspecialidade),
	idInstituicao INT FOREIGN KEY REFERENCES INSTITUICAO(idInstituicao),
	CRM VARCHAR(8) NOT NULL UNIQUE
);

CREATE TABLE CONSULTA(
	idConsulta INT PRIMARY KEY IDENTITY,
	idMedico INT FOREIGN KEY REFERENCES MEDICO(idMedico), 
	idSituacao INT FOREIGN KEY REFERENCES SITUACAO(idSituacao),
	idPaciente INT FOREIGN KEY REFERENCES PACIENTE (idPaciente),
	dataConsulta DATE NOT NULL,
	descricaoConsulta VARCHAR(100) NOT NULL
);
GO

CREATE TABLE IMAGEMUSUARIO(
	idImagem INT PRIMARY KEY IDENTITY(1,1),
	idUsuario INT NOT NULL UNIQUE FOREIGN KEY REFERENCES USUARIO(idUsuario),
	binario VARBINARY(MAX) NOT NULL,
	mimeType VARCHAR(30) NOT NULL,
	nomeArquivo VARCHAR(250) NOT NULL,
	data_inclusao DATETIME DEFAULT GETDATE() NULL
);
GO

DROP DATABASE SPMedicalGroup