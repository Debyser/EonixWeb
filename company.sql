
CREATE TABLE company (
	id  INT IDENTITY(1,1),
	"name" varchar(40) NOT NULL,
	company2address INT NOT NULL,
	CONSTRAINT company_pk PRIMARY KEY (id)
);


-- public.company foreign keys

ALTER TABLE company ADD CONSTRAINT company_company2address_fkey FOREIGN KEY (company2address) REFERENCES address(id);