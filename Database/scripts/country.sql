go
CREATE TABLE [SchemaTest].[country] (
    [id]         SMALLINT          ,
    [iso_2_code] VARCHAR (2)  NOT NULL,
    [iso_3_code] VARCHAR (3)  NOT NULL,
    [name]       VARCHAR (50) NOT NULL,
    CONSTRAINT [pk_country] PRIMARY KEY CLUSTERED ([id] ASC)

);


-- Unique 
GO
IF OBJECT_ID('SchemaTest.[uk_country_iso_3_code]', 'UQ') IS NULL 
	ALTER TABLE [SchemaTest].[country] ADD CONSTRAINT uk_country_iso_3_code UNIQUE (iso_3_code);


GO
IF OBJECT_ID('SchemaTest.[uk_country_iso_2_code]', 'UQ') IS NULL 
	ALTER TABLE [SchemaTest].[country] ADD CONSTRAINT uk_country_iso_2_code UNIQUE (iso_2_code);