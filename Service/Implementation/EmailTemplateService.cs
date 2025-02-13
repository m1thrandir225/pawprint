using Domain.enums;
using Service.Interface;

namespace Service.Implementation;

public class EmailTemplateService : IEmailTemplateService
{
    public EmailTemplateService()
    {

    }
    public string GenerateEmailTemplate(EmailTemplateType templateType, Dictionary<string, string> templateData)
    {
        return templateType switch
        {
            EmailTemplateType.UserRegistration => GenerateUserRegistrationTemplate(templateData),
            EmailTemplateType.ShelterRegistration => GenerateShelterRegistrationTemplate(templateData),
            EmailTemplateType.PetListingAdoption => GeneratePetListingAdoptionTemplate(templateData),
            EmailTemplateType.AdoptionApproval => GenerateAdoptionApprovalTemplate(templateData),
            _ => throw new ArgumentException("Invalid template type")
        };
    }

    private string GenerateUserRegistrationTemplate(Dictionary<string, string> data)
    {
        return $@"<!--
* This email was built using Tabular.
* For more information, visit https://tabular.email
-->
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" lang=""en"">
<head>
<title></title>
<meta charset=""UTF-8"" />
<meta http-equiv=""Content-Type"" content=""text/html; charset=UTF-8"" />
<!--[if !mso]>-->
<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
<!--<![endif]-->
<meta name=""x-apple-disable-message-reformatting"" content="""" />
<meta content=""target-densitydpi=device-dpi"" name=""viewport"" />
<meta content=""true"" name=""HandheldFriendly"" />
<meta content=""width=device-width"" name=""viewport"" />
<meta name=""format-detection"" content=""telephone=no, date=no, address=no, email=no, url=no"" />
<style type=""text/css"">
table {{
border-collapse: separate;
table-layout: fixed;
mso-table-lspace: 0pt;
mso-table-rspace: 0pt
}}
table td {{
border-collapse: collapse
}}
.ExternalClass {{
width: 100%
}}
.ExternalClass,
.ExternalClass p,
.ExternalClass span,
.ExternalClass font,
.ExternalClass td,
.ExternalClass div {{
line-height: 100%
}}
body, a, li, p, h1, h2, h3 {{
-ms-text-size-adjust: 100%;
-webkit-text-size-adjust: 100%;
}}
html {{
-webkit-text-size-adjust: none !important
}}
body, #innerTable {{
-webkit-font-smoothing: antialiased;
-moz-osx-font-smoothing: grayscale
}}
#innerTable img+div {{
display: none;
display: none !important
}}
img {{
Margin: 0;
padding: 0;
-ms-interpolation-mode: bicubic
}}
h1, h2, h3, p, a {{
line-height: inherit;
overflow-wrap: normal;
white-space: normal;
word-break: break-word
}}
a {{
text-decoration: none
}}
h1, h2, h3, p {{
min-width: 100%!important;
width: 100%!important;
max-width: 100%!important;
display: inline-block!important;
border: 0;
padding: 0;
margin: 0
}}
a[x-apple-data-detectors] {{
color: inherit !important;
text-decoration: none !important;
font-size: inherit !important;
font-family: inherit !important;
font-weight: inherit !important;
line-height: inherit !important
}}
u + #body a {{
color: inherit;
text-decoration: none;
font-size: inherit;
font-family: inherit;
font-weight: inherit;
line-height: inherit;
}}
a[href^=""mailto""],
a[href^=""tel""],
a[href^=""sms""] {{
color: inherit;
text-decoration: none
}}
</style>
<style type=""text/css"">
@media (min-width: 481px) {{
.hd {{ display: none!important }}
}}
</style>
<style type=""text/css"">
@media (max-width: 480px) {{
.hm {{ display: none!important }}
}}
</style>
<style type=""text/css"">
@media (max-width: 480px) {{
.t36,.t41{{mso-line-height-alt:0px!important;line-height:0!important;display:none!important}}.t37{{padding:40px!important}}.t39{{border-radius:0!important;width:480px!important}}.t15,.t34,.t9{{width:398px!important}}.t27{{text-align:left!important}}.t26{{vertical-align:top!important;width:auto!important;max-width:100%!important}}
}}
</style>
<!--[if !mso]>-->
<link href=""https://fonts.googleapis.com/css2?family=Sofia+Sans:wght@700&amp;family=Open+Sans:wght@400;600&amp;family=Montserrat:wght@700&amp;display=swap"" rel=""stylesheet"" type=""text/css"" />
<!--<![endif]-->
<!--[if mso]>
<xml>
<o:OfficeDocumentSettings>
<o:AllowPNG/>
<o:PixelsPerInch>96</o:PixelsPerInch>
</o:OfficeDocumentSettings>
</xml>
<![endif]-->
</head>
<body id=""body"" class=""t44"" style=""min-width:100%;Margin:0px;padding:0px;background-color:#BFDBFE;""><div class=""t43"" style=""background-color:#BFDBFE;""><table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" align=""center""><tr><td class=""t42"" style=""font-size:0;line-height:0;mso-line-height-rule:exactly;background-color:#BFDBFE;"" valign=""top"" align=""center"">
<!--[if mso]>
<v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""true"" stroke=""false"">
<v:fill color=""#BFDBFE""/>
</v:background>
<![endif]-->
<table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" border=""0"" align=""center"" id=""innerTable""><tr><td><div class=""t36"" style=""mso-line-height-rule:exactly;mso-line-height-alt:50px;line-height:50px;font-size:1px;display:block;"">&nbsp;&nbsp;</div></td></tr><tr><td align=""center"">
<table class=""t40"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""Margin-left:auto;Margin-right:auto;""><tr>
<!--[if mso]>
<td width=""600"" class=""t39"" style=""background-color:#FFFFFF;border:1px solid #EBEBEB;overflow:hidden;width:600px;border-radius:3px 3px 3px 3px;"">
<![endif]-->
<!--[if !mso]>-->
<td class=""t39"" style=""background-color:#FFFFFF;border:1px solid #EBEBEB;overflow:hidden;width:600px;border-radius:3px 3px 3px 3px;"">
<!--<![endif]-->
<table class=""t38"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""width:100%;""><tr><td class=""t37"" style=""padding:44px 42px 32px 42px;""><table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""width:100% !important;""><tr><td align=""left"">
<table class=""t4"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""Margin-right:auto;""><tr>
<!--[if mso]>
<td width=""42"" class=""t3"" style=""width:42px;"">
<![endif]-->
<!--[if !mso]>-->
<td class=""t3"" style=""width:42px;"">
<!--<![endif]-->
<table class=""t2"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""width:100%;""><tr><td class=""t1""><div style=""font-size:0px;""><img class=""t0"" style=""display:block;border:0;height:auto;width:100%;Margin:0;max-width:100%;"" width=""42"" height=""42"" alt="""" src=""https://21a809c8-2a08-421d-9da1-2ae37bc75552.b-cdn.net/e/b53e90b8-5351-420b-995e-c7f2715a76d3/197f6006-f9af-4fde-b846-b6d0b0c17a0d.png""/></div></td></tr></table>
</td></tr></table>
</td></tr><tr><td><div class=""t5"" style=""mso-line-height-rule:exactly;mso-line-height-alt:42px;line-height:42px;font-size:1px;display:block;"">&nbsp;&nbsp;</div></td></tr><tr><td align=""center"">
<table class=""t10"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""Margin-left:auto;Margin-right:auto;""><tr>
<!--[if mso]>
<td width=""514"" class=""t9"" style=""border-bottom:1px solid #EFF1F4;width:514px;"">
<![endif]-->
<!--[if !mso]>-->
<td class=""t9"" style=""border-bottom:1px solid #EFF1F4;width:514px;"">
<!--<![endif]-->
<table class=""t8"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""width:100%;""><tr><td class=""t7"" style=""padding:0 0 18px 0;""><h1 class=""t6"" style=""margin:0;Margin:0;font-family:Montserrat,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:28px;font-weight:700;font-style:normal;font-size:24px;text-decoration:none;text-transform:none;letter-spacing:-1px;direction:ltr;color:#141414;text-align:left;mso-line-height-rule:exactly;mso-text-raise:1px;"">You’re Now Part of the PawPrint Pack!</h1></td></tr></table>
</td></tr></table>
</td></tr><tr><td><div class=""t11"" style=""mso-line-height-rule:exactly;mso-line-height-alt:18px;line-height:18px;font-size:1px;display:block;"">&nbsp;&nbsp;</div></td></tr><tr><td align=""center"">
<table class=""t16"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""Margin-left:auto;Margin-right:auto;""><tr>
<!--[if mso]>
<td width=""514"" class=""t15"" style=""width:514px;"">
<![endif]-->
<!--[if !mso]>-->
<td class=""t15"" style=""width:514px;"">
<!--<![endif]-->
<table class=""t14"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""width:100%;""><tr><td class=""t13""><p class=""t12"" style=""margin:0;Margin:0;font-family:Open Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:25px;font-weight:400;font-style:normal;font-size:15px;text-decoration:none;text-transform:none;letter-spacing:-0.1px;direction:ltr;color:#141414;text-align:left;mso-line-height-rule:exactly;mso-text-raise:3px;"">Welcome! Your journey to helping pets find loving homes begins here. Let’s make a difference together.</p></td></tr></table>
</td></tr></table>
</td></tr><tr><td><div class=""t18"" style=""mso-line-height-rule:exactly;mso-line-height-alt:24px;line-height:24px;font-size:1px;display:block;"">&nbsp;&nbsp;</div></td></tr><tr><td align=""left"">
<table class=""t22"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""Margin-right:auto;""><tr>
<!--[if mso]>
<td class=""t21"" style=""background-color:#03045E;overflow:hidden;width:auto;border-radius:40px 40px 40px 40px;"">
<![endif]-->
<!--[if !mso]>-->
<td class=""t21"" style=""background-color:#03045E;overflow:hidden;width:auto;border-radius:40px 40px 40px 40px;"">
<!--<![endif]--><a href=""https://pawprint.sebastijanzindl.me/login"" style=""text-decoration: none;"">
<table class=""t20"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""width:auto;""><tr><td class=""t19"" style=""text-align:center;line-height:34px;mso-line-height-rule:exactly;mso-text-raise:5px;padding:0 23px 0 23px;""><span class=""t17"" style=""display:block;margin:0;Margin:0;font-family:Sofia Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:34px;font-weight:700;font-style:normal;font-size:16px;text-decoration:none;text-transform:none;letter-spacing:-0.2px;direction:ltr;color:#FFFFFF;text-align:center;mso-line-height-rule:exactly;mso-text-raise:5px;"">Find Your Pawfect Match</span></td></tr></table>
</a>

</td></tr></table>
</td></tr><tr><td><div class=""t31"" style=""mso-line-height-rule:exactly;mso-line-height-alt:40px;line-height:40px;font-size:1px;display:block;"">&nbsp;&nbsp;</div></td></tr><tr><td align=""center"">
<table class=""t35"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" style=""Margin-left:auto;Margin-right:auto;""><tr>
<!--[if mso]>
<td width=""514"" class=""t34"" style=""border-top:1px solid #DFE1E4;width:514px;"">
<![endif]-->
<!--[if !mso]>-->
<td class=""t34"" style=""border-top:1px solid #DFE1E4;width:514px;"">
<!--<![endif]-->
<table class=""t33"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""width:100%;""><tr><td class=""t32"" style=""padding:24px 0 0 0;""><div class=""t30"" style=""width:100%;text-align:left;""><div class=""t29"" style=""display:inline-block;""><table class=""t28"" role=""presentation"" cellpadding=""0"" cellspacing=""0"" align=""left"" valign=""top"">
<tr class=""t27""><td></td><td class=""t26"" valign=""top"">
<table role=""presentation"" width=""100%"" cellpadding=""0"" cellspacing=""0"" class=""t25"" style=""width:auto;""><tr><td class=""t24"" style=""background-color:#FFFFFF;text-align:center;line-height:20px;mso-line-height-rule:exactly;mso-text-raise:2px;""><span class=""t23"" style=""display:block;margin:0;Margin:0;font-family:Open Sans,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;line-height:20px;font-weight:600;font-style:normal;font-size:14px;text-decoration:none;direction:ltr;color:#222222;text-align:center;mso-line-height-rule:exactly;mso-text-raise:2px;"">© PawPrint</span></td></tr></table>
</td>
<td></td></tr>
</table></div></div></td></tr></table>
</td></tr></table>
</td></tr></table></td></tr></table>
</td></tr></table>
</td></tr><tr><td><div class=""t41"" style=""mso-line-height-rule:exactly;mso-line-height-alt:50px;line-height:50px;font-size:1px;display:block;"">&nbsp;&nbsp;</div></td></tr></table></td></tr></table></div><div class=""gmail-fix"" style=""display: none; white-space: nowrap; font: 15px courier; line-height: 0;"">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</div></body>
</html>";
    }

   private string GeneratePetListingAdoptionTemplate(Dictionary<string, string> data)
{
    return $@"
    <html>
    <body>
        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto;'>
            <h1>Great News, {data.GetValueOrDefault("OwnerOrShelterName", "User")}!</h1>
            
            <p>Your pet listing has been successfully submitted on PetAdopt.</p>
            
            <div style='background-color: #f4f4f4; padding: 15px; border-radius: 5px;'>
                <h2>Listing Details:</h2>
                <p><strong>Pet Name:</strong> {data.GetValueOrDefault("PetName", "N/A")}</p>
                <p><strong>Listing Type:</strong> {data.GetValueOrDefault("ListingType", "N/A")}</p>
                <p><strong>Status:</strong> Pending Approval</p>
            </div>

            <p>We will review your listing and notify you once it is approved.</p>
            
            <p>If you need to make any changes, visit your <a href='{data.GetValueOrDefault("DashboardLink", "#")}' style='color: #4CAF50; text-decoration: none;'>dashboard</a>.</p>

            <p>Thank you for trusting PetAdopt!</p>

            <p>Best regards,<br>PetAdopt Team</p>
        </div>
    </body>
    </html>";
}

private string GenerateShelterRegistrationTemplate(Dictionary<string, string> data)
{
    return $@"
    <html>
    <body>
        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto;'>
            <h1>Welcome to PetAdopt, {data.GetValueOrDefault("ShelterName", "Shelter")}!</h1>
            
            <p>Congratulations on registering with PetAdopt. Your account has been created successfully.</p>
            
            <div style='background-color: #f4f4f4; padding: 15px; border-radius: 5px;'>
                <h2>Your Account Details:</h2>
                <p><strong>Shelter Name:</strong> {data.GetValueOrDefault("ShelterName", "N/A")}</p>
                <p><strong>Email:</strong> {data.GetValueOrDefault("Email", "N/A")}</p>
            </div>
            
            <p>To complete your registration, please verify your email by clicking the link below:</p>
            
            <p style='text-align: center;'>
                <a href='{data.GetValueOrDefault("VerificationLink", "#")}' 
                   style='background-color: #4CAF50; color: white; padding: 10px 20px; 
                   text-decoration: none; border-radius: 5px;'>Verify Email</a>
            </p>
            
            <p>If you did not create this account, please ignore this email.</p>
            
            <p>Best regards,<br>PetAdopt Team</p>
        </div>
    </body>
    </html>";
}

    private string GenerateAdoptionApprovalTemplate(Dictionary<string, string> data)
    {
        return $@"
        <html>
        <body>
            <div style='font-family: Arial, sans-serif; max-width: 600px; margin: auto;'>
                <h1>Exciting News, {data.GetValueOrDefault("AdopterName", "Adopter")}!</h1>
                
                <p>Your adoption request has been approved.</p>
                
                <div style='background-color: #f4f4f4; padding: 15px; border-radius: 5px;'>
                    <h2>Adoption Details:</h2>
                    <p><strong>Pet Name:</strong> {data.GetValueOrDefault("PetName", "N/A")}</p>
                    <p><strong>Handover Date:</strong> {data.GetValueOrDefault("HandoverDate", "N/A")}</p>
                    <p><strong>Pickup Location:</strong> {data.GetValueOrDefault("PickupLocation", "TBD")}</p>
                </div>

                <p>Please make sure to prepare everything for the handover.</p>

                <p>For further details, check your <a href='{data.GetValueOrDefault("DashboardLink", "#")}' style='color: #4CAF50; text-decoration: none;'>adoption dashboard</a>.</p>

                <p>We’re happy to help a pet find a loving home!</p>

                <p>Best regards,<br>PetAdopt Team</p>
            </div>
        </body>
        </html>";
    }

}

