/* Ex2_B_SQL */
SELECT e.Id, e.Name, SUM(DATEDIFF(DAY, v.DateSince, v.DateUntil) + 1) AS UsedDays
FROM Employee e
JOIN Vacations v ON e.Id  = v.EmployeeId
WHERE YEAR(v.DateSince) = YEAR(GETDATE()) AND v.DateUntil < GETDATE() AND v.IsPartialVacation = 0
GROUP BY e.Id, e.Name;
