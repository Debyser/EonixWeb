CREATE TABLE [dbo].[company] (
    [id]              INT          IDENTITY (1, 1) NOT NULL,
    [name]            VARCHAR (40) NOT NULL,
    [company2address] INT          NOT NULL,
    [active]          BIT          DEFAULT ((1)) NOT NULL,
    CONSTRAINT [pK_company] PRIMARY KEY CLUSTERED ([id] ASC)
);

GO
ALTER TABLE [company] ADD CONSTRAINT [company_company2address_fkey] FOREIGN KEY ([company2address]) REFERENCES [address](id);
