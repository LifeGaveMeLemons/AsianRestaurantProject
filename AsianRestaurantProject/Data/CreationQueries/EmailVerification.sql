﻿USE UserData
CREATE TABLE OngoingEmailVerifications
(
	email VARCHAR(256) PRIMARY KEY,
	AuthRNG VARBINARY(256),
	ExpTime VARBINARY(64),
	CryptoKey VARBINARY(512)
)