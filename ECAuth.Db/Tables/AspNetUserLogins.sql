﻿CREATE TABLE [auth].[AspNetUserLogins] (
    [LoginProvider]       NVARCHAR (450) NOT NULL,
    [ProviderKey]         NVARCHAR (450) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              NVARCHAR (450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [auth].[AspNetUsers] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId]
    ON [auth].[AspNetUserLogins]([UserId] ASC);
