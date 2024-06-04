using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploKor.Model
{
    public class Employee
    {
        [Key]
        public int employeeId { get; set; }
        public string employeeSurname { get; set; }
        public string employeeName { get; set; }
        public string? employeePatronymic { get; set; }
        public int employeeSeriesPassport { get; set; }
        public int employeeNumberPassport { get; set; }
        public string employeeEducation { get; set; }
        public string? employeeChildrenBirthCertificateNumber { get; set; }
        public string employeeNumberWorkBook { get; set; }
        public string employeeSeriesWorkBook { get; set; }
        public string employeeEducationNumber { get; set; }
        public string employeeEducationSeries {  get; set; }
        public string employeeNumberOfMilitaryId { get; set; }
        public string employeeSeriesOfMilitaryId { get; set; }
        public Employee() { }
        public Employee(int employeeId, string employeeSurname, string employeeName, string? employeePatronymic, int employeeSeriesPassport, int employeeNumberPassport, string employeeEducation, string? employeeChildrenBirthCertificateNumber, string employeeNumberWorkBook, string employeeSeriesWorkBook, string employeeEducationNumber, string employeeEducationSeries, string employeeNumberOfMilitaryId, string employeeSeriesOfMilitaryId)
        {
            this.employeeId = employeeId;
            this.employeeSurname = employeeSurname;
            this.employeeName = employeeName;
            this.employeePatronymic = employeePatronymic;
            this.employeeSeriesPassport = employeeSeriesPassport;
            this.employeeNumberPassport = employeeNumberPassport;
            this.employeeEducation = employeeEducation;
            this.employeeChildrenBirthCertificateNumber = employeeChildrenBirthCertificateNumber;
            this.employeeNumberWorkBook = employeeNumberWorkBook;
            this.employeeSeriesWorkBook = employeeSeriesWorkBook;
            this.employeeEducationNumber = employeeEducationNumber;
            this.employeeEducationSeries = employeeEducationSeries;
            this.employeeNumberOfMilitaryId = employeeNumberOfMilitaryId;
            this.employeeSeriesOfMilitaryId = employeeSeriesOfMilitaryId;
        }
    }
}
