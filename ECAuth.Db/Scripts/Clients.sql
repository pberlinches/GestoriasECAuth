MERGE INTO [auth].[Clients] AS Target
USING ( VALUES 
--	[ClientId],	[ClientName],	[AbsoluteRefreshTokenLifetime],	[AccessTokenLifetime],	[AccessTokenType],  [AllowAccessTokensViaBrowser],	[AllowOfflineAccess],	[AllowPlainTextPkce],	[AllowRememberConsent],	[AlwaysIncludeUserClaimsInIdToken],	[AlwaysSendClientClaims],	[AuthorizationCodeLifetime],	[BackChannelLogoutSessionRequired],	[BackChannelLogoutUri],	[ClientClaimsPrefix],	[ClientUri],	[ConsentLifetime],	[Description],	[EnableLocalLogin],	[Enabled],	[FrontChannelLogoutSessionRequired],	[FrontChannelLogoutUri],					[IdentityTokenLifetime],	[IncludeJwtId],	[LogoUri],	[PairWiseSubjectSalt],	[ProtocolType],	[RefreshTokenExpiration],	[RefreshTokenUsage],	[RequireClientSecret],	[RequireConsent],	[RequirePkce],	[SlidingRefreshTokenLifetime],	[UpdateAccessTokenClaimsOnRefresh]
   ('coxerp',	'Cox ERP',		2592000,						3600,					0,					0,								1,						0,						1,						0,									0,							300,							1,									NULL,					'client_',				NULL,			NULL,				NULL,			1,					1,			1,										'http://localhost:55814/Home/OidcSignOut',	300,						0,				NULL,		NULL,					'oidc',			1,							1,						0,						0,					0,				1296000,						0),
   ('Aldebaran',	'Aldebaran',2592000,						3600,					0,					0,								1,						0,						1,						0,									0,							300,							1,									NULL,					'client_',				NULL,			NULL,				NULL,			1,					1,			1,										'http://localhost:55895/Home/OidcSignOut',	300,						0,				NULL,		NULL,					'oidc',			1,							1,						0,						0,					0,				1296000,						0),
   ('NCSGDP',	'NCS Gestor de Proyectos',2592000,						3600,					0,					0,								1,						0,						1,						0,									0,							300,							1,									NULL,					'client_',				NULL,			NULL,				NULL,			1,					1,			1,										'http://localhost:55816/Home/OidcSignOut',	300,						0,				NULL,		NULL,					'oidc',			1,							1,						0,						0,					0,				1296000,						0)
)
AS Source ([ClientId], [ClientName], [AbsoluteRefreshTokenLifetime], [AccessTokenLifetime], [AccessTokenType], [AllowAccessTokensViaBrowser], [AllowOfflineAccess], [AllowPlainTextPkce], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [AlwaysSendClientClaims], [AuthorizationCodeLifetime], [BackChannelLogoutSessionRequired], [BackChannelLogoutUri], [ClientClaimsPrefix], [ClientUri], [ConsentLifetime], [Description], [EnableLocalLogin], [Enabled], [FrontChannelLogoutSessionRequired], [FrontChannelLogoutUri], [IdentityTokenLifetime], [IncludeJwtId], [LogoUri], [PairWiseSubjectSalt], [ProtocolType], [RefreshTokenExpiration], [RefreshTokenUsage], [RequireClientSecret], [RequireConsent], [RequirePkce], [SlidingRefreshTokenLifetime], [UpdateAccessTokenClaimsOnRefresh])
ON Target.[ClientId] = Source.[ClientId]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ClientId], [ClientName], [AbsoluteRefreshTokenLifetime], [AccessTokenLifetime], [AccessTokenType], [AllowAccessTokensViaBrowser], [AllowOfflineAccess], [AllowPlainTextPkce], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [AlwaysSendClientClaims], [AuthorizationCodeLifetime], [BackChannelLogoutSessionRequired], [BackChannelLogoutUri], [ClientClaimsPrefix], [ClientUri], [ConsentLifetime], [Description], [EnableLocalLogin], [Enabled], [FrontChannelLogoutSessionRequired], [FrontChannelLogoutUri], [IdentityTokenLifetime], [IncludeJwtId], [LogoUri], [PairWiseSubjectSalt], [ProtocolType], [RefreshTokenExpiration], [RefreshTokenUsage], [RequireClientSecret], [RequireConsent], [RequirePkce], [SlidingRefreshTokenLifetime], [UpdateAccessTokenClaimsOnRefresh])
	VALUES ([ClientId], [ClientName], [AbsoluteRefreshTokenLifetime], [AccessTokenLifetime], [AccessTokenType], [AllowAccessTokensViaBrowser], [AllowOfflineAccess], [AllowPlainTextPkce], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [AlwaysSendClientClaims], [AuthorizationCodeLifetime], [BackChannelLogoutSessionRequired], [BackChannelLogoutUri], [ClientClaimsPrefix], [ClientUri], [ConsentLifetime], [Description], [EnableLocalLogin], [Enabled], [FrontChannelLogoutSessionRequired], [FrontChannelLogoutUri], [IdentityTokenLifetime], [IncludeJwtId], [LogoUri], [PairWiseSubjectSalt], [ProtocolType], [RefreshTokenExpiration], [RefreshTokenUsage], [RequireClientSecret], [RequireConsent], [RequirePkce], [SlidingRefreshTokenLifetime], [UpdateAccessTokenClaimsOnRefresh]);


DECLARE @appId INT
SELECT @appId = Id FROM [auth].[Clients] WHERE [ClientId] = 'NCSGDP'


MERGE INTO [auth].[ClientGrantTypes] AS Target
USING ( VALUES 
--	[ClientId],	[GrantType]
   (@appId,	'hybrid')
)
AS Source ([ClientId],	[GrantType])
ON Target.[ClientId] = Source.[ClientId] and Target.[GrantType] = Source.[GrantType]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ClientId],	[GrantType])
	VALUES ([ClientId],	[GrantType]);


MERGE INTO [auth].[ClientScopes] AS Target
USING ( VALUES 
--	[ClientId],	[Scope]
   (@appId,	'openid'),
   (@appId,	'profile')
)
AS Source ([ClientId], [Scope])
ON Target.[ClientId] = Source.[ClientId] and Target.[Scope] = Source.[Scope]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ClientId], [Scope])
	VALUES ([ClientId], [Scope]);


MERGE INTO [auth].[ClientSecrets] AS Target
USING ( VALUES 
--	[ClientId],	[Type],			[Value]
   (@appId,	'SharedSecret', (SELECT CAST(N'' AS XML).value('xs:base64Binary(xs:hexBinary(sql:column("bin")))', 'VARCHAR(MAX)') FROM (SELECT HASHBYTES('SHA2_256','secret') AS bin) AS bin_sql_server_temp))
)
AS Source ([ClientId], [Type], [Value])
ON Target.[ClientId] = Source.[ClientId] and Target.[Type] = Source.[Type]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ClientId], [Type], [Value])
	VALUES ([ClientId], [Type], [Value]);


MERGE INTO [auth].[ClientRedirectUris] AS Target
USING ( VALUES 
--	[ClientId],	[RedirectUri]
   (@appId,	'http://localhost:55816/')
   )
AS Source ([ClientId], [RedirectUri])
ON Target.[ClientId] = Source.[ClientId] and Target.[RedirectUri] = Source.[RedirectUri]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ClientId], [RedirectUri])
	VALUES ([ClientId], [RedirectUri]);


MERGE INTO [auth].[ClientPostLogoutRedirectUris] AS Target
USING ( VALUES 
--	[ClientId],	[PostLogoutRedirectUri]
   (@appId,	'http://localhost:55816/')
   )
AS Source ([ClientId], [PostLogoutRedirectUri])
ON Target.[ClientId] = Source.[ClientId] and Target.[PostLogoutRedirectUri] = Source.[PostLogoutRedirectUri]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([ClientId], [PostLogoutRedirectUri])
	VALUES ([ClientId], [PostLogoutRedirectUri]);