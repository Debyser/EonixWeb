/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 101,'AT','AUT','Austria' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='AT');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 102,'BE','BEL','Belgium' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='BE');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 103,'BG','BGR','Bulgaria' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='BG');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 104,'HR','HRV','Croatia' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='HR');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 105,'CY','CYP','Cyprus' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='CY');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 106,'CZ','CZE','Czech Republic' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='CZ');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 107,'DK','DNK','Denmark' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='DK');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 108,'EE','EST','Estonia' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='EE');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 109,'FI','FIN','Finland' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='FI');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 110,'FR','FRA','France' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='FR');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 111,'DE','DEU','Germany' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='DE');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 112,'GR','GRC','Greece' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='GR');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 113,'HU','HUN','Hungary' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='HU');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 114,'IE','IRL','Ireland, Republic of (EIRE)' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='IE');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 115,'IT','ITA','Italy' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='IT');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 116,'LV','LVA','Latvia' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='LV');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 117,'LT','LTU','Lithuania' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='LT');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 118,'LU','LUX','Luxembourg' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='LU');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 119,'MT','MLT','Malta' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='MT');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 120,'NL','NLD','Netherlands' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='NL');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 121,'PL','POL','Poland' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='PL');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 122,'PT','PRT','Portugal' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='PT');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 123,'RO','ROU','Romania' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='RO');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 124,'SK','SVK','Slovakia' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='SK');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 125,'SI','SVN','Slovenia' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='SI');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 126,'ES','ESP','Spain' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='ES');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 127,'SE','SWE','Sweden' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='SE');
GO
INSERT INTO [dbo].country (id,iso_2_code,iso_3_code,name) select 128,'GB','GBR','United Kingdom' where NOT EXISTS (SELECT 1 FROM [dbo].COUNTRY where iso_2_code='GB');
GO