CREATE TABLE [auth].[PersistedGrants] (
    [Key]          NVARCHAR (200) NOT NULL,
    [ClientId]     NVARCHAR (200) NOT NULL,
    [CreationTime] DATETIME2 (7)  NOT NULL,
    [Data]         NVARCHAR (MAX) NOT NULL,
    [Expiration]   DATETIME2 (7)  NULL,
    [SubjectId]    NVARCHAR (200) NULL,
    [Type]         NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_PersistedGrants] PRIMARY KEY CLUSTERED ([Key] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PersistedGrants_SubjectId_ClientId_Type]
    ON [auth].[PersistedGrants]([SubjectId] ASC, [ClientId] ASC, [Type] ASC);

