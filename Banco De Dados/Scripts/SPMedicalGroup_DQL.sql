USE SPMedicalGroup;
GO

SELECT * FROM USUARIO
SELECT * FROM TipoUsuario
SELECT * FROM PACIENTE
SELECT * FROM SITUACAO
SELECT * FROM ESPECIALIDADE
SELECT * FROM MEDICO
SELECT * FROM CONSULTA
SELECT * FROM INSTITUICAO
SELECT * FROM IMAGEMUSUARIO;
GO



SELECT nomeUsuario 'Nome do Paciente', emailUsuario 'Email do paciente', titulotipoUsuario 'tipo de usuário', RG
FROM usuario u
INNER JOIN PACIENTE p
ON u.idUsuario = p.idUsuario
INNER JOIN TipoUsuario tu
ON tu.idTipoUsuario = u.idTipoUsuario


SELECT *
FROM USUARIO u
INNER JOIN MEDICO m
ON u.idUsuario = m.idUsuario


SELECT nomeUsuario, emailUsuario, RG, ISNULL (Telefone, 'Não cadastrado') Telefone, enderecoPaciente
FROM USUARIO u
INNER JOIN PACIENTE
ON u.idUsuario = PACIENTE.idUsuario


SELECT UP.nomeUsuario Paciente, 
	   UM.nomeUsuario Medico,
	   E.tipoEspecialidade Especialização,
	   CONVERT(VARCHAR(25), C.dataConsulta, 113) 'Data da Consulta',
	   S.descricao Situação,
	   C.descricaoConsulta 'Descricao da consulta' 
  FROM CONSULTA C
 INNER JOIN SITUACAO S
    ON C.idSituacao = S.idSituacao
 INNER JOIN PACIENTE P
    ON C.idPaciente = P.idPaciente
 INNER JOIN MEDICO M
    ON C.idMedico = M.idMedico 
 INNER JOIN ESPECIALIDADE E
    ON M.idEspecialidade = E.idEspecialidade
 INNER JOIN USUARIO UP
    ON P.idUsuario = UP.idUsuario
 INNER JOIN USUARIO UM
    ON M.idUsuario = UM.idUsuario;
GO


SELECT COUNT(idUsuario) 'Quantidade de Usuarios' FROM USUARIO
GO



CREATE FUNCTION ESPCpMed(@nomeEspec VARCHAR(90))
RETURNS TABLE
     AS
 RETURN (
          SELECT @nomeEspec AS Especialização, COUNT(idEspecialidade) 'Numero de Medicos'
		    FROM ESPECIALIDADE
		   WHERE tipoEspecialidade LIKE '%' + @nomeEspec + '%'
        )
GO
SELECT * FROM ESPCpMed('Acunpuntura');
GO





CREATE PROCEDURE  IdadePaciente
@idade VARCHAR(30)
    AS
 BEGIN
SELECT nomeUsuario, DATEDIFF(YEAR, dataNasc,GETDATE())
    AS idade
  FROM USUARIO U
 INNER JOIN PACIENTE P
    ON U.idUsuario = P.idUsuario
 WHERE nomeUsuario = @idade
   END;
GO

EXEC IdadePaciente 'Ligia'
GO