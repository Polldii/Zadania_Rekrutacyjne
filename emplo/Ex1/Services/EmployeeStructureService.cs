using Ex1.Models;

namespace Ex1.Services
{
    /* 
     * Service to manage employee structures and their hierarchies
     */
    public class EmployeeStructureService : IEmployeeStructureService
    {
        /*
         * Dictionary to hold the employee structures for quick access
         */
        private Dictionary<int, List<EmployeeStructure>> _employeeStructureDictionary = new ();

        /* 
         * Method to fill the employee structure list based on the provided employees
         */
        public List<EmployeeStructure> FillEmployeesStructure(List<Employee> employees)
       {
            /* 
             * List to hold the employee structures
             */
            var employeeStructuresList = new List<EmployeeStructure>();
            /* 
             * Create a dictionary for quick access to employees by their Id
             */
            var employeeDictionary = employees.ToDictionary(e => e.Id);

            /* 
             * Set Superior references
             */
            foreach (var employee in employees)
            {
                /* 
                 * Set the Superior property using the dictionary for quick access
                 */
                employee.Superior = employee.SuperiorId.HasValue? employeeDictionary.GetValueOrDefault(employee.SuperiorId.Value) : null;
            }

            /* 
             * Go through each employee and build the hierarchy
             */
            foreach (var employee in employees)
           {
                /*
                 * HashSet to keep track of visited employees to avoid cycles
                 */
                var visited = new HashSet<int>();

                /*
                 * Start the recursion if the employee has a superior
                 */
                if (employee.Superior != null)
                {
                    /* 
                     * Add the employee to visited to avoid cycles
                     */
                    GoingThroughEmployeesHierarchy(employee.Id, employee.Superior, employeeStructuresList, 1, visited);
                }
           }

            /* 
             * Create a dictionary for quick access
             */
            _employeeStructureDictionary = employeeStructuresList
                                         .GroupBy(r => r.EmployeeId)
                                         .ToDictionary(g => g.Key, g => g.ToList());

            /* 
             * Return the complete list of employee structures
             */
            return employeeStructuresList;
       }
        /* 
         * Recursive method to go through the hierarchy of superiors
         * Implementation of the Depth-First Search [DFS] algorithm
         */
        private void GoingThroughEmployeesHierarchy(int employeeId, Employee superior, List<EmployeeStructure> employeeStructuresList, int row, HashSet<int> visited)
       {
            /*
             * Base case: if there is no superior or if this superior has already been visited
             */
            if (superior == null || visited.Contains(superior.Id))
                return;

            /* 
             * Add the current superior to the list with the corresponding row
             */
            employeeStructuresList.Add(new EmployeeStructure
            {
                EmployeeId = employeeId,
                SuperiorId = superior.Id,
                Row = row
            });

            /* 
             * Mark this superior as visited to avoid cycles
             */
            visited.Add(superior.Id);

            /* 
             * Recursive call for the next superior in the hierarchy
             */
            GoingThroughEmployeesHierarchy(employeeId, superior.Superior, employeeStructuresList, row + 1, visited);

        }

        /* 
         * Method to get the row of a specific superior for a specific employee
         */
        public int? GetSuperiorRowOfEmployee(int employeeId, int superiorId)
       {
            /*
             * Check if the employeeId exists in the dictionary
             */
            if (_employeeStructureDictionary.TryGetValue(employeeId, out var relations))
            {
                /*
                 * Find the relation with the specified superiorId
                 */
                var relation = relations.FirstOrDefault(r => r.SuperiorId == superiorId);

                /*
                 * Return the row if found, otherwise return null
                 */
                return relation?.Row;
            }

            /* 
             * If the employeeId does not exist, return null as row
             */
            return null;
        }

    }
}
