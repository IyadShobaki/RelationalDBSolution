CREATE TABLE [dbo].[ContactPhoneNumbers]
(
	[Id] INT NOT NULL PRIMARY KEY identity,
	[ContactId] int not null,
	[PhoneNumberId] int not null
)
