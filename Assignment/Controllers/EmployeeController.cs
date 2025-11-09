using Assignment.Models;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDbContext dbContext;
        public EmployeeController(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = dbContext.EmployeeMasters.ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeMaster employee)
        {
            if (ModelState.IsValid)
            {
                var existemployee = dbContext.EmployeeMasters.Any(e => e.EmployeeCode == employee.EmployeeCode);
                if(existemployee)
                {
                    ModelState.AddModelError("EmployeeCode", "Employee already exists with this Employee Code.");
                    return View(employee);
                }
                dbContext.EmployeeMasters.Add(employee);
                dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Employee inserted successfully!";
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = dbContext.EmployeeMasters.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeMaster employee)
        {
            if (ModelState.IsValid)
            {
                dbContext.EmployeeMasters.Update(employee);
                dbContext.SaveChanges();
                TempData["SuccessMessage"] = "Employee updated successfully!";
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = dbContext.EmployeeMasters.FirstOrDefault(e => e.EmployeeId == id);
            return View(employee);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = dbContext.EmployeeMasters.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = dbContext.EmployeeMasters.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Status = false;
            dbContext.EmployeeMasters.Update(employee);
            dbContext.SaveChanges();
            TempData["SuccessMessage"] = "Employee deleted successfully!";
            return RedirectToAction("Index");
        }
        public IActionResult ExportToExcel()
        {
            var employees = dbContext.EmployeeMasters.ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("EmployeeList");

                worksheet.Cell(1, 1).Value = "Employee Code";
                worksheet.Cell(1, 2).Value = "Employee Name";
                worksheet.Cell(1, 3).Value = "Department";
                worksheet.Cell(1, 4).Value = "Designation";
                worksheet.Cell(1, 5).Value = "Joining Date";
                worksheet.Cell(1, 6).Value = "Salary";
                worksheet.Cell(1, 7).Value = "Status";

                int row = 2;
                foreach (var emp in employees)
                {
                    worksheet.Cell(row, 1).Value = emp.EmployeeCode;
                    worksheet.Cell(row, 2).Value = emp.EmployeeName;
                    worksheet.Cell(row, 3).Value = emp.Department;
                    worksheet.Cell(row, 4).Value = emp.Designation;
                    worksheet.Cell(row, 5).Value = emp.JoiningDate?.ToString("dd-MM-yyyy");
                    worksheet.Cell(row, 6).Value = emp.Salary;
                    worksheet.Cell(row, 7).Value = (bool)emp.Status ? "Active" : "Inactive";
                    row++;
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "EmployeeList.xlsx"
                    );
                }
            }
        }

    }
}