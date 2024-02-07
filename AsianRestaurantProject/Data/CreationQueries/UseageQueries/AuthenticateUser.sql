IF (SELECT @username FROM users) != NULL
INSERT INTO authentications (username, authRng, expDate, ipAddress) VALUES (@username,@randNumber, @expirationDate,@ipAddress)