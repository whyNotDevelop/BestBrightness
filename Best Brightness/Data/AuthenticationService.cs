using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private Employee loggedInEmployee;
        private Admin loggedInAdmin;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            // Authenticate using employee ID and password
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && e.Password == password);

            if (employee != null)
            {
                loggedInEmployee = employee;
                loggedInAdmin = null; // Reset loggedInAdmin
            }

            return employee;
        }

        public async Task<Admin> AuthenticateAdmin(int id, string password)
        {
            // Authenticate using admin ID and password
            var admin = await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == id && a.Password == password);

            if (admin != null)
            {
                loggedInAdmin = admin;
                loggedInEmployee = null; // Reset loggedInEmployee
            }

            return admin;
        }

        public Task<Employee> GetLoggedInEmployee()
        {
            return Task.FromResult(loggedInEmployee);
        }

        public Task<Admin> GetLoggedInAdmin()
        {
            return Task.FromResult(loggedInAdmin);
        }
    }
}



/*using BestBrightness.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly ApplicationDbContext _dbContext;
    private Employee loggedInEmployee;

    public AuthenticationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Admin> AuthenticateAdmin(int id, string password)
    {
        var admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == id && a.Password == password);
        return admin;
    }

    public async Task<Employee> AuthenticateEmployee(int id, string password)
    {
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
        if (employee != null)
        {
            loggedInEmployee = employee;
        }
        return employee;
    }

    public async Task<Employee> GetLoggedInEmployee()
    {
        return loggedInEmployee;
    }

    public async Task Logout()
    {
        loggedInEmployee = null;
    }
}



/*using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _authenticationStateProvider = (CustomAuthenticationStateProvider)authenticationStateProvider;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
            if (employee != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                    new Claim(ClaimTypes.Name, employee.Name),
                    new Claim(ClaimTypes.Role, employee.Role)
                };
                var identity = new ClaimsIdentity(claims, "apiauth_type");
                var user = new ClaimsPrincipal(identity);

                _authenticationStateProvider.MarkUserAsAuthenticated(user);
            }
            return employee;
        }

        public async Task<Employee> AuthenticateAdmin(int id, string password)
        {
            var admin = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id && a.Password == password && a.Role == "Store Manager/Admin");
            if (admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.Name, admin.Name),
                    new Claim(ClaimTypes.Role, admin.Role)
                };
                var identity = new ClaimsIdentity(claims, "apiauth_type");
                var user = new ClaimsPrincipal(identity);

                _authenticationStateProvider.MarkUserAsAuthenticated(user);
            }
            return admin;
        }

        public async Task<Employee> GetLoggedInEmployee()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int intUserId))
            {
                return await _context.Employees.FirstOrDefaultAsync(e => e.Id == intUserId);
            }
            return null;
        }
    }
}


/*using BestBrightness.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, AuthenticationStateProvider authenticationStateProvider)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
            if (employee != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                    new Claim(ClaimTypes.Name, employee.Name),
                    new Claim(ClaimTypes.Role, employee.Role)
                };
                var identity = new ClaimsIdentity(claims, "apiauth_type");
                var user = new ClaimsPrincipal(identity);

                ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user);
            }
            return employee;
        }

        public async Task<Employee> AuthenticateAdmin(int id, string password)
        {
            var admin = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id && a.Password == password && a.Role == "Store Manager/Admin");
            if (admin != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                    new Claim(ClaimTypes.Name, admin.Name),
                    new Claim(ClaimTypes.Role, admin.Role)
                };
                var identity = new ClaimsIdentity(claims, "apiauth_type");
                var user = new ClaimsPrincipal(identity);

                ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(user);
            }
            return admin;
        }

        public async Task<Employee> GetLoggedInEmployee()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int intUserId))
            {
                return await _context.Employees.FirstOrDefaultAsync(e => e.Id == intUserId);
            }
            return null;
        }
    }
}


/*using BestBrightness.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
            return employee;
        }

        public async Task<Employee> AuthenticateAdmin(int id, string password)
        {
            var admin = await _context.Employees.FirstOrDefaultAsync(a => a.Id == id && a.Password == password && a.Role == "Store Manager/Admin");
            return admin;
        }

        public async Task<Employee> GetLoggedInEmployee()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int intUserId))
            {
                return await _context.Employees.FirstOrDefaultAsync(e => e.Id == intUserId);
            }
            return null;
        }
    }
}


/*using BestBrightness.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> AuthenticateEmployee(string username, string password)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Name == username && e.Password == password);
            return employee;
        }

        public async Task<Admin> AuthenticateAdmin(string username, string password)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Name == username && a.Password == password);
            return admin;
        }

        public async Task<Employee> GetLoggedInEmployee()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id.ToString() == userId);
        }
    }
}

/*using BestBrightness.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> AuthenticateAdmin(string username, string password)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Role == "Admin" && e.Name == username && e.Password == password);
        }

        public async Task<Employee> AuthenticateEmployee(string username, string password)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Name == username && e.Password == password);
        }

        public async Task<Employee> GetLoggedInEmployee()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                if (int.TryParse(userId, out int id))
                {
                    return await _dbContext.Employees.FindAsync(id);
                }
            }
            return null;
        }

        public async Task<bool> RegisterEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task LogActivity(int employeeId, string activityType)
        {
            var activity = new Activity
            {
                EmployeeId = employeeId,
                ActivityType = activityType,
                Timestamp = DateTime.Now
            };
            _dbContext.Activities.Add(activity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateEmployeePassword(int employeeId, string newPassword)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                employee.Password = newPassword;
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}


/*using BestBrightness.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> AuthenticateAdmin(string username, string password)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Role == "Admin" && e.Name == username && e.Password == password);
        }

        public async Task<Employee> AuthenticateEmployee(string username, string password)
        {
            return await _dbContext.Employees
                .FirstOrDefaultAsync(e => e.Name == username && e.Password == password);
        }

        public async Task<Employee> GetLoggedInEmployee()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                if (int.TryParse(userId, out int id))
                {
                    return await _dbContext.Employees.FindAsync(id);
                }
            }
            return null;
        }

        public async Task<bool> RegisterEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteEmployee(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }

        public async Task LogActivity(int employeeId, string activityType)
        {
            var activity = new Activity
            {
                EmployeeId = employeeId,
                ActivityType = activityType,
                Timestamp = DateTime.Now,
            };
            _dbContext.Activities.Add(activity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateEmployeePassword(int employeeId, string newPassword)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                employee.Password = newPassword;
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            return false;
        }
    }
}



/*using BestBrightness.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly ApplicationDbContext _dbContext;
    private Employee loggedInEmployee;

    public AuthenticationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Admin> AuthenticateAdmin(int id, string password)
    {
        var admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == id && a.Password == password);
        return admin;
    }

    public async Task<Employee> AuthenticateEmployee(int id, string password)
    {
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
        if (employee != null)
        {
            loggedInEmployee = employee;
        }
        return employee;
    }

    public async Task<Employee> GetLoggedInEmployee()
    {
        return loggedInEmployee;
    }

    public async Task TrackActivity(int employeeId, DateTime loginTime, DateTime? logoutTime = null)
    {
        var activity = new Activity
        {
            EmployeeId = employeeId,
            LoginDateTime = loginTime,
            LogoutDateTime = logoutTime
        };

        _dbContext.Activities.Add(activity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Logout()
    {
        if (loggedInEmployee != null)
        {
            var activity = await _dbContext.Activities
                .Where(a => a.EmployeeId == loggedInEmployee.Id && a.LogoutDateTime == null)
                .OrderByDescending(a => a.LoginDateTime)
                .FirstOrDefaultAsync();

            if (activity != null)
            {
                activity.LogoutDateTime = DateTime.Now;
                await _dbContext.SaveChangesAsync();
            }
        }

        loggedInEmployee = null;
    }
}

/*using BestBrightness.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly ApplicationDbContext _dbContext;
    private Employee loggedInEmployee;

    public AuthenticationService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Admin> AuthenticateAdmin(int id, string password)
    {
        var admin = await _dbContext.Admins.FirstOrDefaultAsync(a => a.Id == id && a.Password == password);
        return admin;
    }

    public async Task<Employee> AuthenticateEmployee(int id, string password)
    {
        var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
        if (employee != null)
        {
            loggedInEmployee = employee;
        }
        return employee;
    }

    public async Task<Employee> GetLoggedInEmployee()
    {
        return loggedInEmployee;
    }
}


/*using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ApplicationDbContext context, ILogger<AuthenticationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError("Password is null or empty.");
                return null;
            }

            try
            {
                var loggedInEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id && e.Password == password);

                if (loggedInEmployee == null)
                {
                    _logger.LogWarning("No employee found with id {Id} and provided password.", id);
                }

                return loggedInEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating employee with id {Id}", id);
                return null;
            }
        }

        public async Task<Admin> AuthenticateAdmin(int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError("Password is null or empty.");
                return null;
            }

            try
            {
                var loggedInAdmin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Id == id && a.Password == password);

                if (loggedInAdmin == null)
                {
                    _logger.LogWarning("No admin found with id {Id} and provided password.", id);
                }

                return loggedInAdmin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating admin with id {Id}", id);
                return null;
            }
        }

        // This method returns the logged-in employee
        public async Task<Employee> GetLoggedInEmployee()
        {
            // Replace this with your logic for retrieving the logged-in employee, e.g., from session or state
            int loggedInEmployeeId = 1; // Example, replace with actual value
            string loggedInEmployeePassword = "password"; // Example, replace with actual value
            return await AuthenticateEmployee(loggedInEmployeeId, loggedInEmployeePassword);
        }
    }
}


/*using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ApplicationDbContext context, ILogger<AuthenticationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError("Password is null or empty.");
                return null;
            }

            try
            {
                var loggedInEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id && e.Password == password);

                if (loggedInEmployee == null)
                {
                    _logger.LogWarning("No employee found with id {Id} and provided password.", id);
                }

                return loggedInEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating employee with id {Id}", id);
                return null;
            }
        }

        public async Task<Admin> AuthenticateAdmin(int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError("Password is null or empty.");
                return null;
            }

            try
            {
                var loggedInAdmin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Id == id && a.Password == password);

                if (loggedInAdmin == null)
                {
                    _logger.LogWarning("No admin found with id {Id} and provided password.", id);
                }

                return loggedInAdmin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating admin with id {Id}", id);
                return null;
            }
        }
    }
}


/*using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public AuthenticationService(ApplicationDbContext context, ILogger<AuthenticationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError("Password is null or empty.");
                return null;
            }

            try
            {
                var loggedInEmployee = await _context.Employees
                    .FirstOrDefaultAsync(e => e.Id == id && e.Password == password);

                if (loggedInEmployee == null)
                {
                    _logger.LogWarning("No employee found with id {Id} and provided password.", id);
                }

                return loggedInEmployee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating employee with id {Id}", id);
                return null;
            }
        }

        public async Task<Admin> AuthenticateAdmin(int id, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                _logger.LogError("Password is null or empty.");
                return null;
            }

            try
            {
                var loggedInAdmin = await _context.Admins
                    .FirstOrDefaultAsync(a => a.Id == id && a.Password == password);

                if (loggedInAdmin == null)
                {
                    _logger.LogWarning("No admin found with id {Id} and provided password.", id);
                }

                return loggedInAdmin;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while authenticating admin with id {Id}", id);
                return null;
            }
        }
    }
}


/*using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            // Authenticate using employee ID and password
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
        }

        public async Task<Admin> AuthenticateAdmin(int id, string password)
        {
            // Authenticate using admin ID and password
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == id && a.Password == password);
        }

        public async Task<Employee> GetLoggedInEmployee()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id.ToString() == userId);
        }
    }
}


/*using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> AuthenticateEmployee(int id, string password)
        {
            // Authenticate using employee ID and password
            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && e.Password == password);
        }

        public async Task<Admin> AuthenticateAdmin(int id, string password)
        {
            // Authenticate using admin ID and password
            return await _context.Admins
                .FirstOrDefaultAsync(a => a.Id == id && a.Password == password);
        }
    }
}

/*using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BestBrightness.Data
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Authenticate(string username, string password)
        {
            // Simulate authentication logic
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Name == username && e.Password == password);

            return employee;
        }
    }
}


*/