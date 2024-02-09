USE UserData;
CREATE TABLE users
(
gmail varchar(254) PRIMARY KEY NOT NULL,
forename varchar(20),
lastname varchar(20),
password varbinary(512),
salt varbinary(256)
)