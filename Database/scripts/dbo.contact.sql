CREATE TABLE [contact] (
    [id]              INT          IDENTITY (2000, 1) NOT NULL,
    [last_name]        VARCHAR (40) NOT NULL,
    [first_name]       VARCHAR (40) NOT NULL,
    [contact2address] INT          NOT NULL,
    [active]          BIT          DEFAULT ((1)) NOT NULL,
    [creation_time]   DATETIME     NULL,
    [phone_number] VARCHAR(30) NULL, 
    CONSTRAINT [pk_contact] PRIMARY KEY CLUSTERED ([id] ASC),
 
);


GO
ALTER TABLE [contact] ADD CONSTRAINT contact_contact2address_fkey FOREIGN KEY (contact2address) REFERENCES [address](id);