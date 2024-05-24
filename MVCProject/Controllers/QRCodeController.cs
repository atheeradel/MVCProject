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
using SQLitePCL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SkiaSharp;
using static QRCoder.PayloadGenerator;

namespace MVCProject.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ModelContext _context;


        public QRCodeController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
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
            string pdfUrl = $"https://drive.google.com/file/d/1-gdSKERhZqvYVgHux9tSOMdX77ZoffBu/view?usp=sharing";

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

        
        public IActionResult usergenerate(string Email)
        {


            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new PDF document
                var userid = HttpContext.Session.GetInt32("UserId");
                var user = _context.Userinfos.Where(x => x.UserId == userid).SingleOrDefault();
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Add the content to the PDF document
                var titleFont = FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                var headingFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD);
                var normalFont = FontFactory.GetFont("Arial", 12);

                // Title
                document.Add(new Paragraph("Welcome to MasterChef!", titleFont));
                document.Add(new Paragraph(" ")); // Add a blank line

                // Subtitle
                document.Add(new Paragraph("Your Gateway to Culinary Excellence", headingFont));
                document.Add(new Paragraph(" ")); // Add a blank line

                // Content
                string[] content = new string[]
                {
                    $"Dear {user.Firstname}{user.Lastname},",
                    "Thank you for registering with MasterChef! We are delighted to have you as part of our culinary community. MasterChef is your ultimate destination for exploring a world of flavors, crafted by some of the finest chefs from around the globe. Our mission is to make gourmet cooking accessible to everyone by offering recipes at symbolic prices.",
                    "What You Can Expect",
                    "As a registered member, you will receive:",
                    "1. Exclusive Recipes: Discover unique and delicious recipes curated by world-renowned chefs. Each recipe is designed to bring the taste of fine dining into your home.",
                    "2. Regular Updates: Stay inspired with our latest additions. We update our recipe collection regularly, ensuring you always have something new to try.",
                    "3. Special Discounts: Enjoy special promotions and discounts exclusive to our members. Cooking gourmet meals has never been more affordable!",
                    "4. Chef Spotlights: Learn more about the chefs behind your favorite recipes. Get insights into their cooking philosophies, tips, and more.",
                    "5. User-Friendly Platform: Easily browse, purchase, and download recipes through our intuitive online system. Cooking delicious meals is just a few clicks away.",
                    "How It Works",
                    "1. Browse: Explore our extensive recipe collection, categorized by cuisine, dietary preferences, difficulty level, and more.",
                    "2. Select: Choose your favorite recipes and add them to your cart.",
                    "3. Purchase: Complete your purchase securely and receive your recipes instantly.",
                    "4. Cook: Follow the detailed instructions and enjoy cooking like a professional chef.",
                    "5. Repeat: Check your email regularly for new recipes and updates.",
                    "Stay Connected",
                    "To ensure you never miss out on our latest offerings, we will send you regular emails with all the new and exciting recipes added to our collection. Be sure to keep an eye on your inbox for these updates.",
                    "We value your feedback! If you have any suggestions or questions, please don't hesitate to reach out to our customer support team at [Customer Support Email].",
                    "Happy Cooking!",
                    "We are thrilled to be part of your culinary journey. With MasterChef, every meal is an opportunity to explore new tastes and refine your cooking skills.",
                    "Bon appétit!",
                    "Best regards,",
                    "The MasterChef Team"
                };


                foreach (string line in content)
                {
                    document.Add(new Paragraph(line, normalFont));
                    document.Add(new Paragraph(" ")); // Add a blank line between paragraphs
                }
                string path = _webHostEnvironment.WebRootPath + "/Home/images";

                string imgcombine = Path.Combine(path, "Recipe-removebg-preview.png");

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgcombine);

                img.ScaleToFit(200f, img.Height);

                img.BorderColor = BaseColor.Black;
                img.BorderWidth = 1f;


                document.Add(img);

                // Close the PDF document
                document.Close();
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string pdfPath = Path.Combine(wwwRootPath, "pdfs", "MasterChefServicesForUser.pdf");

                using (var fileStream = new FileStream(pdfPath, FileMode.Create))
                {
                    ms.WriteTo(fileStream);
                }
                string pdfUrl = $"{Request.Scheme}://{Request.Host}/pdfs/MasterChefServicesForUser.pdf";

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
                       

                        string bodyHtml = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            color: #333333;
            line-height: 1.6;
        }}
        .container {{
            width: 100%;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #eaeaea;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }}
        .header {{
            text-align: center;
            padding: 10px 0;
        }}
        .header h1 {{
            color: Black;
            margin: 0;
        }}
        .content {{
            padding: 20px 0;
        }}
        .qr-code {{
            display: block;
            margin: 0 auto;
            width: 200px;
            height: 200px;
        }}
        .footer {{
            text-align: center;
            padding: 10px 0;
            font-size: 0.9em;
            color: #777777;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>

            <h1>Welcome to MasterChef!</h1>

        </div>
        <div class='content'>
            <p>Dear {user.Firstname}{user.Lastname},</p>
            <p>Thank you for joining our culinary community! We are excited to have you on board and can't wait for you to start exploring delicious recipes crafted by world-renowned chefs.</p>
            <p>To access your welcome PDF, please scan the QR code below:</p>
            <img class='qr-code' src='data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}' alt='QR Code'/>

            <p>If you have any questions or need assistance, feel free to contact our support team at [Info@MasterChef.com].</p>
            <p>Happy Cooking!</p>
            <p>Best regards,<br>The MasterChef Team</p>
        </div>
        <div class='footer'>
            &copy; 2024 MasterChef. All rights reserved.
        </div>
    </div>
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
                    return RedirectToAction("UserIndex", "Home");


                }



            }
        }




        public IActionResult chefgenerate(string Email)
        {


            using (MemoryStream ms = new MemoryStream())
            {
                // Create a new PDF document
                var userid = HttpContext.Session.GetInt32("ChefId");
                var user = _context.Userinfos.Where(x => x.UserId == userid).SingleOrDefault();
                var email = _context.Contactus.Where(x => x.ContId == 23).Select(x=>x.Contactinfo).SingleOrDefault();
                
               
                Document document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                // Add the content to the PDF document
                var titleFont = FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);
                var headingFont = FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD);
                var normalFont = FontFactory.GetFont("Arial", 12);

                // Title
                document.Add(new Paragraph("Welcome to MasterChef!", titleFont));
                document.Add(new Paragraph(" ")); // Add a blank line

                // Subtitle
                document.Add(new Paragraph("Your Gateway to Culinary Excellence", headingFont));
                document.Add(new Paragraph(" ")); // Add a blank line

                // Content
                string[] content = new string[]
                {
            $"Dear Chef {user.Firstname}{user.Lastname},",
            "Thank you for registering with MasterChef! We are delighted to welcome you to our prestigious culinary community. MasterChef is your ultimate platform for sharing and selling your culinary creations with food enthusiasts from around the world. Our mission is to make gourmet cooking accessible to everyone by offering recipes at symbolic prices.",
            "What You Can Expect",
            "As a registered chef, you will receive:",
            "1. Exposure: Showcase your unique recipes to a global audience and gain recognition for your culinary expertise.",
            "2. Revenue: Earn from your recipes by selling them through our platform at affordable prices.",
            "3. Regular Updates: Stay informed with our latest features and updates, ensuring your presence remains dynamic and engaging.",
            "4. Chef Spotlights: Get featured in our Chef Spotlights section, where we highlight the stories and talents of our top chefs.",
            "5. User-Friendly Platform: Easily upload, manage, and track the performance of your recipes through our intuitive online system.",
            "How It Works",
            "1. Upload: Share your favorite recipes, complete with ingredients, instructions, and photos.",
            "2. Manage: Keep track of your recipe sales and customer feedback through your dashboard.",
            "3. Update: Regularly update your recipe collection to keep your audience engaged.",
            "4. Engage: Interact with your customers, answer their questions, and build a loyal following.",
            "5. Earn: Receive payments for your recipes directly to your account.",
            "Stay Connected",
            "To ensure you stay up-to-date with all our latest offerings, we will send you regular emails with new features, updates, and tips on how to maximize your presence on MasterChef.",
            $"We value your feedback! If you have any suggestions or questions, please don't hesitate to reach out to our customer support team at {email}.",
            "Happy Cooking!",
            "We are excited to have you on board and look forward to seeing your culinary creations inspire food lovers everywhere.",
            "Bon appétit!",
            "Best regards,",
            "The MasterChef Team"
                };

                foreach (string line in content)
                {
                    document.Add(new Paragraph(line, normalFont));
                    document.Add(new Paragraph(" ")); // Add a blank line between paragraphs
                }

                // Close the PDF document
               

                string path = _webHostEnvironment.WebRootPath + "/Home/images";

                string imgcombine = Path.Combine(path, "Recipe-removebg-preview.png");

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgcombine);

                img.ScaleToFit(200f, img.Height);

                img.BorderColor = BaseColor.Black;
                img.BorderWidth = 1f;


                document.Add(img);

                // Close the PDF document
                document.Close();
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string pdfPath = Path.Combine(wwwRootPath, "pdfs", "MasterChefServicesForChef.pdf");

                using (var fileStream = new FileStream(pdfPath, FileMode.Create))
                {
                    ms.WriteTo(fileStream);
                }


                string pdfUrl = $"{Request.Scheme}://{Request.Host}/pdfs/MasterChefServicesForChef.pdf";

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


                        string bodyHtml = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            color: #333333;
            line-height: 1.6;
        }}
        .container {{
            width: 100%;
            max-width: 600px;
            margin: 0 auto;
            padding: 20px;
            border: 1px solid #eaeaea;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }}
        .header {{
            text-align: center;
            padding: 10px 0;
        }}
        .header h1 {{
            color: Black;
            margin: 0;
        }}
        .content {{
            padding: 20px 0;
        }}
        .qr-code {{
            display: block;
            margin: 0 auto;
            width: 200px;
            height: 200px;
        }}
        .footer {{
            text-align: center;
            padding: 10px 0;
            font-size: 0.9em;
            color: #777777;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>

            <h1>Welcome to MasterChef!</h1>

        </div>
        <div class='content'>
            <p>Dear Chef {user.Firstname} {user.Lastname},</p>
            <p>Thank you for joining our culinary community! We are excited to have you on board and can't wait for you to start Adding your delicious recipes crafted by Your Hand.</p>
            <p>To access your welcome PDF, please scan the QR code below:</p>
            <img class='qr-code' src='data:image/png;base64,{Convert.ToBase64String(qrCodeBytes)}' alt='QR Code'/>

            <p>If you have any questions or need assistance, feel free to contact our support team at [info@MasteChef.com].</p>
            <p>Happy Cooking!</p>
            <p>Best regards,<br>The MasterChef Team</p>
        </div>
        <div class='footer'>
            &copy; 2024 MasterChef. All rights reserved.
        </div>
    </div>
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
                    return RedirectToAction("ChefIndex", "Home");


                }



            }
        }
    }
}



        
 
