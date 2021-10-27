# EarthquakeApi improvements

This is an example of a solution that can receive many improvements. Let's briefly point out some of them.

* Decoupled the file management of the UsgsService class. Create a new Helper folder where we can create a new "FileHelper" class.
   * Will provide public methods and better ways to create new tests. Also better cohesion in the design.
* Create tests for the UsgsService class and the new "FileHelper" class.
* Create a new method to get information from the USGS through its API (must be searched).
* Check the structure of folders in the project. Also, check the company's standards. If this project can grow, check out another concept such as Domain Driven Design.
* Planning to use the cloud. A tool like Azure DevOps is a good idea, it can integrate with analytics code tools like Sonnarqube (QA). They are good alternatives for CI / CD deployment.
  * Azure DevOps has GIT integration, Jenkins integration, its native deployment.
* Check the need for scalable flow. Docker for containerization is great.
* Code revision and code refactoring are always great recommendations.
