CREATE TABLE [auth].[ApiSecrets] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [ApiResourceId] INT             NOT NULL,
    [Description]   NVARCHAR (1000) NULL,
    [Expiration]    DATETIME2 (7)   NULL,
    [Type]          NVARCHAR (250)  NULL,
    [Value]         NVARCHAR (2000) NULL,
    CONSTRAINT [PK_ApiSecrets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ApiSecrets_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [auth].[ApiResources] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_ApiSecrets_ApiResourceId]
    ON [auth].[ApiSecrets]([ApiResourceId] ASC);

