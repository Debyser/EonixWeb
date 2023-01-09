-- public.contact_role definition

-- Drop table

-- DROP TABLE public.contact_role;

--CREATE TABLE contact_role (
--	id INT IDENTITY(1,1),
--	contact_role2company INT NOT NULL,
--	contact_role2contact INT NOT NULL,
--	"name" varchar(40) NOT NULL,	active bit DEFAULT 1 NOT NULL,
--	CONSTRAINT contact_role_pk PRIMARY KEY (id),
--	CONSTRAINT contact_role_un UNIQUE (contact_role2company, contact_role2contact, name)
--);


CREATE TABLE [myschema].contact_role (
	id INT IDENTITY(1,1),
	contact_role2company INT NOT NULL,
	contact_role2contact INT NOT NULL,
	"name" varchar(40) NOT NULL,
	active bit DEFAULT 1 NOT NULL,
	CONSTRAINT contact_role_pk PRIMARY KEY (id),
);
-- public.contact_role foreign keys

IF OBJECT_ID('myschema.[contact_role_contact_role2company_fkey]', 'UQ') IS NULL 
	ALTER TABLE contact_role ADD CONSTRAINT contact_role_contact_role2company_fkey FOREIGN KEY (contact_role2company) REFERENCES [myschema].company(id);

IF OBJECT_ID('myschema.[contact_role_contact_role2company_fkey]', 'UQ') IS NULL 
	ALTER TABLE contact_role ADD CONSTRAINT contact_role_contact_role2contact_fkey FOREIGN KEY (contact_role2contact) REFERENCES [myschema].contact(id);

-- Unique 
IF OBJECT_ID('myschema.[contact_role_un]', 'UQ') IS NULL 
	ALTER TABLE contact_role ADD CONSTRAINT contact_role_un UNIQUE (contact_role2company, contact_role2contact, name);