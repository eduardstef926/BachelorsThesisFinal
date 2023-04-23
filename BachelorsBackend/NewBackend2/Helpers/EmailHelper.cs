
namespace NewBackend2.Helpers
{
    public class EmailHelper
    {
        public static IConfigurationRoot GetEmailConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            return config;
        }

        public static string GetUserEmail()
        {
            var config = GetEmailConfiguration();   
            return config.GetSection("EmailConfiguration:email").Value;
        }

        public static string GetUserPassword()
        {
            var config = GetEmailConfiguration();
            return config.GetSection("EmailConfiguration:password").Value;
        }

        public static string GetPasswordResetEmailTemplate()
        {
            return @"<html>
                        <head>
                            <style>
                                body {
                                  font-family: Arial, sans-serif;
                                  font-size: 16px;
                                  line-height: 1.5;
                                  color: #333333;
                                }
                                
                                .container {
                                  max-width: 600px;
                                  margin: 0 auto;
                                  padding: 20px;
                                  background-color: #f8f8f8;
                                  border-radius: 10px;
                                  box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
                                }

                                .text-div {
                                  width: 7.5rem;
                                  height: 1.4rem;
                                  border-radius: 2px;
                                  background-color: green;
                                }
                                
                                h1, h2, h3 {
                                  margin-top: 0;
                                  margin-bottom: 20px;
                                  font-weight: normal;
                                }
                                
                                p {
                                  margin-top: 0;
                                  margin-bottom: 20px;
                                }
                                
                                a {
                                  color: #007bff;
                                  padding-left: 0.8rem;
                                  text-decoration: none;
                                }
                                
                                a:hover {
                                  text-decoration: underline;
                                }
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <h1>Password Reset</h1>
                                <p>Dear [Recipient Name],</p>
                                <p>Please click on the link below to reset your password:</p>
                                <div class='text-div'>
                                    <p><a href='[Link]' style='color: white'>Reset password</a></p>
                                </div><br><br>
                                <p>The Virtual Clinic Team</p>
                            </div>
                        </body>
                    </html>";
        }

        public static string GetUserWelcomeEmailTemplate()
        {
            return @"<html>
                        <head>
                            <style>
                                body {
                                  font-family: Arial, sans-serif;
                                  font-size: 16px;
                                  line-height: 1.5;
                                  color: #333333;
                                }
                                
                                .container {
                                  max-width: 600px;
                                  margin: 0 auto;
                                  padding: 20px;
                                  background-color: #f8f8f8;
                                  border-radius: 10px;
                                  box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
                                }

                                .text-div {
                                  width: 7rem;
                                  height: 1.4rem;
                                  border-radius: 2px;
                                  background-color: green;
                                }
                                
                                h1, h2, h3 {
                                  margin-top: 0;
                                  margin-bottom: 20px;
                                  font-weight: normal;
                                }
                                
                                p {
                                  margin-top: 0;
                                  margin-bottom: 20px;
                                }
                                
                                a {
                                  color: #007bff;
                                  padding-left: 1rem;
                                  text-decoration: none;
                                }
                                
                                a:hover {
                                  text-decoration: underline;
                                }
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <h1>Thank you for signing in</h1>
                                <p>Dear [Recipient Name],</p>
                                <p>Welcome to the team, please click the link below to confirm your email:</p>
                                <div class='text-div'>
                                    <p><a href='[Link]' style='color: white'>Confirm email</a></p>
                                </div><br><br>
                                <p>The Virtual Clinic Team</p>
                            </div>
                        </body>
                    </html>";
        }

        public static string GetAppointmentConfirmationEmailTemplate()
        {
            return @"<html>
                    <head>
                    <style>
                        body {
                        font-family: Arial, sans-serif;
                        font-size: 16px;
                        line-height: 1.5;
                        color: #333333;
                        }

                        .container {
                        max-width: 480px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #f8f8f8;
                        border-radius: 10px;
                        box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
                        }

                        h1 {
                        margin-top: 0;
                        margin-bottom: 20px;
                        font-weight: normal;
                        }

                        p {
                        margin-top: 0.5rem;
                        font-size: 14px;
                        margin-bottom: 20px;
                        }
      
                        .doctor-line {
                        display: flex;
                        gap: 0.8rem;
                        }
      
                        .first-container {
                        font-weight: bold;
                        font-size: 30px;
                        padding-top: 1.5rem;
                        display: flex;
                        }
                
                        .last-container {
                        padding-bottom: 1rem;
                        }
      
                        .appointment-details {
                        border-radius: 6px;
                        padding-left: 0.8rem;
      	                background-color: #e9f0eb;
                        }

                    </style>
                    </head>
                    <body>
                    <div class='container'>
                        <h1>Your appointment is confirmed!</h1>
                        <p>Dear [Recipient Name],</p>
                        <p>Your appointment details are described below:</p>
                        <div class=""appointment-details"">
                        <div class=""first-container"">
                            <p>Dr.</p>
                            <p>[Doctor Name]</p>
                        </div>
                        <div>
                            <p>Hospital:</p>
                            <p style='font-weight: bold;'>[Hospital]</p>
                        </div>
                        <div>
                            <p>Location:</p>
                            <p style='font-weight: bold;'>[City]</p>
                        </div>
                        <div>
                            <p>Appointment Date:</p>
                            <p style='font-weight: bold;'>[Date]</p>
                        </div> 
                        <div>
                            <p>Appointment Hour:</p>
                            <p style='font-weight: bold;'>[Hour]</p>
                        </div> 
                        <div class=""last-container"">
                            <p>Cost:</p>
                            <p style='font-weight: bold;'>[Cost] lei</p>
                        </div> 
                        </div>
                        <p style='margin-top: 1rem;'>The Virtual Clinic Team</p>
                    </div>
                    </body>
                </html>";

        }

        public static string GetReviewEmailTemplate()
        {
            return @"<html>
                    <head>
                    <style>
                          body {
                            font-family: Arial, sans-serif;
                            font-size: 16px;
                            line-height: 1.5;
                            color: #333333;
                          }

                          .container {
                            max-width: 600px;
                            margin: 0 auto;
                            padding: 20px;
                            background-color: #f8f8f8;
                            border-radius: 10px;
                            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
                          }

                          .text-div {
                            width: 4.5rem;
                            height: 1.4rem;
                            border-radius: 2px;
                            background-color: green;
                          }

                          h1 {
                            margin-top: 0;
                            margin-bottom: 20px;
                            font-weight: normal;
                          }

                          p{
                            margin-top: 0;
                            margin-bottom: 20px;
                          }

                          a{
                            color: #007bff;
                            padding-left: 1rem;
                            text-decoration: none;
                          }

                          a:hover {
                            text-decoration: underline;
                          }
                        </style>
                        </head>
                        <body>
                        <div class='container'>
                          <h1>Leave a feedback!</h1>
                          <p>Dear [Recipient Name],</p>
                          <p>I hope you had an amazing experience. We would appreciate if you could leave a review.</p>
                          <div class='text-div'>
                            <p><a href='[Link]' style='color: white'>Review</a></p>
                          </div><br><br>
                          <p>The Virtual Clinic Team</p>
                        </div>
                        </body>
                    </html>";
        }


        public static string GetAppointmentReminderEmailTemplate()
        {
            return @"<html>
                    <head>
                    <style>
                        body {
                        font-family: Arial, sans-serif;
                        font-size: 16px;
                        line-height: 1.5;
                        color: #333333;
                        }

                        .container {
                        max-width: 480px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #f8f8f8;
                        border-radius: 10px;
                        box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
                        }

                        h1 {
                        margin-top: 0;
                        margin-bottom: 20px;
                        font-weight: normal;
                        }

                        p {
                        margin-top: 0.5rem;
                        font-size: 14px;
                        margin-bottom: 20px;
                        }
      
                        .doctor-line {
                        display: flex;
                        gap: 0.8rem;
                        }
      
                        .first-container {
                        font-weight: bold;
                        font-size: 30px;
                        padding-top: 1.5rem;
                        display: flex;
                        }
                
                        .last-container {
                        padding-bottom: 1rem;
                        }
      
                        .appointment-details {
                        border-radius: 6px;
                        padding-left: 0.8rem;
      	                background-color: #e9f0eb;
                        }

                    </style>
                    </head>
                    <body>
                    <div class='container'>
                        <h1>Appointment reminder!</h1>
                        <p>Dear [Recipient Name],</p>
                        <p>We want to remind you that you have an appointment:</p>
                        <div class=""appointment-details"">
                        <div class=""first-container"">
                            <p>Dr.</p>
                            <p>[Doctor Name]</p>
                        </div>
                        <div>
                            <p>Hospital:</p>
                            <p style='font-weight: bold;'>[Hospital]</p>
                        </div>
                        <div>
                            <p>Location:</p>
                            <p style='font-weight: bold;'>[City]</p>
                        </div>
                        <div>
                            <p>Appointment Date:</p>
                            <p style='font-weight: bold;'>[Date]</p>
                        </div> 
                        <div>
                            <p>Appointment Hour:</p>
                            <p style='font-weight: bold;'>[Hour]</p>
                        </div> 
                        <div class=""last-container"">
                            <p>Cost:</p>
                            <p style='font-weight: bold;'>[Cost] lei</p>
                        </div> 
                        </div>
                        <p style='margin-top: 1rem;'>The Virtual Clinic Team</p>
                    </div>
                    </body>
                </html>";

        }
    }
}
