SELECT forename,lastname FROM
 (SELECT id FROM authentications WHERE id = @username AND authRng = @authRng) AS authMatches INNER JOIN users ON authMatches.id = users.id


