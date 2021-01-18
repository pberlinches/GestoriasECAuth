CREATE TABLE [auth].[IdentityResources] (
    [Id]                      INT             IDENTITY (1, 1) NOT NULL,
    [Description]             NVARCHAR (1000) NULL,
    [DisplayName]             NVARCHAR (200)  NULL,
    [Emphasize]               BIT             NOT NULL,
    [Enabled]                 BIT             NOT NULL,
    [Name]                    NVARCHAR (200)  NOT NULL,
    [Required]                BIT             NOT NULL,
    [ShowInDiscoveryDocument] BIT             NOT NULL,
    CONSTRAINT [PK_IdentityResources] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_IdentityResources_Name]
    ON [auth].[IdentityResources]([Name] ASC);

