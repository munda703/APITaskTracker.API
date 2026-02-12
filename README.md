Task Tracker – Local Setup Guide
1. Clone the Repository
Clone the project from the GitHub link provided: https://github.com/munda703/APITaskTracker.API.git

2. Open and Run the API with Visual Studio
- Open Visual Studio.
- Load the cloned solution. If the seleted project is not API make the start up project to be the API project.
- Make sure SQL installed in the machine you are cloning this project to since this project is using entity framework core not in memory DB

- UI PROJECT IS INSIDE API PROJECT IN THE COLDER CALLED ClientApp
- Navigate to: TaskTrackerProject > APITaskTracker > Properties
- Open launchSettings.json.
- Copy the SSL port assigned by https (example: 7141).
Always run the API using https so the port stays consistent. and this port is the one I assigned to backend to know of where the calls are sent is going
3. Configure the React Environment File
Go to:
C:\Users\monde\OneDrive\Documents\visual studio projects\APITaskTracker.API\ClientApp\TaskTracker(where you clone your code go to the directory API folder contains UI Folder called ClientApp)
Open env and update apiUrl:
apiUrl: 'VITE_API_URL=https://localhost:7141/api'
Example:
apiUrl: 'VITE_API_URL=https://localhost:7141/api'
4. Verify the API
If configured correctly, Swagger UI will load with CRUD endpoints.
The backend uses .NET 8 with controller-based APIs (not minimal APIs).
5. Run the react Application
Navigate to:
C:\Users\monde\OneDrive\Documents\visual studio projects\APITaskTracker.API\ClientApp\TaskTracker
Open Command Prompt in this folder:
cmd
Launch VS Code:
code .
In VS Code terminal:
npm install
Then to run it npm run dev. from here you successful ran the 
