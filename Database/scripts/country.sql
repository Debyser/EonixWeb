go
CREATE TABLE [SchemaTest].[country] (
    [id]         SMALLINT          ,
    [iso_2_code] VARCHAR (2)  NOT NULL,
    [iso_3_code] VARCHAR (3)  NOT NULL,
    [name]       VARCHAR (50) NOT NULL,
    CONSTRAINT [country_pk] PRIMARY KEY CLUSTERED ([id] ASC)
);