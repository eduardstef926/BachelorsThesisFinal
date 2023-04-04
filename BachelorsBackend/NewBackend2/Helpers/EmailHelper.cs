
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
    }
}
