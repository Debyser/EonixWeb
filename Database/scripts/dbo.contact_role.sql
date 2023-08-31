CREATE TABLE [dbo].[contact_role] (
    [id]                   INT          IDENTITY (2000, 1) NOT NULL,
    [contact_role2company] INT          NOT NULL,
    [contact_role2contact] INT          NOT NULL,
    [name]                 VARCHAR (40) NOT NULL,
    [active]               BIT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [pk_contact_role] PRIMARY KEY CLUSTERED ([id] ASC),
);

GO
ALTER TABLE [dbo].contact_role ADD CONSTRAINT contact_role_contact_role2company_fkey FOREIGN KEY (contact_role2company) REFERENCES [dbo].company(id);

GO
ALTER TABLE [dbo].contact_role ADD CONSTRAINT contact_role_contact_role2contact_fkey FOREIGN KEY (contact_role2contact) REFERENCES [dbo].contact(id);

-- Unique 
GO
ALTER TABLE [dbo].contact_role ADD CONSTRAINT contact_role_un UNIQUE (contact_role2company, contact_role2contact, [name]);