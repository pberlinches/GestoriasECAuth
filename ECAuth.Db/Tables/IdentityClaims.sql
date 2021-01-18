CREATE TABLE [auth].[IdentityClaims] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [IdentityResourceId] INT            NOT NULL,
    [Type]               NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_IdentityClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IdentityClaims_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [auth].[IdentityResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_IdentityClaims_IdentityResourceId]
    ON [auth].[IdentityClaims]([IdentityResourceId] ASC);

