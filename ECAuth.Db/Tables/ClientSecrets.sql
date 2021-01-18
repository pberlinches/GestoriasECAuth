CREATE TABLE [auth].[ClientSecrets] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [ClientId]    INT             NOT NULL,
    [Description] NVARCHAR (2000) NULL,
    [Expiration]  DATETIME2 (7)   NULL,
    [Type]        NVARCHAR (250)  NULL,
    [Value]       NVARCHAR (2000) NOT NULL,
    CONSTRAINT [PK_ClientSecrets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClientSecrets_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [auth].[Clients] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ClientSecrets_ClientId]
    ON [auth].[ClientSecrets]([ClientId] ASC);

