# SonarQubeTest
My goal is to generate a code coverage file with correct metrics which I can send to SonarQube via a build step in TFS.  

We are using Cake to build and test our application, so the code coverage report needs to be done as part of that process, then
I can tell the TFS SonarQube task where to find the coverage report file and hopefully it will be imported successfully into
SonarQube and we can start to see some nice metrics about our code quality.

