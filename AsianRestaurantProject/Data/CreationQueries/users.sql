USE UserData;
CREATE TABLE Users
(
id INT PRIMARY KEY NOT NULL,
forename varchar(20),
lastname varchar(20),
gmail varchar(254),
password varbinary(256)
)