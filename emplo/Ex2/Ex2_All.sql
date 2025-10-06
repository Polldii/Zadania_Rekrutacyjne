/* Ex2_A_SQL */
SELECT DISTINCT e.Id, e.Name FROM Employee e
JOIN Team t ON e.TeamId = t.Id
JOIN Vacations v ON e.Id = v.EmployeeId
WHERE t.Name = '.NET' AND YEAR(v.DateSince) = 2019;


------------------------------------------------------------------------------------------------------
/* Ex2_B_SQL */
SELECT e.Id, e.Name, SUM(DATEDIFF(DAY, v.DateSince, v.DateUntil) + 1) AS UsedDays
FROM Employee e
JOIN Vacations v ON e.Id  = v.EmployeeId
WHERE YEAR(v.DateSince) = YEAR(GETDATE()) AND v.DateUntil < GETDATE() AND v.IsPartialVacation = 0
GROUP BY e.Id, e.Name;

------------------------------------------------------------------------------------------------------
/* Ex2_C_SQL */
SELECT t.* FROM Team t
WHERE NOT EXISTS (SELECT 1 FROM Employee e
                    JOIN Vacations v ON e.Id = v.EmployeeId
                    WHERE e.TeamId = t.Id AND YEAR(v.DateSince) = 2019);

------------------------------------------------------------------------------------------------------