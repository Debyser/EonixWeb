CREATE TABLE address (
	id INT IDENTITY(1,1),
	zipcode varchar(20) NOT NULL,
	street varchar(50) NOT NULL,
	box_number varchar(30) NOT NULL,
	city varchar(40) NULL,
	active bit DEFAULT 1 NOT NULL,
	address2country int NOT NULL,
	CONSTRAINT address_pk PRIMARY KEY (id)
);


-- public.address foreign keys

ALTER TABLE address ADD CONSTRAINT address_address2country_fkey FOREIGN KEY (address2country) REFERENCES country(id);
