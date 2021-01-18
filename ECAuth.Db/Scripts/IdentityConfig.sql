MERGE INTO [auth].[IdentityResources] AS Target
USING ( VALUES 
--	[Name],		[Description],													[DisplayName],			[Emphasize],	[Enabled],	[Required],	[ShowInDiscoveryDocument]
   ('openid',	NULL,															'Your user identifier',	0,				1,			1,			1),
   ('profile',	'Your user profile information (first name, last name, etc.)',	'User profile',			1,				1,			0,			1)
)
AS Source ([Name], [Description], [DisplayName], [Emphasize], [Enabled], [Required], [ShowInDiscoveryDocument])
ON Target.[Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([Name], [Description], [DisplayName], [Emphasize], [Enabled], [Required], [ShowInDiscoveryDocument])
	VALUES ([Name], [Description], [DisplayName], [Emphasize], [Enabled], [Required], [ShowInDiscoveryDocument]);


DECLARE @openid INT
DECLARE @profile INT
SELECT @openid = Id FROM [auth].[IdentityResources] WHERE [Name] = 'openid'
SELECT @profile = Id FROM [auth].[IdentityResources] WHERE [Name] = 'profile'


MERGE INTO [auth].[IdentityClaims] AS Target
USING ( VALUES 
--	[IdentityResourceId],	[Type]
   (@openid,				'sub'),
   (@profile,				'name'),
   (@profile,				'family_name'),
   (@profile,				'given_name'),
   (@profile,				'middle_name'),
   (@profile,				'nickname'),
   (@profile,				'preferred_username'),
   (@profile,				'profile'),
   (@profile,				'picture'),
   (@profile,				'website'),
   (@profile,				'gender'),
   (@profile,				'birthdate'),
   (@profile,				'zoneinfo'),
   (@profile,				'locale'),
   (@profile,				'updated_at'),
   (@profile,				'role'),
   (@profile,				'id')
)
AS Source ([IdentityResourceId], [Type])
ON Target.[IdentityResourceId] = Source.[IdentityResourceId] and Target.[Type] = Source.[Type]
WHEN NOT MATCHED BY TARGET THEN
	INSERT ([IdentityResourceId], [Type])
	VALUES ([IdentityResourceId], [Type]);
