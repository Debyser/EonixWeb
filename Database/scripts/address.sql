GO
CREATE TABLE [SchemaTest].[address] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [zipcode]         VARCHAR (20) NOT NULL,
    [street]          VARCHAR (50) NOT NULL,
    [box_number]      VARCHAR (30) NOT NULL,
    [city]            VARCHAR (40) NULL,
    [address2country] SMALLINT          NOT NULL,
    [active]          BIT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [address_pk] PRIMARY KEY CLUSTERED ([id] ASC),
);

GO
IF OBJECT_ID('myschema.[address_address2country_fkey]') IS NULL 
	ALTER TABLE [SchemaTest].[address]  ADD CONSTRAINT [address_address2country_fkey]  FOREIGN KEY ([address2country]) REFERENCES [SchemaTest].[country](id);
