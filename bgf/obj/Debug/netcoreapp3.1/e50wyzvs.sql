IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Student] (
    [Id] nvarchar(450) NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [MiddleName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Gender_ID] int NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [BirthPlace] nvarchar(max) NULL,
    [Address1] nvarchar(max) NULL,
    [Address2] nvarchar(max) NULL,
    [Phone] nvarchar(max) NULL,
    [Year] int NOT NULL,
    [StudentProfile_ID] int NOT NULL,
    [Deactivated] bit NOT NULL,
    [CreationDate] datetime2 NOT NULL,
    [LastModificationDate] datetime2 NOT NULL,
    [LastLoginDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [StudentRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_StudentRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [StudentUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_StudentUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StudentUserClaims_Student_UserId] FOREIGN KEY ([UserId]) REFERENCES [Student] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [StudentUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_StudentUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_StudentUserLogins_Student_UserId] FOREIGN KEY ([UserId]) REFERENCES [Student] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [StudentUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_StudentUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_StudentUserTokens_Student_UserId] FOREIGN KEY ([UserId]) REFERENCES [Student] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [StudentRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_StudentRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StudentRoleClaims_StudentRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [StudentRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [StudentUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_StudentUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_StudentUserRoles_StudentRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [StudentRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentUserRoles_Student_UserId] FOREIGN KEY ([UserId]) REFERENCES [Student] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [EmailIndex] ON [Student] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Student] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_StudentRoleClaims_RoleId] ON [StudentRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [StudentRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_StudentUserClaims_UserId] ON [StudentUserClaims] ([UserId]);

GO

CREATE INDEX [IX_StudentUserLogins_UserId] ON [StudentUserLogins] ([UserId]);

GO

CREATE INDEX [IX_StudentUserRoles_RoleId] ON [StudentUserRoles] ([RoleId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201026123605_Initial', N'3.1.9');

GO

