﻿-- public.contact definition

-- Drop table

-- DROP TABLE public.contact;


CREATE TABLE contact (
	id INT IDENTITY(1,1),
	lastname varchar(40) NOT NULL,
	firstname varchar(40) NOT NULL,
	contact2address int NOT NULL,
	CONSTRAINT contact_pk PRIMARY KEY (id)
);