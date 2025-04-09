-- Check if each table exists before creating it
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AgeRestrictionLookup')
BEGIN
    create table AgeRestrictionLookup
    (
        agerestrictionid int identity
            constraint PK_AgeRestrictionLookup
                primary key,
        code             nvarchar(10)  not null,
        description      nvarchar(200) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoles')
BEGIN
    create table AspNetRoles
    (
        Id               uniqueidentifier not null
            constraint PK_AspNetRoles
                primary key,
        Name             nvarchar(256),
        NormalizedName   nvarchar(256),
        ConcurrencyStamp nvarchar(max)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetRoleClaims')
BEGIN
    create table AspNetRoleClaims
    (
        Id         int identity
            constraint PK_AspNetRoleClaims
                primary key,
        RoleId     uniqueidentifier not null
            constraint FK_AspNetRoleClaims_AspNetRoles_RoleId
                references AspNetRoles
                on delete cascade,
        ClaimType  nvarchar(max),
        ClaimValue nvarchar(max)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AspNetRoleClaims_RoleId')
BEGIN
    create index IX_AspNetRoleClaims_RoleId
        on AspNetRoleClaims (RoleId)
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'RoleNameIndex')
BEGIN
    create unique index RoleNameIndex
        on AspNetRoles (NormalizedName)
        where [NormalizedName] IS NOT NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUsers')
BEGIN
    create table AspNetUsers
    (
        Id                   uniqueidentifier not null
            constraint PK_AspNetUsers
                primary key,
        FirstName            nvarchar(max)    not null,
        LastName             nvarchar(max)    not null,
        CreatedAt            datetime2        not null,
        IsActive             bit              not null,
        UserName             nvarchar(256),
        NormalizedUserName   nvarchar(256),
        Email                nvarchar(256),
        NormalizedEmail      nvarchar(256),
        EmailConfirmed       bit              not null,
        PasswordHash         nvarchar(max),
        SecurityStamp        nvarchar(max),
        ConcurrencyStamp     nvarchar(max),
        PhoneNumber          nvarchar(max),
        PhoneNumberConfirmed bit              not null,
        TwoFactorEnabled     bit              not null,
        LockoutEnd           datetimeoffset,
        LockoutEnabled       bit              not null,
        AccessFailedCount    int              not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserClaims')
BEGIN
    create table AspNetUserClaims
    (
        Id         int identity
            constraint PK_AspNetUserClaims
                primary key,
        UserId     uniqueidentifier not null
            constraint FK_AspNetUserClaims_AspNetUsers_UserId
                references AspNetUsers
                on delete cascade,
        ClaimType  nvarchar(max),
        ClaimValue nvarchar(max)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AspNetUserClaims_UserId')
BEGIN
    create index IX_AspNetUserClaims_UserId
        on AspNetUserClaims (UserId)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserLogins')
BEGIN
    create table AspNetUserLogins
    (
        LoginProvider       nvarchar(450)    not null,
        ProviderKey         nvarchar(450)    not null,
        ProviderDisplayName nvarchar(max),
        UserId              uniqueidentifier not null
            constraint FK_AspNetUserLogins_AspNetUsers_UserId
                references AspNetUsers
                on delete cascade,
        constraint PK_AspNetUserLogins
            primary key (LoginProvider, ProviderKey)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AspNetUserLogins_UserId')
BEGIN
    create index IX_AspNetUserLogins_UserId
        on AspNetUserLogins (UserId)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserRoles')
BEGIN
    create table AspNetUserRoles
    (
        UserId uniqueidentifier not null
            constraint FK_AspNetUserRoles_AspNetUsers_UserId
                references AspNetUsers
                on delete cascade,
        RoleId uniqueidentifier not null
            constraint FK_AspNetUserRoles_AspNetRoles_RoleId
                references AspNetRoles
                on delete cascade,
        constraint PK_AspNetUserRoles
            primary key (UserId, RoleId)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_AspNetUserRoles_RoleId')
BEGIN
    create index IX_AspNetUserRoles_RoleId
        on AspNetUserRoles (RoleId)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'AspNetUserTokens')
BEGIN
    create table AspNetUserTokens
    (
        UserId        uniqueidentifier not null
            constraint FK_AspNetUserTokens_AspNetUsers_UserId
                references AspNetUsers
                on delete cascade,
        LoginProvider nvarchar(450)    not null,
        Name          nvarchar(450)    not null,
        Value         nvarchar(max),
        constraint PK_AspNetUserTokens
            primary key (UserId, LoginProvider, Name)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'EmailIndex')
BEGIN
    create index EmailIndex
        on AspNetUsers (NormalizedEmail)
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'UserNameIndex')
BEGIN
    create unique index UserNameIndex
        on AspNetUsers (NormalizedUserName)
        where [NormalizedUserName] IS NOT NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Locations')
BEGIN
    create table Locations
    (
        locationid       int identity
            constraint PK_Locations
                primary key,
        locationname     nvarchar(100)                 not null,
        address          nvarchar(max)                 not null,
        suburb           nvarchar(max)                 not null,
        postalcode       nvarchar(max)                 not null,
        state            nvarchar(max)                 not null,
        country          nvarchar(max)                 not null,
        latitude         float                         not null,
        longitude        float                         not null,
        parkingavailable bit default CONVERT([bit], 0) not null,
        active           bit default CONVERT([bit], 1) not null,
        createdbyid      int                           not null,
        createdat        datetime2                     not null,
        updatedid        int,
        updatedat        datetime2
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'RefreshTokens')
BEGIN
    create table RefreshTokens
    (
        Id         int identity
            constraint PK_RefreshTokens
                primary key,
        Token      nvarchar(max)    not null,
        ExpiryDate datetime2        not null,
        IsRevoked  bit              not null,
        CreatedAt  datetime2        not null,
        UserId     uniqueidentifier not null
            constraint FK_RefreshTokens_AspNetUsers_UserId
                references AspNetUsers
                on delete cascade
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_RefreshTokens_UserId')
BEGIN
    create index IX_RefreshTokens_UserId
        on RefreshTokens (UserId)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
BEGIN
    create table Roles
    (
        roleid    int identity
            constraint PK_Roles
                primary key,
        rolename  nvarchar(100)                 not null,
        cancreate bit default CONVERT([bit], 0) not null,
        canread   bit default CONVERT([bit], 0) not null,
        canedit   bit default CONVERT([bit], 0) not null,
        candelete bit default CONVERT([bit], 0) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Roles_rolename')
BEGIN
    create unique index IX_Roles_rolename
        on Roles (rolename)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ShowTypeLookup')
BEGIN
    create table ShowTypeLookup
    (
        typeid   int identity
            constraint PK_ShowTypeLookup
                primary key,
        showtype nvarchar(100) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TicketTypes')
BEGIN
    create table TicketTypes
    (
        tickettypeid int identity
            constraint PK_TicketTypes
                primary key,
        typename     nvarchar(100) not null,
        description  nvarchar(200) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'VenueTypeLookup')
BEGIN
    create table VenueTypeLookup
    (
        typeid    int identity
            constraint PK_VenueTypeLookup
                primary key,
        venuetype nvarchar(100) not null
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Venues')
BEGIN
    create table Venues
    (
        venueid       int identity
            constraint PK_Venues
                primary key,
        venuename     nvarchar(150)                 not null,
        locationid    int                           not null
            constraint FK_Venues_Locations_locationid
                references Locations
                on delete cascade,
        typeid        int                           not null
            constraint FK_Venues_VenueTypeLookup_typeid
                references VenueTypeLookup
                on delete cascade,
        maxcapacity   int                           not null,
        seatingplanid int,
        description   nvarchar(max)                 not null,
        contactemail  nvarchar(max)                 not null,
        contactphone  nvarchar(max)                 not null,
        isaccessible  bit default CONVERT([bit], 0) not null,
        venueurl      nvarchar(max)                 not null,
        active        bit default CONVERT([bit], 1) not null,
        createdbyid   int                           not null,
        createdat     datetime2                     not null,
        updatedid     int,
        updatedat     datetime2
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Shows')
BEGIN
    create table Shows
    (
        showid             int identity
            constraint PK_Shows
                primary key,
        showname           nvarchar(150)                 not null,
        venueid            int                           not null
            constraint FK_Shows_Venues_venueid
                references Venues
                on delete cascade,
        showtypeid         int                           not null
            constraint FK_Shows_ShowTypeLookup_showtypeid
                references ShowTypeLookup
                on delete cascade,
        description        nvarchar(max)                 not null,
        agerestrictionid   int                           not null
            constraint FK_Shows_AgeRestrictionLookup_agerestrictionid
                references AgeRestrictionLookup
                on delete cascade,
        warningdescription nvarchar(max)                 not null,
        startdate          datetime2                     not null,
        enddate            datetime2                     not null,
        tickettypeid       int
            constraint FK_Shows_TicketTypes_tickettypeid
                references TicketTypes,
        imagesurl          nvarchar(max)                 not null,
        videosurl          nvarchar(max)                 not null,
        active             bit default CONVERT([bit], 1) not null,
        createdbyid        int                           not null,
        createdat          datetime2                     not null,
        updatedid          int,
        updatedat          datetime2
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Shows_agerestrictionid')
BEGIN
    create index IX_Shows_agerestrictionid
        on Shows (agerestrictionid)
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Shows_showtypeid')
BEGIN
    create index IX_Shows_showtypeid
        on Shows (showtypeid)
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Shows_tickettypeid')
BEGIN
    create index IX_Shows_tickettypeid
        on Shows (tickettypeid)
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Shows_venueid')
BEGIN
    create index IX_Shows_venueid
        on Shows (venueid)
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Venues_locationid')
BEGIN
    create index IX_Venues_locationid
        on Venues (locationid)
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Venues_typeid')
BEGIN
    create index IX_Venues_typeid
        on Venues (typeid)
END
GO

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__EFMigrationsHistory')
BEGIN
    create table __EFMigrationsHistory
    (
        MigrationId    nvarchar(150) not null
            constraint PK___EFMigrationsHistory
                primary key,
        ProductVersion nvarchar(32)  not null
    )
END
GO

-- Insert roles (already has WHERE NOT EXISTS checks)
INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
SELECT NEWID(), 'Admin', 'ADMIN', NEWID()
    WHERE NOT EXISTS (SELECT 1 FROM [AspNetRoles] WHERE [Name] = 'Admin');

INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
SELECT NEWID(), 'Manager', 'MANAGER', NEWID()
    WHERE NOT EXISTS (SELECT 1 FROM [AspNetRoles] WHERE [Name] = 'Manager');

INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
SELECT NEWID(), 'User', 'USER', NEWID()
    WHERE NOT EXISTS (SELECT 1 FROM [AspNetRoles] WHERE [Name] = 'User');

-- Insert admin user with improved checks
DECLARE @adminEmail NVARCHAR(256) = 'admin@fringe.com';
DECLARE @adminId UNIQUEIDENTIFIER;
DECLARE @passwordHash NVARCHAR(MAX) = 'AQAAAAIAAYagAAAAEFoTPa9kvFxBTs6FTLfIHMe9od9d1xIYu0eLzC8SgPRS4NKN49XZ4GiSy0xrwHWs4Q==';
DECLARE @adminRoleId UNIQUEIDENTIFIER;

-- First check if admin user already exists
IF NOT EXISTS (SELECT 1 FROM [AspNetUsers] WHERE [Email] = @adminEmail)
BEGIN
    -- Create new admin user
    SET @adminId = NEWID();
    
    INSERT INTO [AspNetUsers] (
        [Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail],
        [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp],
        [FirstName], [LastName], [CreatedAt], [IsActive],
        [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnabled], [AccessFailedCount]
    )
    VALUES (
        @adminId, @adminEmail, UPPER(@adminEmail), @adminEmail, UPPER(@adminEmail),
        1, @passwordHash, NEWID(), NEWID(),
        'Admin', 'User', GETDATE(), 1,
        0, 0, 1, 0
    );
    
    -- Get admin role ID
    SELECT @adminRoleId = [Id] FROM [AspNetRoles] WHERE [Name] = 'Admin';
    
    -- Assign admin role
    IF @adminRoleId IS NOT NULL
    BEGIN
        INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
        VALUES (@adminId, @adminRoleId);
    END
END
ELSE
BEGIN
    -- Get existing admin user ID
    SELECT @adminId = [Id] FROM [AspNetUsers] WHERE [Email] = @adminEmail;
    
    -- Get admin role ID
    SELECT @adminRoleId = [Id] FROM [AspNetRoles] WHERE [Name] = 'Admin';
    
    -- Ensure existing admin has Admin role if not already assigned
    IF NOT EXISTS (
        SELECT 1 FROM [AspNetUserRoles] 
        WHERE [UserId] = @adminId AND [RoleId] = @adminRoleId
    ) AND @adminId IS NOT NULL AND @adminRoleId IS NOT NULL
    BEGIN
        INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
        VALUES (@adminId, @adminRoleId);
    END
END