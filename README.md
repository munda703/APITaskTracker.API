Task Tracker – Local Setup Guide
Here is your setup guide rewritten in **professional paragraph format**, suitable for a Word document:

---

# Task Tracker – Local Setup Guide

To set up the Task Tracker project locally, begin by cloning the repository from GitHub using the following link: [https://github.com/munda703/APITaskTracker.API.git](https://github.com/munda703/APITaskTracker.API.git). Once the repository has been successfully cloned to your machine, open Visual Studio and load the solution file. After opening the solution, ensure that the API project is set as the startup project. If it is not already selected, right-click on the API project within Solution Explorer and choose “Set as Startup Project.”

Before running the application, confirm that SQL Server is installed on your machine. This project uses Entity Framework Core with a SQL Server database and does not rely on an in-memory database. A properly installed and running SQL Server instance is therefore required for the application to function correctly.

The React frontend application is located inside the API solution folder. Specifically, navigate to the ClientApp folder within the APITaskTracker.API project, and inside it you will find the TaskTracker folder containing the UI source code.

Next, configure the HTTPS port used by the backend. Navigate to the APITaskTracker project, open the Properties folder, and locate the launchSettings.json file. Inside this file, find the HTTPS application URL (for example, [https://localhost:7141](https://localhost:7141)) and take note of the port number. It is important to always run the API using HTTPS to ensure the port remains consistent, as the frontend depends on this port for sending API requests.

After identifying the correct HTTPS port, navigate to the TaskTracker folder inside ClientApp and open the .env file. Update the VITE_API_URL variable to match the HTTPS port from launchSettings.json. For example, if the port is 7141, the value should be set to VITE_API_URL=[https://localhost:7141/api](https://localhost:7141/api). This ensures that the React application communicates with the correct backend endpoint.

Once configuration is complete, run the API project in Visual Studio. If everything has been set up correctly, Swagger UI will launch in the browser, displaying the available CRUD endpoints. The backend is built using .NET 8 with controller-based APIs and Entity Framework Core connected to SQL Server.

To run the React application, navigate to the TaskTracker folder inside ClientApp. Open Command Prompt in this directory and launch Visual Studio Code by typing “code .”. Within the VS Code terminal, first run “npm install” to install all required dependencies. After installation is complete, execute “npm run dev” to start the development server. The terminal will display a local development URL, which can be opened in a browser to access the application.

Once both the backend and frontend are running successfully, the Task Tracker application will be fully operational and connected to the database.
