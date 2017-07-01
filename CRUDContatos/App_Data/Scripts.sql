-- Cria a database
CREATE DATABASE CrudContatos

USE CrudContatos

-- Cria as tabelas
CREATE TABLE Clientes
(
	CodCliente INT IDENTITY PRIMARY KEY,
	Nome VARCHAR(100) NOT NULL,
	Email VARCHAR(255) NULL,
	DataCriacao DATETIME NOT NULL
)

CREATE TABLE TelefonesCliente
(
	CodTelefoneCliente INT IDENTITY PRIMARY KEY,
	CodCliente INT,
	Telefone VARCHAR(12)

	CONSTRAINT FK_TelefonesCliente_Clientes FOREIGN KEY(CodCliente)
	REFERENCES Clientes(CodCliente)
)

-- Realiza o insert nas tabelas

INSERT INTO Clientes VALUES('João', 'joão@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Carlos', 'carlos@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Eduardo', 'eduardo@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Lucas', 'lucas@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Jorge', 'jorge@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Henrique', 'Henrique@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Pedro', 'pedro@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Batista', 'batista@gmail.com', GETDATE())
INSERT INTO Clientes VALUES('Michele', 'michele@gmail.com', GETDATE())

INSERT INTO TelefonesCliente VALUES (1, '118888888')
INSERT INTO TelefonesCliente VALUES (1, '119999999')
INSERT INTO TelefonesCliente VALUES (2, '117777777')
INSERT INTO TelefonesCliente VALUES (2, '116666666')
INSERT INTO TelefonesCliente VALUES (3, '115555555')
INSERT INTO TelefonesCliente VALUES (3, '114444444')
INSERT INTO TelefonesCliente VALUES (4, '113333333')
INSERT INTO TelefonesCliente VALUES (4, '112222222')
INSERT INTO TelefonesCliente VALUES (5, '111111111')