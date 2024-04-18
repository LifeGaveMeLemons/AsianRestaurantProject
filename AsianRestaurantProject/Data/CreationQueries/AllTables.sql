
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.users') AND type = N'U')
BEGIN
    CREATE TABLE dbo.users (
        gmail VARCHAR(254) PRIMARY KEY NOT NULL,
        forename VARCHAR(20),
        lastname VARCHAR(20),
        password VARBINARY(512),
        salt VARBINARY(256)
    )
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.authentications') AND type = N'U')
BEGIN
    CREATE TABLE dbo.authentications (
        id VARCHAR(254) FOREIGN KEY REFERENCES users(gmail),
        authRng INT,
        ipAddress VARCHAR(39) NOT NULL,
        expDate VARCHAR(33) NOT NULL,
        PRIMARY KEY (id, authRng)
    )
END
GO

-- Create the 'OngoingEmailVerifications' table if it does not exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.OngoingEmailVerifications') AND type = N'U')
BEGIN
    CREATE TABLE dbo.OngoingEmailVerifications (
        Email VARCHAR(256) PRIMARY KEY,
        AuthRNG VARBINARY(256),
        ExpTime VARBINARY(64),
        CryptoKey VARBINARY(512)
    )
END
GO
