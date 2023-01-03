go
CREATE TABLE [SchemaTest].[company] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [name]            VARCHAR (40) NOT NULL,
    [company2address] INT          NOT NULL,
    [active]          BIT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [company_pk] PRIMARY KEY CLUSTERED ([id] ASC),
    --CONSTRAINT [company_company2address_fkey] FOREIGN KEY ([company2address]) REFERENCES [SchemaTest].[address] ([id])
);

GO
IF OBJECT_ID('SchemaTest.[company_company2address_fkey]') IS NULL 
	ALTER TABLE [SchemaTest].[company] ADD CONSTRAINT company_company2address_fkey FOREIGN KEY (company2address) REFERENCES [SchemaTest].[address](id);
