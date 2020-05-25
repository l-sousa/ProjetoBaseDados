DROP PROC PROJETO.insert_staff;

create procedure PROJETO.insert_staff @especialidade varchar(35), @numero_ordem int, @nome varchar(100), @contacto int, @nif int, @idade int, @endereco varchar(100), @salario decimal(8,2), @profissao varchar(100) 
as 

IF(EXISTS(SELECT nif FROM PROJETO.PESSOA WHERE nif = @nif))
RETURN -1; -- erro!

ELSE
declare @nif_clinica int;
set @nif_clinica = (SELECT PROJETO.get_nif_clinica());

-- insert pessoa
INSERT INTO PROJETO.PESSOA (endereco, contacto, idade, nif, nome) VALUES (@endereco, @contacto, @idade, @nif, @nome)
-- insert staff
INSERT INTO PROJETO.STAFF (nif, salario, nif_clinica) VALUES (@nif, @salario, @nif_clinica)
-- insert profissao

IF(@profissao = 'DENTISTA')
BEGIN
INSERT INTO PROJETO.DENTISTA (nif, especialidade, numero_ordem) VALUES (@nif, @especialidade, @numero_ordem)
RETURN 1;
END

ELSE IF (@profissao = 'ASSISTENTE')
BEGIN
INSERT INTO PROJETO.ASSISTENTE (nif) VALUES (@nif)
RETURN 1;
END

ELSE IF(@profissao = 'RECECCIONISTA')
BEGIN 
INSERT INTO PROJETO.RECECIONISTA (nif) VALUES (@nif)
RETURN 1;
END

ELSE
BEGIN
RETURN 0;--erro!
END