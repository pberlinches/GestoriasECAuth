CREATE TABLE [auth].[ClientRedirectUris] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [ClientId]    INT             NOT NULL,
    [RedirectUri] NVARCHAR (2000) NOT NULL,
    CONSTRAINT [PK_ClientRedirectUris] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [auth].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientRedirectUris_ClientId]
    ON [auth].[ClientRedirectUris]([ClientId] ASC);

