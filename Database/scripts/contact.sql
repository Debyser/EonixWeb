go
CREATE TABLE [SchemaTest].[contact] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [last_name]        VARCHAR (40) NOT NULL,
    [first_name]       VARCHAR (40) NOT NULL,
    [contact2address] INT          NOT NULL,
    [active]          BIT          DEFAULT ((1)) NOT NULL,
    [creation_time]   DATETIME     NULL,
    CONSTRAINT [contact_pk] PRIMARY KEY CLUSTERED ([id] ASC),
   -- CONSTRAINT [contact_contact2address_fkey] FOREIGN KEY ([contact2address]) REFERENCES [SchemaTest].[address] ([id])
);


GO
IF OBJECT_ID('SchemaTest.[contact_contact2address_fkey]') IS NULL 
	ALTER TABLE [SchemaTest].[contact] ADD CONSTRAINT contact_contact2address_fkey FOREIGN KEY (contact2address) REFERENCES [SchemaTest].[address](id);
