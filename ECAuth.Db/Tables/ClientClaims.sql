CREATE TABLE [auth].[ClientClaims] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [ClientId] INT            NOT NULL,
    [Type]     NVARCHAR (250) NOT NULL,
    [Value]    NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ClientClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientClaims_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [auth].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientClaims_ClientId]
    ON [auth].[ClientClaims]([ClientId] ASC);

