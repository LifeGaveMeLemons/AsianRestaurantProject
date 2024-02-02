USE UserData;
CREATE TABLE authentications
(
 id INT FOREIGN KEY REFERENCES Users(id),
 authRng INT,
 ipAddress VARCHAR(39) NOT NULL,
 expDate VARCHAR(33) NOT NULL,
 PRIMARY KEY(id,authRng)
)
