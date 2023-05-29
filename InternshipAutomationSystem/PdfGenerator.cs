using Internship.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Hosting;

namespace InternshipAutomationSystem
{
    public class PdfGenerator
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public PdfGenerator(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public void GeneratePdf(ApplicationForm applicationForm)
        {

            // Load the HTML template
            string templatePath = Path.Combine(webHostEnvironment.WebRootPath, "HtmlUtility", "ApplicationForm.html");
            string htmlContent = File.ReadAllText(templatePath);

            // Replace placeholders in the HTML template with actual data from the application form
            htmlContent = htmlContent.Replace("{{TC}}", applicationForm.TC);
            htmlContent = htmlContent.Replace("{{Name}}", applicationForm.Name);
            htmlContent = htmlContent.Replace("{{SID}}", applicationForm.SID);
            htmlContent = htmlContent.Replace("{{PhoneNumber}}", applicationForm.PhoneNumber);
            htmlContent = htmlContent.Replace("{{Faculty}}", applicationForm.Faculty);
            htmlContent = htmlContent.Replace("{{Class}}", applicationForm.Class);
            htmlContent = htmlContent.Replace("{{Department}}", applicationForm.Department);
            htmlContent = htmlContent.Replace("{{StartDate}}", applicationForm.StartDate.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{{EndDate}}", applicationForm.EndDate.ToString("yyyy-MM-dd"));
            htmlContent = htmlContent.Replace("{{NumberOfDays}}", applicationForm.NumberOfDays.ToString());
            htmlContent = htmlContent.Replace("{{Type}}", applicationForm.Type);
            htmlContent = htmlContent.Replace("{{DependendParent}}", applicationForm.DependendParent.ToString());
            htmlContent = htmlContent.Replace("{{InstitutionName}}", applicationForm.InstitutionName);
            htmlContent = htmlContent.Replace("{{InstitutionAddress}}", applicationForm.InstitutionAddress);
            htmlContent = htmlContent.Replace("{{InstitutionPerson}}", applicationForm.InstitutionPerson);
            htmlContent = htmlContent.Replace("{{InstitutionPhoneNumber}}", applicationForm.InstitutionPhoneNumber);
            // Replace other placeholders...

            // Create the PDF document
            Document document = new Document();

            // Create a PDF writer instance
            string uniqueId = Guid.NewGuid().ToString();
            string outputPath = Path.Combine(webHostEnvironment.WebRootPath, "PDF", "hello.pdf");
         
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));

            // Open the PDF document
            document.Open();

            // Parse the HTML content and generate PDF
            using (var sr = new StringReader(htmlContent))
            {
                // Set up the PDF parser
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
            }

            // Close the PDF document
            document.Close();
        }
        public void GeneratePdfOfficialLetter(OfficialLetter officialLetter)
        {

            // Load the HTML template
            string templatePath = Path.Combine(webHostEnvironment.WebRootPath, "HtmlUtility", "OfficialLetterT.html");
            string htmlContent = File.ReadAllText(templatePath);
            
            // Replace placeholders in the HTML template with actual data from the application form
            htmlContent = htmlContent.Replace("{{CompanyName}}", officialLetter.CompanyName);
            htmlContent = htmlContent.Replace("{{Department}}", officialLetter.Department);
            htmlContent = htmlContent.Replace("{{ReceiverName}}", officialLetter.ReceiverName);
            htmlContent = htmlContent.Replace("{{NumOfInternships}}", officialLetter.NumOfInternships.ToString());
            htmlContent = htmlContent.Replace("{{InternshipCoordinator}}", officialLetter.InternshipCoordinator.Name);
            htmlContent = htmlContent.Replace("{{DatePosted}}", officialLetter.DatePosted.ToString());
            // Replace other placeholders...

            // Create the PDF document
            Document document = new Document();

            // Create a PDF writer instance
            string uniqueId = Guid.NewGuid().ToString();
            string outputPath = Path.Combine(webHostEnvironment.WebRootPath, "PDF", "OfficialLetterT.pdf");

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));

            // Open the PDF document
            document.Open();

            // Parse the HTML content and generate PDF
            using (var sr = new StringReader(htmlContent))
            {
                // Set up the PDF parser
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
            }

            // Close the PDF document
            document.Close();
        }

    }
}
