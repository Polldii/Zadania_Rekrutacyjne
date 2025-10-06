/* Ex2_C_SQL */
SELECT t.* FROM Team t
WHERE NOT EXISTS (SELECT 1 FROM Employee e
                    JOIN Vacations v ON e.Id = v.EmployeeId
                    WHERE e.TeamId = t.Id AND YEAR(v.DateSince) = 2019);
