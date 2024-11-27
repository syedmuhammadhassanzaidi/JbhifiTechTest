# JB HI-FI Technical Challenge
This app allows the user to query the weather of a place by entering the city and country along with an API Key for validation. The app will return the weather for the place queried and if it encounters a problem it will return a relevant error message.

# The Problem
Develop and test a service in C# .NET and Javascript that fronts the Current Weather Data service. The service should have:
- Your own back-end API that enforces rate limiting that then on calls OpenWeatherMap.com with its API keys:
  - Rate limiting:
    - Apply your own API Key scheme.
    - Your API Key is rate limited to 5 weather reports an hour.
    - After that your service should respond in a way which communicates that the hourly limit has been exceeded.
    - Create 5 API Keys. Pick a convention for handling them that you like; using simple string constants is fine.
  - Functionality:
    - This back-end API should have a REST URL that accepts both a city name and country name.
    - Based upon these inputs, and the API Key (your scheme), your service should decide whether or not to call the OpenWeatherMap service with its API key (the two provided above).
    - If it does, the only weather data you need to return to the client is the description field from the weather JSON result. Whether it does or does not have this field, it should respond appropriately to the client.
- A webpage (front-end) that:
    - Uses the REST URL above.
    - Allows user to enter city name and country name.
    - Presents the result to user.
    - Handles any error.
 
# How to Run
### Prerequisites
Ensure you have node version 18.18.0 installed and the latest .NET SDK installed as the backend targets .NET Version 9.0.

### Process
The app consists of two parts a frontend written using react with typescript and a backend written in C#. To run the project simply open the .sln file using visual studio. Once this is done you will see all the projects loaded in the solution explorer even the frontend project. All you need to do to run the project is click the start button in the top menu of visual studio. It will automatically install any and all libaries required for the frontend from the package.json file and similary it will also run the backend as well.<br /><br />
Now that the app is running, you will need to use any of the API Keys provided in the API.txt file, simply copy and paste it into the API Key textbox and you can start searching for weather updates for any city by entering the city and country it belongs to in the respective textboxes.
- Important Note: Each API key is limited to 5 calls per hour. If you run out of API calls then you can restart the project and it will reset the count as the rate limiting is implemented using in-memory cache hence it does not persist once the app is shutdown or restarted.

# Testing
Unit tests have been implemented using NUnit for the OpenWeatherMap Service and they can be run using the test explorer in Visual Studio.
