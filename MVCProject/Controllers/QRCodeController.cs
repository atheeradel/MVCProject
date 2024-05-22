using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.Drawing;
using System.IO;
using MVCProject.Models;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using System.Net.Mail;
using System.Net;

namespace MVCProject.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;


        public QRCodeController(IWebHostEnvironment webHostEnvironment)
        {

            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult GenerateQRCode(string Email)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Create a new document
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Add content to the PDF
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
                var sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                var bodyFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                // Title
                var title = new Paragraph("MasterChef Recipe Platform: Join the Culinary Revolution", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);
                

                document.Add(new Paragraph("\nWelcome to the MasterChef Recipe Platform, the ultimate destination for culinary enthusiasts and professional chefs. Our platform offers a range of exclusive services designed to enhance your cooking experience and foster a vibrant culinary community. Here’s what we offer to entice both users and chefs to join us:", bodyFont));

                // For Users Section
                var usersSection = new Paragraph("\nFor Users: Unleash Your Inner Chef", sectionFont);
                document.Add(usersSection);
                document.Add(new Paragraph("\n1. Exclusive Recipe Access", bodyFont));
                document.Add(new Paragraph("• Curated Collections: Explore a wide variety of recipes categorized by type, cuisine, and difficulty level.", bodyFont));
                document.Add(new Paragraph("• Chef Specials: Gain access to exclusive recipes crafted by top chefs.", bodyFont));

                document.Add(new Paragraph("\n2. Coupon Codes and Discounts", bodyFont));
                document.Add(new Paragraph("• Welcome Discount: Get a 20% discount on your first recipe purchase with the code WELCOME20.", bodyFont));
                document.Add(new Paragraph("• Seasonal Offers: Enjoy special discounts during holidays and seasonal promotions.", bodyFont));

                document.Add(new Paragraph("\n3. User-Friendly Features", bodyFont));
                document.Add(new Paragraph("• Personal Recipe Box: Save your favorite recipes in your personal collection for easy access anytime.", bodyFont));
                document.Add(new Paragraph("• Ratings and Reviews: Share your feedback on recipes and read reviews from other users.", bodyFont));

                document.Add(new Paragraph("\n4. Seamless Purchase and Delivery", bodyFont));
                document.Add(new Paragraph("• Instant Access: Purchased recipes are delivered directly to your email.", bodyFont));
                document.Add(new Paragraph("• Secure Payments: Our platform supports secure transactions through various payment methods.", bodyFont));

                document.Add(new Paragraph("\n5. Stay Informed", bodyFont));
                document.Add(new Paragraph("• Email Notifications: Receive updates on new recipes, special offers, and cooking tips.", bodyFont));
                document.Add(new Paragraph("• Community Events: Participate in cooking challenges and virtual cooking classes hosted by renowned chefs.", bodyFont));

                // For Chefs Section
                var chefsSection = new Paragraph("\nFor Chefs: Showcase Your Culinary Mastery", sectionFont);
                document.Add(chefsSection);
                document.Add(new Paragraph("\n1. Expand Your Reach", bodyFont));
                document.Add(new Paragraph("• Global Audience: Share your culinary creations with food enthusiasts worldwide.", bodyFont));
                document.Add(new Paragraph("• Profile Customization: Create a detailed profile showcasing your skills, experience, and culinary philosophy.", bodyFont));

                document.Add(new Paragraph("\n2. Monetize Your Recipes", bodyFont));
                document.Add(new Paragraph("• Revenue Sharing: Earn a share of the revenue from each recipe purchased by users.", bodyFont));
                document.Add(new Paragraph("• Exclusive Deals: Offer special promotions and coupon codes to attract more followers.", bodyFont));

                document.Add(new Paragraph("\n3. Enhanced Recipe Management", bodyFont));
                document.Add(new Paragraph("• Easy Uploads: Quickly upload and categorize your recipes.", bodyFont));
                document.Add(new Paragraph("• Recipe Analytics: Track the performance of your recipes with detailed analytics.", bodyFont));

                document.Add(new Paragraph("\n4. Engagement and Feedback", bodyFont));
                document.Add(new Paragraph("• User Interaction: Engage with users through comments and feedback on your recipes.", bodyFont));
                document.Add(new Paragraph("• Community Building: Build a loyal following and establish your brand within our community.", bodyFont));

                document.Add(new Paragraph("\n5. Professional Growth", bodyFont));
                document.Add(new Paragraph("• Workshops and Webinars: Participate in professional development sessions and webinars.", bodyFont));
                document.Add(new Paragraph("• Networking Opportunities: Connect with other chefs and culinary experts.", bodyFont));

                // Join Us Today Section
                var joinSection = new Paragraph("\nJoin Us Today", sectionFont);
                document.Add(joinSection);
                document.Add(new Paragraph("\nFor Users:", bodyFont));
                document.Add(new Paragraph("Register now and use the code WELCOME20 to get 20% off your first recipe purchase. Dive into a world of culinary delights and elevate your cooking skills with MasterChef Recipes.", bodyFont));

                document.Add(new Paragraph("\nFor Chefs:", bodyFont));
                document.Add(new Paragraph("Sign up today and start sharing your culinary masterpieces. Reach a global audience, monetize your recipes, and grow your professional network with the MasterChef Recipe Platform.", bodyFont));

                document.Add(new Paragraph("\nContact Us", bodyFont));
                document.Add(new Paragraph("For more information, visit our website at www.masterchefrecipe.com or contact our support team at support@masterchefrecipe.com.", bodyFont));
                string path = _webHostEnvironment.WebRootPath + "/Home/images";

                string imgcombine = Path.Combine(path, "Recipe-removebg-preview.png");

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgcombine);

                img.ScaleToFit(200f, img.Height);

                img.BorderColor = BaseColor.Black;
                img.BorderWidth = 1f;


                document.Add(img);
                document.Close();
                writer.Close();
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string pdfPath = Path.Combine(wwwRootPath, "pdfs", "MasterChefServices.pdf");

                using (var fileStream = new FileStream(pdfPath, FileMode.Create))
                {
                    memoryStream.WriteTo(fileStream);
                }
               
            }
            // Construct the URL to the PDF file
            string pdfUrl = $"{Request.Scheme}://{Request.Host}/pdfs/MasterChefServices.pdf";

            // Generate the QR code
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(pdfUrl, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new PngByteQRCode(qrCodeData);
                var qrCodeBytes = qrCode.GetGraphic(20);

                // Define the directory to save the QR code
                string qrCodeDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "qrcodes");
                if (!Directory.Exists(qrCodeDirectory))
                {
                    Directory.CreateDirectory(qrCodeDirectory);
                }

                // Define the path to save the QR code image
                string qrCodePath = Path.Combine(qrCodeDirectory, "pdf-qr.png");

                // Save the QR code as a PNG file
                System.IO.File.WriteAllBytes(qrCodePath, qrCodeBytes);

                // Create and send the email with the QR code
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("liliane79@ethereal.email");
                    mail.To.Add($"{Email}");
                    mail.Subject = " MasterChefServicePDF with QR Code";
                    mail.Body = $"Please scan the QR code below to access your PDF:\n\n";

                    string bodyHtml = $@"
                        <html>
                        <head>
                            <style>
                                img.qr-code {{width: 300px; height: 400px; }}
                            </style>
                        </head>
                        <body>
                            <p>Please scan the QR code below to access your PDF:</p>
                            <img class='qr-code' src='data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}' alt='QR Code'/>
                        </body>
                        </html>";

                    mail.Body = bodyHtml;
                    mail.IsBodyHtml = true;
                    // Setup SMTP
                    using (SmtpClient smtpServer = new SmtpClient("smtp.ethereal.email"))
                    {
                        smtpServer.Port = 587;
                        smtpServer.Credentials = new NetworkCredential("liliane79@ethereal.email", "U9Qmf8sPcwKFhHXfVP");
                        smtpServer.EnableSsl = true;

                        // Send email
                        smtpServer.Send(mail);
                    }
                }
                TempData["message"] = "check your email you are subscribed successfully";
                return RedirectToAction("Index", "Home");
            }


           
        }




      

    }
}





            // URL to the PDF file
            //string pdfUrl = $"{Request.Scheme}://{Request.Host}/pdfs/Recipe.pdf";
            //string pdfFilePath = "http://localhost:7170/pdfs/Recipe.pdf";
            //using (var qrGenerator = new QRCodeGenerator())
            //{
            //    var qrCodeData = qrGenerator.CreateQrCode(pdfFilePath, QRCodeGenerator.ECCLevel.Q);
            //    var qrCode = new QRCode(qrCodeData);
            //    using (var qrCodeImage = qrCode.GetGraphic(20))
            //    {
            //        using (var ms = new MemoryStream())
            //        {
            //            qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //            var byteImage = ms.ToArray();
            //            return File(byteImage, "image/png");
            //        }
            //    }
            //}
        
 
