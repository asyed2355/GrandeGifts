IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
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
    [GivenNames] nvarchar(128) NOT NULL,
    [Surname] nvarchar(128) NOT NULL,
    [JoinDate] datetime2 NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [TblCategories] (
    [CategoryId] int NOT NULL IDENTITY,
    [CategoryName] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [InUse] bit NOT NULL,
    CONSTRAINT [PK_TblCategories] PRIMARY KEY ([CategoryId])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [TblAddresses] (
    [AddressId] uniqueidentifier NOT NULL,
    [StreetAddress] nvarchar(max) NOT NULL,
    [Suburb] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NULL,
    [Postcode] int NOT NULL,
    [AddressType] nvarchar(max) NULL,
    [PreferredShippingAddress] bit NOT NULL,
    [ApplicationUserId] nvarchar(450) NULL,
    CONSTRAINT [PK_TblAddresses] PRIMARY KEY ([AddressId]),
    CONSTRAINT [FK_TblAddresses_AspNetUsers_ApplicationUserId] FOREIGN KEY ([ApplicationUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [TblHampers] (
    [HamperId] int NOT NULL IDENTITY,
    [HamperName] nvarchar(max) NOT NULL,
    [Price] float NOT NULL,
    [Products] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Supplier] nvarchar(max) NULL,
    [ImageUrl] nvarchar(max) NULL,
    [InUse] bit NOT NULL,
    [CategoryId] int NOT NULL,
    CONSTRAINT [PK_TblHampers] PRIMARY KEY ([HamperId]),
    CONSTRAINT [FK_TblHampers_TblCategories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [TblCategories] ([CategoryId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

CREATE INDEX [IX_TblAddresses_ApplicationUserId] ON [TblAddresses] ([ApplicationUserId]);

GO

CREATE INDEX [IX_TblHampers_CategoryId] ON [TblHampers] ([CategoryId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190613002751_InitialMigration', N'2.1.8-servicing-32085');

GO

ALTER TABLE [TblHampers] DROP CONSTRAINT [FK_TblHampers_TblCategories_CategoryId];

GO

ALTER TABLE [TblCategories] DROP CONSTRAINT [PK_TblCategories];

GO

EXEC sp_rename N'[TblCategories]', N'Category';

GO

ALTER TABLE [Category] ADD CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId]);

GO

CREATE TABLE [TblLineItems] (
    [OrderId] uniqueidentifier NOT NULL,
    [HamperId] int NOT NULL,
    [Quantity] int NOT NULL,
    CONSTRAINT [PK_TblLineItems] PRIMARY KEY ([HamperId], [OrderId])
);

GO

ALTER TABLE [TblHampers] ADD CONSTRAINT [FK_TblHampers_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190614051352_AddingOrderAndLineItem', N'2.1.8-servicing-32085');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190614052810_AddingOrders', N'2.1.8-servicing-32085');

GO

CREATE TABLE [Order] (
    [OrderId] uniqueidentifier NOT NULL,
    [Price] float NOT NULL,
    [StreetAddress] nvarchar(max) NULL,
    [Suburb] nvarchar(max) NULL,
    [State] nvarchar(max) NULL,
    [Postcode] int NOT NULL,
    [UserId] nvarchar(max) NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([OrderId])
);

GO

CREATE INDEX [IX_TblLineItems_OrderId] ON [TblLineItems] ([OrderId]);

GO

ALTER TABLE [TblLineItems] ADD CONSTRAINT [FK_TblLineItems_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([OrderId]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190614052901_AddingOrder_2', N'2.1.8-servicing-32085');

GO

ALTER TABLE [Order] ADD [DateOrdered] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190614055258_AddDateTimeToOrders', N'2.1.8-servicing-32085');

GO

