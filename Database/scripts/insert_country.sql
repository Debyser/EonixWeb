BULK INSERT SchemaTest.[country]
        FROM 'C:\Demo\CountryCode.csv'
            WITH
    (
                FIELDTERMINATOR = ';',
                ROWTERMINATOR = '\n',
				FIRSTROW=2
    )
