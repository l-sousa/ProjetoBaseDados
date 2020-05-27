CREATE FUNCTION PROJETO.get_nif_clinica () RETURNS int
AS
BEGIN
declare @clin_nif int;
SELECT @clin_nif = nif FROM PROJETO.CLINICA 
RETURN @clin_nif;

END

SELECT PROJETO.get_nif_clinica()