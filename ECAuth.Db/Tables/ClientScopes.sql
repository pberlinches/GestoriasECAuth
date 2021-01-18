CREATE TABLE [auth].[ClientScopes] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [ClientId] INT            NOT NULL,
    [Scope]    NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ClientScopes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientScopes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [auth].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientScopes_ClientId]
    ON [auth].[ClientScopes]([ClientId] ASC);

