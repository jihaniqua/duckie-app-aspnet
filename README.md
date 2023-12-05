# Duckie Web App

Assignment for COMP 2084: "Server-Side Scripting-ASP.NET"
by Jihan Duerme

### Update (Dec 5, 2023)
## Assignment 4 - Unit Testing
The goal for this assignment is to create and execute tests for the Child Profiles Controllers within the app.
- Added dependencies
- Created 3 mock data
- 3 GET & 3 POST test methods for Edit
### Test Methods
1. GET Edit - Returns Error if there's no Id given
2. GET Edit - Returns Error if Id doesn't exist in the database
3. GET Edit - Gets a ChildProfile object from database with matching values to model
4. POST Edit - Returns Error if Id is different from the ChildProfileId
5. POST Edit - Returns Error if Id doesn't exist in the database
6. POST Edit  - Saves changes and redirects to Index of valid data

## Assignment requirements
- Plan and create a new .NET Core MVC Web Application using C#
- Integrate it with a SQL Server database to perform CRUD operations

## Purpose of this application
The purpose of my application is to create a digital space exclusively designed for parents. They can manage various aspects of their child’s life—saving milestones, archiving art project images, documenting medical information, accessing extracurriculars and school schedules, and writing journal entries for their children to read in the future. The application aims to provide one centralized location for essential information and memories, and create a digital time capsule to share with their children.

## Why it is useful
This application provides parents with a convenient way to save and revisit all child-related information in one organized location, making it a useful tool. Firstly, by consolidating essential information, it simplifies the management of multiple sources, saving time otherwise spent searching for or managing scattered records. Secondly, it creates emotional connections and preserves precious memories that 
would be nice to look back to whenever they want. Thirdly, it enables parents to access and collaborate on information, facilitating communication and cooperation among family members.

## Bonus 1 - Assignment 2
Created my own CSS for the Milestones and some parts of the website like the homepage. However, I didn't get enough time to finish the rest of the pages. Instead of the default table, I made each item into cards for a better and more fun way to read the Milestones content. I also added a logo on the nav bar, and images and features list on the homepage. All custom CSS are inside wwwroot/css/bonus.css

## Live site
duckie.azurewebsites.net
