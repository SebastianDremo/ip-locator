# ip-locator
Recruitment task for junior/mid back-end developer 

# To run this project: 
* run dotnet restore in main folder of project 
* go to locator.Web and run dotent ef database update 

# For testing
First of all we have to add some ip locations to our database by searching them in IpStack
I suggest to do the following: 
  * run the app
  * make POST request at  localhost/localization/locate/{ip/dns}
  * some of ips - 2a03:2880:f134:83:face:b00c::25de / 176.32.103.205 / 107.180.57.162 / 185.66.120.38
  * dns - github.com / google.com
 
 ## Add and locate ip 
 * /localization/locate/{ip/dns}
 
 ## Get all stored localizations in DB 
 * /localization/all-localizations
 
 ## Remove given localization from DB
 * /localizations/remove/{ip}?removeAllRows=true/false 
 (removeAllRows is optional parameter set on default to fale, you can remove all rows with ip equal to given ip by it) 
 
 ### Used third party packages 
 * Testing - NUnit with Moq
 * Mapping - AutoMapper
 * ORM - EntityFramework Core
 
 
