GO
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE NAME = 'SchemaTest')
    BEGIN
        EXEC('CREATE SCHEMA SchemaTest AUTHORIZATION [dbo];')

    END