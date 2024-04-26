CREATE TABLE [dbo].[ContactEmail]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[ContactId] int not null,
	[EmailAddressId] int not null
)
