DROP PROC PROJETO.insert_staff;
declare @retval AS int;

exec PROJETO.insert_staff null, -9, "LUCASSSSS", 234234, 11651111, 21332, adsafasd, 123,  "ASSISTENTE", @retval output

print @retval

create procedure PROJETO.insert_staff 
	@especialidade varchar(35),
	@numero_ordem int, 
	@nome varchar(100), 
	@contacto int, 
	@nif int, 
	@idade int,
	@endereco varchar(100), 
	@salario decimal(8,2),
	@profissao varchar(100),
	@retval int output
as 

IF(EXISTS(SELECT nif FROM PROJETO.PESSOA WHERE nif = @nif))
BEGIN
set @retval = -1;
RETURN @retval; -- erro!
END

ELSE
declare @nif_clinica int;
set @nif_clinica = (SELECT PROJETO.get_nif_clinica());

set @retval = 1;
-- insert pessoa
INSERT INTO PROJETO.PESSOA (endereco, contacto, idade, nif, nome) VALUES (@endereco, @contacto, @idade, @nif, @nome)
-- insert staff
INSERT INTO PROJETO.STAFF (nif, salario, nif_clinica) VALUES (@nif, @salario, @nif_clinica)
-- insert profissao

IF(@profissao = 'DENTISTA')
BEGIN
INSERT INTO PROJETO.DENTISTA (nif, especialidade, numero_ordem) VALUES (@nif, @especialidade, @numero_ordem)
return @retval;
END

ELSE IF (@profissao = 'ASSISTENTE')
BEGIN
INSERT INTO PROJETO.ASSISTENTE (nif) VALUES (@nif)
return @retval;
END

ELSE IF(@profissao = 'RECECCIONISTA')
BEGIN 
INSERT INTO PROJETO.RECECIONISTA (nif) VALUES (@nif)
return @retval;
END

ELSE
BEGIN
set @retval = 0;
return @retval;
END