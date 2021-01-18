CREATE TABLE [auth].[Clients] (
    [Id]                                INT             IDENTITY (1, 1) NOT NULL,
    [AbsoluteRefreshTokenLifetime]      INT             NOT NULL,
    [AccessTokenLifetime]               INT             NOT NULL,
    [AccessTokenType]                   INT             NOT NULL,
    [AllowAccessTokensViaBrowser]       BIT             NOT NULL,
    [AllowOfflineAccess]                BIT             NOT NULL,
    [AllowPlainTextPkce]                BIT             NOT NULL,
    [AllowRememberConsent]              BIT             NOT NULL,
    [AlwaysIncludeUserClaimsInIdToken]  BIT             NOT NULL,
    [AlwaysSendClientClaims]            BIT             NOT NULL,
    [AuthorizationCodeLifetime]         INT             NOT NULL,
    [BackChannelLogoutSessionRequired]  BIT             NOT NULL,
    [BackChannelLogoutUri]              NVARCHAR (2000) NULL,
    [ClientClaimsPrefix]                NVARCHAR (200)  NULL,
    [ClientId]                          NVARCHAR (200)  NOT NULL,
    [ClientName]                        NVARCHAR (200)  NULL,
    [ClientUri]                         NVARCHAR (2000) NULL,
    [ConsentLifetime]                   INT             NULL,
    [Description]                       NVARCHAR (1000) NULL,
    [EnableLocalLogin]                  BIT             NOT NULL,
    [Enabled]                           BIT             NOT NULL,
    [FrontChannelLogoutSessionRequired] BIT             NOT NULL,
    [FrontChannelLogoutUri]             NVARCHAR (2000) NULL,
    [IdentityTokenLifetime]             INT             NOT NULL,
    [IncludeJwtId]                      BIT             NOT NULL,
    [LogoUri]                           NVARCHAR (2000) NULL,
    [PairWiseSubjectSalt]               NVARCHAR (200)  NULL,
    [ProtocolType]                      NVARCHAR (200)  NOT NULL,
    [RefreshTokenExpiration]            INT             NOT NULL,
    [RefreshTokenUsage]                 INT             NOT NULL,
    [RequireClientSecret]               BIT             NOT NULL,
    [RequireConsent]                    BIT             NOT NULL,
    [RequirePkce]                       BIT             NOT NULL,
    [SlidingRefreshTokenLifetime]       INT             NOT NULL,
    [UpdateAccessTokenClaimsOnRefresh]  BIT             NOT NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Clients_ClientId]
    ON [auth].[Clients]([ClientId] ASC);

