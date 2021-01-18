CREATE TABLE [auth].[ClientIdPRestrictions] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [ClientId] INT            NOT NULL,
    [Provider] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ClientIdPRestrictions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientIdPRestrictions_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [auth].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientIdPRestrictions_ClientId]
    ON [auth].[ClientIdPRestrictions]([ClientId] ASC);

