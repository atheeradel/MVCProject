using System.IO;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;



public class PdfController : Controller
{
    private readonly ModelContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;

    public PdfController(ModelContext context, IWebHostEnvironment hostingEnvironment)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }

    public IActionResult GenerateAndStorePdf(int id)
    {
        // Retrieve the record from the database
        var record = _context.Recipes.Where(x=>x.RecId==id).SingleOrDefault();

        if (record == null)
        {
            return NotFound(); // Handle the case where the record is not found
        }

        // Generate PDF
        using (MemoryStream memoryStream = new MemoryStream())
        {
            Document doc = new Document();
            Font font;
            PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();
            doc.SetPageSize(PageSize.A4);
            doc.SetMargins(10f, 10f, 25f, 10f);
            
            Paragraph title= new Paragraph("Master Chef Recipe");
            font = FontFactory.GetFont("Tahoma", 8f, 1);
            string path = _hostingEnvironment.WebRootPath + "/Home/images";
            string path2 = _hostingEnvironment.WebRootPath + "/Images";

            string imgcombine = Path.Combine(path, "Recipe-removebg-preview.png");
            string imgcombine2 = Path.Combine(path2, $"{record.Image}");
            Image img=Image.GetInstance(imgcombine);
            Image img2=Image.GetInstance(imgcombine2);  
            img.ScaleToFit(300f, img.Height);
           
            img.BorderColor = BaseColor.Black;
            img.BorderWidth = 2f; 
            img2.ScaleToFit(200f, img.Height);
            img2.BorderWidth = 1f;
            doc.Add(img);

            // Add content to PDF
            doc.Add(new Paragraph($"Welcome To Master Chef our customer  Thank you for your trust in our recipes 🙏"));
            doc.Add(new Paragraph($"   "));
            doc.Add(new Paragraph($"Your Recipe Name 🍽️: {record.Name}"));
            doc.Add(new Paragraph($"   "));
            doc.Add(new Paragraph($"Your Recipe Ingredients: {record.Ingrediants}"));
            doc.Add(new Paragraph($"   "));
            doc.Add(new Paragraph($"Your Recipe Instructions: {record.Instruction}"));
            doc.Add(img2 );
            doc.Add(new Paragraph($"   "));
            doc.Add(new Paragraph($" Dont Forget to share us your opinon for our recipes 😉  "));
            // Add other properties as needed

            doc.Close();

            // Store PDF in wwwroot
            string wwwRootPath = _hostingEnvironment.WebRootPath;
            string pdfPath = Path.Combine(wwwRootPath, "pdfs", "generated.pdf");

            using (var fileStream = new FileStream(pdfPath, FileMode.Create))
            {
                memoryStream.WriteTo(fileStream);
            }
        }

        return RedirectToAction("email_send(1)");
    }
    public void email_send()

    { var record = _context.Userinfos.Where(x => x.UserId == 8).SingleOrDefault();
        var record2 = _context.Recipes.Where(x => x.RecId == 1).SingleOrDefault();
        MailMessage mail=new MailMessage(); 
        SmtpClient SmtpServer=new SmtpClient("smtp.ethereal.email");
        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential("liliane79@ethereal.email", "U9Qmf8sPcwKFhHXfVP");
        SmtpServer.EnableSsl = true;
        //MailAddress from = new MailAddress("MasterChef@gamil.com");
        mail.From = new MailAddress("liliane79@ethereal.email");
        mail.To.Add($"{record.Email}");
        mail.Body = $"Thank You {record.Firstname} {record.Lastname} for your Purchase  your invoice is ${record2.Price} and your recipe is by this attached Pdf File"; ;
        Attachment attachment = new Attachment("D:\\dirsttask\\MVCProject - Copy\\MVCProject\\wwwroot\\pdfs\\generated.pdf");
        mail.Attachments.Add(attachment);
        SmtpServer.Send(mail);
    }





       
    }

