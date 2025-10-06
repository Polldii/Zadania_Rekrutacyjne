/* Ex2_A_SQL */
SELECT DISTINCT e.Id, e.Name FROM Employee e
JOIN Team t ON e.TeamId = t.Id
JOIN Vacations v ON e.Id = v.EmployeeId
WHERE t.Name = '.NET' AND YEAR(v.DateSince) = 2019;
