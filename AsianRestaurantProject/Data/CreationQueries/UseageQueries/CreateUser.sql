USE UserData 
INSERT INTO users (users.gmail,users.password,users.forename,users.lastname,users.salt) VALUES (@email,CONVERT(VARBINARY(512),@password),@Name,@lastName,CONVERT(VARBINARY(256),@salt))