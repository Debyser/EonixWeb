GO
CREATE TABLE [SchemaTest].[contact_role] (
    [id]                   INT          IDENTITY (1, 1) NOT NULL,
    [contact_role2company] INT          NOT NULL,
    [contact_role2contact] INT          NOT NULL,
    [name]                 VARCHAR (40) NOT NULL,
    [active]               BIT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [contact_role_pk] PRIMARY KEY CLUSTERED ([id] ASC),
    --CONSTRAINT [contact_role_contact_role2company_fkey] FOREIGN KEY ([contact_role2company]) REFERENCES [SchemaTest].[company] ([id]),
    --CONSTRAINT [contact_role_contact_role2contact_fkey] FOREIGN KEY ([contact_role2contact]) REFERENCES [SchemaTest].[contact] ([id]),
    --CONSTRAINT [contact_role_un] UNIQUE NONCLUSTERED ([contact_role2company] ASC, [contact_role2contact] ASC, [name] ASC)
);

GO
IF OBJECT_ID('SchemaTest.[contact_role_contact_role2company_fkey]') IS NULL 
	ALTER TABLE [SchemaTest].contact_role ADD CONSTRAINT contact_role_contact_role2company_fkey FOREIGN KEY (contact_role2company) REFERENCES [SchemaTest].company(id);

GO
IF OBJECT_ID('SchemaTest.[contact_role_contact_role2company_fkey]') IS NULL 
	ALTER TABLE [SchemaTest].contact_role ADD CONSTRAINT contact_role_contact_role2contact_fkey FOREIGN KEY (contact_role2contact) REFERENCES [SchemaTest].contact(id);

-- Unique 
GO
IF OBJECT_ID('SchemaTest.[contact_role_un]', 'UQ') IS NULL 
	ALTER TABLE [SchemaTest].contact_role ADD CONSTRAINT contact_role_un UNIQUE (contact_role2company, contact_role2contact, [name]);