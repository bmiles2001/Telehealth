IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Practice] (
    [PracticeId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Description] nvarchar(max) NULL,
    [IsVirtual] bit NOT NULL,
    CONSTRAINT [PK_Practice] PRIMARY KEY CLUSTERED ([PracticeId])
);

CREATE TABLE [Profile] (
    [ProfileId] int NOT NULL IDENTITY,
    [DateOfBirth] date NOT NULL,
    [Email] nvarchar(75) NOT NULL,
    [FirstName] nvarchar(75) NOT NULL,
    [LastName] nvarchar(75) NOT NULL,
    [PhoneNumber] nvarchar(25) NOT NULL,
    [ProfileType] int NOT NULL,
    CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED ([ProfileId])
);

CREATE TABLE [Location] (
    [LocationId] int NOT NULL IDENTITY,
    [Name] nvarchar(250) NOT NULL,
    [PracticeId] int NOT NULL,
    [StreetAddress] nvarchar(150) NOT NULL,
    [City] nvarchar(50) NOT NULL,
    [State] nvarchar(25) NOT NULL,
    [Zip] nvarchar(15) NOT NULL,
    [ClosingTime] time(0) NOT NULL,
    [OpeningTime] time(0) NOT NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationId]),
    CONSTRAINT [FK_Location_Practice_PracticeId] FOREIGN KEY ([PracticeId]) REFERENCES [Practice] ([PracticeId])
);

CREATE TABLE [Provider] (
    [ProviderId] int NOT NULL IDENTITY,
    [ProfileId] int NOT NULL,
    [Rate] decimal(18,0) NOT NULL,
    CONSTRAINT [PK_Provider] PRIMARY KEY CLUSTERED ([ProviderId]),
    CONSTRAINT [FK_Provider_Profile_ProfileId] FOREIGN KEY ([ProfileId]) REFERENCES [Profile] ([ProfileId])
);

CREATE TABLE [Appointment] (
    [AppointmentId] int NOT NULL IDENTITY,
    [State] int NOT NULL,
    [PatientId] int NOT NULL,
    [ProviderId] int NULL,
    [LocationId] int NOT NULL,
    [AppointmentTime] datetimeoffset(0) NOT NULL,
    [AppointmentType] nvarchar(max) NOT NULL,
    [Duration] time(0) NOT NULL,
    [Notes] nvarchar(max) NULL,
    CONSTRAINT [PK_Appointment] PRIMARY KEY CLUSTERED ([AppointmentId]),
    CONSTRAINT [FK_Appointment_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([LocationId]),
    CONSTRAINT [FK_Appointment_Profile_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Profile] ([ProfileId]),
    CONSTRAINT [FK_Appointment_Provider_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Provider] ([ProviderId])
);

CREATE INDEX [IX_Appointment_LocationId] ON [Appointment] ([LocationId]);

CREATE INDEX [IX_Appointment_PatientId] ON [Appointment] ([PatientId]);

CREATE INDEX [IX_Appointment_ProviderId] ON [Appointment] ([ProviderId]);

CREATE INDEX [IX_Location_PracticeId] ON [Location] ([PracticeId]);

CREATE INDEX [IX_Profile_ProfileType] ON [Profile] ([ProfileType]);

CREATE INDEX [IX_Provider_ProfileId] ON [Provider] ([ProfileId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241126001548_CreateDatabase', N'9.0.0');

COMMIT;
GO

